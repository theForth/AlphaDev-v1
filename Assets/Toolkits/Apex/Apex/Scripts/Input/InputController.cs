﻿/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Input
{
    using Apex.DataStructures;
    using Apex.PathFinding;
    using Apex.Services;
    using Apex.Steering;
    using Apex.Units;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// The input controller exposes all Apex related actions that can be taken through some form of input.
    /// </summary>
    public partial class InputController
    {
        /// <summary>
        /// Selects units inside the bounding rectangle defined by two opposing corners.
        /// </summary>
        /// <param name="startScreen">The bounding rectangle start point in screen coordinates.</param>
        /// <param name="endScreen">The bounding rectangle end point in screen coordinates.</param>
        /// <param name="append">if set to <c>true</c> the selection will append to any currently selected units.</param>
        public void SelectUnitRange(Vector3 startScreen, Vector3 endScreen, bool append)
        {
            var boundingPoly = GetBoundingPoly(startScreen, endScreen);

            GameServices.gameStateManager.unitSelection.SelectUnitsIn(boundingPoly, append);
        }

        /// <summary>
        /// Tentatively selects units inside the bounding rectangle defined by two opposing corners.
        /// What this means is that the units are not really selected, they only appear to be graphically, to do the actual selection a call to <see cref="SelectUnitRange"/> must be made.
        /// </summary>
        /// <param name="startScreen">The start screen.</param>
        /// <param name="endScreen">The end screen.</param>
        /// <param name="append">if set to <c>true</c> the selection will append to any currently selected units.</param>
        public void SelectUnitRangeTentative(Vector3 startScreen, Vector3 endScreen, bool append)
        {
            var boundingPoly = GetBoundingPoly(startScreen, endScreen);

            GameServices.gameStateManager.unitSelection.SelectUnitsAsPendingIn(boundingPoly, append);
        }

        /// <summary>
        /// Selects the unit at the specified screen position.
        /// </summary>
        /// <param name="screenPos">The screen position.</param>
        /// <param name="append">if set to <c>true</c> the selection will append to any currently selected units.</param>
        public void SelectUnit(Vector3 screenPos, bool append)
        {
            ISelectable selectable = null;
            var collider = UnityServices.mainCamera.GetColliderAtPosition(screenPos, Layers.units);
            if (collider != null)
            {
                selectable = collider.As<ISelectable>();
            }

            if (selectable != null)
            {
                GameServices.gameStateManager.unitSelection.ToggleSelected(selectable, append);
            }
            else if (!append)
            {
                GameServices.gameStateManager.unitSelection.DeselectAll();
            }
        }

        /// <summary>
        /// Selects the unit with the specified index.
        /// </summary>
        /// <param name="unitIndex">Index of the unit.</param>
        /// <param name="toggle">if set to <c>true</c> this will toggle the selection on and off.</param>
        public void SelectUnit(int unitIndex, bool toggle)
        {
            GameServices.gameStateManager.unitSelection.Select(unitIndex, toggle);
        }

        /// <summary>
        /// Selects the group with the specified index.
        /// </summary>
        /// <param name="groupIndex">Index of the group.</param>
        public void SelectGroup(int groupIndex)
        {
            GameServices.gameStateManager.unitSelection.SelectGroup(groupIndex);
        }

        /// <summary>
        /// Assigns the currently selected units to a group.
        /// </summary>
        /// <param name="groupIndex">Index of the group.</param>
        public void AssignGroup(int groupIndex)
        {
            GameServices.gameStateManager.unitSelection.AssignGroup(groupIndex);
        }

        /// <summary>
        /// Sets the destination of all selected units.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="append">if set to <c>true</c> the destination will be appended to any existing destinations, i.e way points.</param>
        public virtual void SetDestination(Vector3 destination, bool append)
        {
            RaycastHit hit;
            if (UnityServices.mainCamera.ScreenToLayerHit(destination, Layers.terrain, 1000.0f, out hit))
            {
                var destinationBlock = hit.collider.GetComponent<InvalidDestinationComponent>();
                if (destinationBlock != null)
                {
                    if (destinationBlock.entireTransform)
                    {
                        return;
                    }

                    if (destinationBlock.onlySubArea.Contains(hit.point))
                    {
                        return;
                    }
                }

                foreach (var unit in GameServices.gameStateManager.unitSelection.selected)
                {
                    var steerable = unit.As<ISteerable>();
                    if (steerable != null)
                    {
                        steerable.MoveTo(hit.point, append);
                    }
                }
            }
        }

        private PolygonXZ GetBoundingPoly(Vector3 startScreen, Vector3 endScreen)
        {
            var cam = UnityServices.mainCamera;

            var c1 = cam.ScreenToGroundPoint(startScreen);
            var c2 = cam.ScreenToGroundPoint(new Vector3(endScreen.x, startScreen.y, startScreen.z));
            var c3 = cam.ScreenToGroundPoint(endScreen);
            var c4 = cam.ScreenToGroundPoint(new Vector3(startScreen.x, endScreen.y, endScreen.z));

            return new PolygonXZ(c1, c2, c3, c4);
        }
    }
}