/*
 * 
 * 
 *
 * The high Hierarchy of state management
 *
 */
using System;
using UnityEngine;
using System.Collections.Generic;

public class GlobalGameManager: MonoBehaviour
{
		public int totalEnemiesToSpawn { get; set; }
		public Transform spawnPointPlayer;
		public static int totalSpawnedPokemon{ get; set; }
		public List <GameObject> pokemonSpawned;  //delete if not needed
		public static PlayerControlState playerControlState ;
	 
	
}


