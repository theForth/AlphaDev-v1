using UnityEngine;
using System.Collections;

public class GUICamera : MonoBehaviour
{
		public float targetOffsetX = 0;
		public float targetOffsetY = 0.05f;
		public float targetOffsetZ = 0.5f;
		
		// Use this for initialization
		public Transform target;
		void Start ()
		{
				target = transform.parent.gameObject.transform;
				transform.position = target.position + new Vector3 (targetOffsetX, targetOffsetY, targetOffsetZ);
				transform.Rotate (0, 180, 0);
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
}
