using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour {
	float runSpeed = 2;
	public bool run = false;
	Animator animator;
	
	void OnAnimatorMove() {
		//Animator animator = GetComponent<Animator> ();
		animator = Player.trainer.GetComponent<Animator> ();
		if (animator) {
			Vector3 newPosition = animator.deltaPosition;
			if (Rebind.GetInput ("SPRINT") || run) {
				newPosition *= runSpeed;
			}
			//rigidbody.position += newPosition;
			rigidbody.position+=newPosition;
			
			if (Rebind.GetInput ("ACTION_ROLL")) {
				StartCoroutine(Dive());
			}
		}
	}

	IEnumerator Dive() {
		animator.SetBool ("dive", true);
		yield return new WaitForSeconds(2.27f); // wait for two seconds.
		animator.SetBool ("dive", false);
	}
}