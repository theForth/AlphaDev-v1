namespace Apex.Examples.Misc
{
    using Apex.Steering;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// A point that draws a lot of attention.
    /// </summary>
    [AddComponentMenu("Apex/Examples/Fixation Point")]
    public class FixationPoint : MonoBehaviour, IProvideFacingOrientation
    {
        /// <summary>
        /// The fixation point
        /// </summary>
        public Transform fixationPoint;

        /// <summary>
        /// Gets the orientation this component would like to face.
        /// </summary>
        /// <returns>
        /// The facing orientation
        /// </returns>
        public FacingOrientation GetOrientation()
        {
            if (fixationPoint == null)
            {
                return null;
            }

            return new FacingOrientation
            {
                orientation = (fixationPoint.position - this.transform.position),
                priority = 100,
                turnSpeed = 3.0f
            };
        }

        private void Start()
        {
            //Sign up as a provider of facing orientation
            var turner = this.As<IControlFacingOrientation>();
            if (turner != null)
            {
                turner.RegisterOrientationProvider(this);
            }
        }
    }
}
