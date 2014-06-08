using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement.AstarPathfindingProject.AIPath
{
		[TaskDescription("Flee the target ")]
		[TaskCategory("Movement")]

		public class Flee : Action
		{
				[Tooltip("The speed of the agent")]
				public SharedFloat
						speed;
				[Tooltip("Turn speed of the agent")]
				public SharedFloat
						angularSpeed;
				[Tooltip("The transform that the agent is moving towards")]
				public SharedTransform
						targetTransform;
				[Tooltip("If target is null then use the target position")]
				public SharedVector3
						targetPosition;
				[Tooltip("The distance to look ahead when fleeing")]
				public float
						lookAheadDistance = 20;
				[Tooltip("The agent has fleed when the square magnitude is greater than this value")]
				public float
						fleedDistance = 10000;
		
				// True if the target is a transform
				private bool dynamicTarget;
				// A cache of the AIPath
				private AIPathAgent aiPathAgent;
		
				public override void OnAwake ()
				{
						// cache for quick lookup
						aiPathAgent = gameObject.GetComponent<AIPathAgent> ();
				}
		
				public override void OnStart ()
				{
						// the target is dynamic if the target transform is not null and has a valid
						dynamicTarget = (targetTransform != null && targetTransform.Value != null);
			
						// set the speed, angular speed, and destination then enable the agent
						aiPathAgent.speed = speed.Value;
						aiPathAgent.turningSpeed = angularSpeed.Value;
						aiPathAgent.target.parent = null;
						aiPathAgent.target.position = Target ();
						aiPathAgent.enabled = true;
				}
		
				// Flee from the target. Return success once the agent has fleed the target by moving far enough away from it
				// Return running if the agent is still fleeing
				public override TaskStatus OnUpdate ()
				{
						// Update the target position if the target is a transform because that agent could move
						if (dynamicTarget) {
								aiPathAgent.target.position = Target ();
						}
						return (aiPathAgent.PathCalculated () && Vector3.SqrMagnitude (transform.position - TargetPosition ()) > fleedDistance) ? TaskStatus.Success : TaskStatus.Running;
				}
		
				// Return targetPosition if targetTransform is null
				private Vector3 TargetPosition ()
				{
						if (dynamicTarget) {
								return targetTransform.Value.position;
						} 
						return targetPosition.Value;
				}
		
				// Flee in the opposite direction
				private Vector3 Target ()
				{
						return transform.position + (transform.position - TargetPosition ()).normalized * lookAheadDistance;
				}
		
				public override void OnEnd ()
				{
						// Disable the AIPath
						aiPathAgent.enabled = false;
				}
		
				// Reset the public variables
				public override void OnReset ()
				{
						if (speed != null) {
								speed.Value = 0;
						}
						if (angularSpeed != null) {
								angularSpeed.Value = 0;
						}
						if (targetTransform != null) {
								targetTransform.Value = null;
						}
						if (targetPosition != null) {
								targetPosition.Value = Vector3.zero;
						}
						lookAheadDistance = 5;
						fleedDistance = 20;
				}
		}
}