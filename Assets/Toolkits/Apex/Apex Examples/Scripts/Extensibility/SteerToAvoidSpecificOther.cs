namespace Apex.Examples.Extensibility
{
    using Apex.Steering;
    using UnityEngine;

    [AddComponentMenu("Apex/Examples/Extensibility/Steer To Avoid Specific Other")]
    public class SteerToAvoidSpecificOther : SteeringComponent
    {
        public SteerableUnitComponent other;
        public float repulsionRange = 10.0f;

        private float _repulsionSquared;

        protected override void Awake()
        {
            base.Awake();

            _repulsionSquared = this.repulsionRange * this.repulsionRange;
        }

        protected override Vector3 GetMovementVector(Vector3 currentVelocity)
        {
            if (other.Equals(null))
            {
                return Vector3.zero;
            }

            var diff = (this.transformCached.position - other.transform.position);
            if (diff.sqrMagnitude > _repulsionSquared)
            {
                return Vector3.zero;
            }

            return diff * ((this.repulsionRange - diff.magnitude) / this.repulsionRange);
        }
    }
}
