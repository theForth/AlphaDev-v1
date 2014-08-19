namespace Apex.Examples.Extensibility
{
    using Apex.Steering;
    using UnityEngine;

    /// <summary>
    /// An exampåle speed component
    /// </summary>
    [AddComponentMenu("Apex/Examples/Extensibility/Car Speed Component")]
    public class CarSpeedComponent : MonoBehaviour, IDefineSpeed
    {
        /// <summary>
        /// The minimum speed ever, regardless of movement form. Any speed below this will mean a stop.
        /// </summary>
        public float minimumSpeedThreshold = 0.5f;

        /// <summary>
        /// The maximum speed of the car.
        /// </summary>
        public float maximumSpeed;

        private float _currentSpeed;

        /// <summary>
        /// Gets the minimum speed of the unit.
        /// </summary>
        /// <value>
        /// The minimum speed.
        /// </value>
        float IDefineSpeed.minimumSpeed
        {
            get { return this.minimumSpeedThreshold; }
        }

        /// <summary>
        /// Gets the maximum speed of the unit.
        /// </summary>
        /// <value>
        /// The maximum speed.
        /// </value>
        float IDefineSpeed.maximumSpeed
        {
            get { return this.maximumSpeed; }
        }

        /// <summary>
        /// Signal that the unit has stopped.
        /// </summary>
        public void SignalStop()
        {
            //Can be used to change state in relation to animators and such as well
            _currentSpeed = 0.0f;
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
            //You could use the currentMovementDirection to adjust the speed in accordance with the direction the car is facing,
            //e.g. if the specified movement direction is perpendicular to its current facing direction you probably do not want to move at all, but wait until it has turned,
            //otherwise the car would move sideways.
            return _currentSpeed;
        }

        //This is a custom method for this speed component
        public void Accelerate(float toSpeed, float accelerationRate)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, toSpeed, accelerationRate);
        }

        //This is a custom method for this speed component
        public void Decelerate(float toSpeed, float decelerationRate)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, toSpeed, decelerationRate);
        }
    }
}
