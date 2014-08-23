﻿/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System;
using Apex.Common;
using UnityEngine;

    /// <summary>
    /// Represents a portal between two sections of a grid or two grids.
    /// </summary>
    public sealed class GridPortal
    {
        private string _name;
        private IGrid _gridOne;
        private IGrid _gridTwo;
        private PortalCell _cellOne;
        private PortalCell _cellTwo;
        private Bounds _portalOneBounds;
        private Bounds _portalTwoBounds;
        private AttributeMask _exclusiveTo;
        private bool _enabled;

        private GridPortal()
        {
        }

        /// <summary>
        /// Gets the name of the portal.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the first grid of the portal.
        /// </summary>
        /// <value>
        /// The first grid.
        /// </value>
        public IGrid gridOne
        {
            get { return _gridOne; }
        }

        /// <summary>
        /// Gets the second grid of the portal.
        /// </summary>
        /// <value>
        /// The second grid.
        /// </value>
        public IGrid gridTwo
        {
            get { return _gridTwo; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridPortal"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                if (_enabled == value)
                {
                    return;
                }

                _enabled = value;

                if (_enabled)
                {
                    _cellOne.Activate();
                    _cellTwo.Activate();

                    _gridOne.TouchSections(_portalOneBounds);
                    _gridTwo.TouchSections(_portalTwoBounds);
                }
                else
                {
                    _cellOne.Deactivate();
                    _cellTwo.Deactivate();

                    _gridOne.TouchSections(_portalOneBounds);
                    _gridTwo.TouchSections(_portalTwoBounds);
                }
            }
        }

        /// <summary>
        /// Creates a GridPortal instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="exclusiveTo">The attribute mask to indicate if this portal is exclusive to (only available to) certain units.</param>
        /// <param name="action">The action that handles moving between portals.</param>
        /// <returns>The portal instance</returns>
        public static GridPortal Create(string name, Bounds portalOne, Bounds portalTwo, AttributeMask exclusiveTo, IPortalAction action)
        {
            var p = new GridPortal();
            p.Initialize(name, portalOne, portalTwo, exclusiveTo, action);

            return p;
        }

        internal bool IsUsableBy(AttributeMask requesterAttributes)
        {
            if (_exclusiveTo == AttributeMask.None)
            {
                return true;
            }

            return (_exclusiveTo & requesterAttributes) > 0;
        }

        internal IGrid GetGridFor(PortalCell c)
        {
            if (c == _cellOne)
            {
                return _gridOne;
            }

            return _gridTwo;
        }

        private void Initialize(string name, Bounds portalOne, Bounds portalTwo, AttributeMask exclusiveTo, IPortalAction action)
        {
            _exclusiveTo = exclusiveTo;
            _portalOneBounds = portalOne;
            _portalTwoBounds = portalTwo;

            _cellOne = new PortalCell(this, action);
            _cellTwo = new PortalCell(this, action);

            _gridOne = _cellOne.Initialize(_cellTwo, portalOne);
            _gridTwo = _cellTwo.Initialize(_cellOne, portalTwo);

            if (_gridOne == null || _gridTwo == null)
            {
                throw new ArgumentException("A grid portal has been placed with one or more of its portals outside a grid. Portals must be on a grid.");
            }

            _name = GridManager.instance.RegisterPortal(name, this);
        }
    }
}