namespace Apex.Examples.Extensibility
{
    using Apex.Steering;
    using UnityEngine;

    /// <summary>
    /// Example of an <see cref="IMoveUnits"/> implementation.
    /// </summary>
    [AddComponentMenu("Apex/Examples/Extensibility/Character Controller Mover")]
    public class CharacterControllerMover : MonoBehaviour, IMoveUnits
    {
        private CharacterController _controller;

        public void Move(Vector3 velocity, float deltaTime)
        {
            _controller.Move(velocity * deltaTime);
        }

        public void Stop()
        {
            /* NOOP */
        }

        private void Awake()
        {
            _controller = this.GetComponent<CharacterController>();
        }
    }
}
