using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{


		public Transform playerTransform;
		public Transform pokemonTransform;
		public bool isPokemonTarget = false;
		public Transform target;
		public Transform newTarget;
		public GameObject SmoothCamera ;
		void Start ()
		{
				SmoothCamera = new GameObject ("MadefromCode");
				playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
				pokemonTransform = GameObject.FindGameObjectWithTag ("pokemon").transform;
				//target = Camera.main.GetComponent<ThirdPersonCameraControl> ().target;
		}
		
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.A)) {
						isPokemonTarget = true;
						//Camera.main.GetComponent<ThirdPersonCameraControl> ().target = SmoothCamera.transform;
						//newTarget.position = Vector3.Lerp (pokemonTransform.position, playerTransform.position, Time.deltaTime * 0.01f);
					
						//transitionCamera.transform.position = positionCurrent.transform.position;
				}
			
		}
		
		void LateUpdate ()
		{
				if (isPokemonTarget) {
						// Camera.main.GetComponent<ThirdPersonCameraControl> ()._transform = Vector3.Lerp (pokemonTransform.position, playerTransform.position, Time.deltaTime * 0.01f);
				}
		
		}
		

}
