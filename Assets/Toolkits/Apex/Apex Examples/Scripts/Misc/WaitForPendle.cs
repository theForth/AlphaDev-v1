namespace Apex.Examples.Misc
{
    using Apex.Steering.Components;
    using UnityEngine;

    /// <summary>
    /// An example behavior that makes units wait for an <see cref="ObjectPendler"/>
    /// </summary>
    [AddComponentMenu("Apex/Examples/Wait for Object Pendler")]
    public class WaitForPendle : MonoBehaviour
    {
        /// <summary>
        /// The pendler
        /// </summary>
        public ObjectPendler pendler;

        /// <summary>
        /// The position this moves the pendler to
        /// </summary>
        public ObjectPendler.Position movesTo;

        /// <summary>
        /// Whether this is an on-board (the pendler) trigger
        /// </summary>
        public bool isOnboardTrigger;

        private bool _moveOnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (pendler == null)
            {
                return;
            }

            if (pendler.IsAtPosition(this.movesTo))
            {
                _moveOnExit = false;
                return;
            }

            var steerer = other.GetComponent<SteerForPathComponent>();
            if (steerer == null)
            {
                return;
            }

            if (this.isOnboardTrigger)
            {
                _moveOnExit = true;
                steerer.DisableMovementOrders();
            }
            else
            {
                StartPendle(steerer);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (this.isOnboardTrigger)
            {
                var steerer = other.GetComponent<SteerForPathComponent>();
                if (steerer == null)
                {
                    return;
                }

                if (_moveOnExit)
                {
                    StartPendle(steerer);
                }
                else
                {
                    steerer.EnableMovementOrders();
                }
            }
        }

        private void StartPendle(SteerForPathComponent steerer)
        {
            steerer.DisableMovementOrders();
            steerer.Wait(null);

            var curParent = steerer.transform.parent;

            if (this.isOnboardTrigger)
            {
                steerer.rigidbody.isKinematic = true;
                steerer.transform.parent = pendler.transform;
            }

            pendler.MoveTo(
                this.movesTo,
                () =>
                {
                    if (this.isOnboardTrigger)
                    {
                        steerer.transform.parent = curParent;
                        steerer.rigidbody.isKinematic = false;
                    }
                    else
                    {
                        steerer.EnableMovementOrders();
                    }

                    steerer.Resume();
                });
        }
    }
}
