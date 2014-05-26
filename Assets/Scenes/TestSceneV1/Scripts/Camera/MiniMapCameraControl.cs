using UnityEngine;
using System.Collections;

public class MiniMapCameraControl : MonoBehaviour
{

		// Use this for initialization
		public Transform target;
		public float rotationX;
		public float ax = 0;
		public float az = 0;
	 
		void Start ()
		{
				target = GameObject.FindGameObjectWithTag ("Player").transform;
				//target.localEulerAngles = v3Rotate;
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{

				transform.position = new Vector3 (target.position.x, 15f, target.position.z);

		}
	

}
