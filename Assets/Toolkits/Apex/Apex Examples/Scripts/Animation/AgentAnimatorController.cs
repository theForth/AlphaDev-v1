namespace Apex.Examples.Animation
{
    using Apex.Steering.Components;
    using UnityEngine;

    /// <summary>
    /// An example animation controller
    /// </summary>
    [AddComponentMenu("Apex/Examples/AgentAnimatorController")]
    public class AgentAnimatorController : MonoBehaviour
    {
        private HumanoidSpeedComponent _speedDef;
        private Animator _animator;
        private int _speedId;
        private HumanoidSpeedComponent.SpeedIndex _lastSpeedIndex;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
            if (_animator == null)
            {
                this.enabled = false;
            }

            _speedDef = this.As<HumanoidSpeedComponent>(true);
            if (_speedDef == null)
            {
                this.enabled = false;
                return;
            }

            _speedId = Animator.StringToHash("SpeedMark");
        }

        private void Update()
        {
            var speedIndex = _speedDef.currentSpeedIndex;
            if (speedIndex != _lastSpeedIndex)
            {
                _lastSpeedIndex = speedIndex;

                _animator.SetInteger(_speedId, (int)speedIndex);
            }
        }
    }
}
