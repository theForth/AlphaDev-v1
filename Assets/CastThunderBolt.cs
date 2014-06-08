using UnityEngine;
using System.Collections;

public class CastThunderBolt : MonoBehaviour
{
		
		public GameObject thunderBoltprefab;
		//private GameObject tempCursor;
		private float cooldown;
		private RaycastHit hit;	
		// Use this for initialization
		void Start ()
		{
		
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (cooldown > 0) {
						cooldown -= Time.deltaTime;
				}
				var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
						// Create a prefab if hit something
			
						//tempCursor.transform.position = hit.point;
			
			
						if (Input.GetMouseButton (0) && cooldown <= 0) {
								GameObject thunderBolt = (GameObject)Instantiate (thunderBoltprefab, hit.point, Quaternion.identity);
								thunderBolt.transform.position += new Vector3 (0, 1.5f, 0);
								thunderBolt.AddComponent<TimedDestroy> ();
								Debug.Log ("Casting " + thunderBolt.name);
								cooldown = 0.15f;
								
						}
				}
				
		}
}
