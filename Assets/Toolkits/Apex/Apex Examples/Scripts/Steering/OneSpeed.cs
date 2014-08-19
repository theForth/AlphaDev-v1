namespace Apex.Examples.Steering
{
    using Apex.Steering;
    using UnityEngine;

    /// <summary>
    /// An example speed controller
    /// </summary>
    [AddComponentMenu("Apex/Examples/One Speed")]
    public class OneSpeed : MonoBehaviour, IDefineSpeed
    {
        /// <summary>
        /// The one and only speed
        /// </summary>
        public float speed;

        /// <summary>
        /// Gets the minimum speed of the unit.
        /// </summary>
        /// <value>
        /// The minimum speed.
        /// </value>
        public float minimumSpeed
        {
            get { return speed / 2.0f; }
        }

        /// <summary>
        /// Gets the maximum speed of the unit.
        /// </summary>
        /// <value>
        /// The maximum speed.
        /// </value>
        public float maximumSpeed
        {
            get { return speed; }
        }

        /// <summary>
        /// Signal that the unit has stopped.
        /// </summary>
        public void SignalStop()
        {
        }

        /// <summary>
        /// Gets the preferred speed of the unit.
        /// </summary>
        /// <param name="currentMovementDirection">The current movement direction.</param>
        /// <returns>
        /// The preferred speed
        /// </returns>
        public float GetPreferredSpeed(Vector3 currentMovementDirection)
        {
            return speed;
        }
    }
}
