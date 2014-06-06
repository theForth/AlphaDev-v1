using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Movement.AstarPathfindingProject.AIPath
{
		public class Wander : Action
		{
				
				public SharedFloat    //speed of gameObject
						speed;
				
				public SharedFloat		//turn Speed
						angularSpeed;
			
				public float					//how far should it wander from the starter point
						wanderDistance = 20;
			
				public float
						wanderRate = 2;

				// A cache of the AIPath
				private AIPathAgent aiPathAgent;
				public GameObject WanderTarget;
		
				public override void OnAwake ()
				{
						// cache for quick lookup
						aiPathAgent = gameObject.GetComponent<AIPathAgent> ();
						WanderTarget = new GameObject ("WanderTarget");
						WanderTarget.transform.parent = transform;
						aiPathAgent.target = WanderTarget.transform;
				}

				public override void OnStart ()
				{
						// set the speed, angular speed, and destination then enable the agent
						aiPathAgent.speed = speed.Value;
						aiPathAgent.turningSpeed = angularSpeed.Value;
						aiPathAgent.target.parent = null;
						aiPathAgent.target.position = Target ();
						aiPathAgent.enabled = true;
				}

				// There is no success or fail state with wander - the agent will just keep wandering
				public override TaskStatus OnUpdate ()
				{
						aiPathAgent.target.position = Target ();
						return TaskStatus.Running;
				}

				// Return targetPosition if targetTransform is null
				private Vector3 Target ()
				{		// TODO need to add logic for only finding targets that are within a proxmity from spawnPoint.
						// TODO use couroutines to reduce constant new targets. Should match the pathing read of the agent
						// point in a new random direction and then multiply that by the wander distance
						var direction = transform.forward + Random.insideUnitSphere * wanderRate;
						return transform.position + direction.normalized * wanderDistance;
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
						wanderDistance = 20;
						wanderRate = 2;
				}
		}
}