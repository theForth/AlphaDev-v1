//------------------------------------------------------------------------------
// <summary>
// The Game manager will hold most of the game's static (global) 
//variables that don't change between rooms/scenes as well as a bunch of other
// public things.
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

public class GlobalGameManager: MonoBehaviour
{
		public int totalEnemiesToSpawn { get; set; }
		public Transform spawnPointPlayer;
		public static int totalSpawnedPokemon{ get; set; }
		public List <GameObject> pokemonSpawned;  //delete if not needed
	
}


