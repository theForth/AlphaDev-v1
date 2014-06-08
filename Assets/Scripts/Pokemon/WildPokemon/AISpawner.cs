using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/// <summary>

/// </summary>
public class AISpawner : MonoBehaviour
{
		public float[] spawnProbabilty;
		public int maximumSpawnerCount = 5;
		public float radius = 5f;
		public GameObject[] enemyPokemonToSpawn;
		//public List<GameObject> spawned = new List<GameObject> ();
		GlobalGameManager _globalGameManager;
		private DiscreteDistribution discreteDistribution;
		void Awake ()
		{
		
				discreteDistribution = new DiscreteDistribution (spawnProbabilty);
				StartCoroutine (DisplayProbabilites ());
				
		}
		IEnumerator DisplayProbabilites ()
		{
				
				for (int i=0; i < maximumSpawnerCount; i++) {
						GameObject prefab = (GameObject)enemyPokemonToSpawn [discreteDistribution.Sample ()];
						GameObject newPokemon = (GameObject)Instantiate (prefab);
						newPokemon.transform.position = transform.position 
								+ Quaternion.Euler (0, Random.value * 360, 0) * Vector3.right * radius * Mathf.Sqrt (Random.value);
						RaycastHit hit;
						//if (Physics.Raycast (newPokemon.transform.position, Vector3.down, out hit)) {
						//newPokemon.transform.position = hit.point;
						//}
						Debug.Log ("PokemonSpawned: " + newPokemon.name);
						//_globalGameManager.pokemonSpawned.Add (newPokemon);
						yield return 0;
				}
		}
		IEnumerator Wait (float duration)
		{
				for (float timer = 0; timer < duration; timer += Time.deltaTime)
						yield return 0;
		}	
		void Update ()
		{
		
		}
}