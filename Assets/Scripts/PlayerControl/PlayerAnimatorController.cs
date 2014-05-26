using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour
{
		public GameObject model;
	
		private bool _active = true;
		private string _action = string.Empty;
		private string _animation = string.Empty;
	
		public string Action {
				get { return _action; }
				set { _action = value; }
		}
	
		void Start ()
		{
				// Check to make sure the model is selected and has animation
				if (!model) {
						Debug.LogWarning ("SimpleRpgAnimator: No model selected");
						_active = false;
				} else {
						if (!model.animation) {
								Debug.LogWarning ("SimpleRpgAnimator: Selected model has no animation");
								_active = false;
						}
				}
		}
	
		void Update ()
		{
				if (_active) {
						// CrossFade the animation to match the action
						if (_animation != _action) {
								_animation = _action;
								model.animation.CrossFade (_animation);
						}
				}
		}
	
		public void SetSpeed (float n)
		{
				if (_active) {
						// Set the current animation's speed
						if (model.animation [_animation]) {
								if (model.animation [_animation].speed != n) {
										model.animation [_animation].speed = n;
								}
						}
				}
		}
}