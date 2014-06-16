using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StaticGlobalVariables 
{
	public static string UserName;
	public static string sessionhash;
	public static string pokeJSON;
	public static int trainerLevel;
	public static List<simplePokemon> dbPokemon;
	public static List<MoveData> moveData;
	public static List<PokeData> pokeData;
		
}

public  class simplePokemon{
	public  int Id;
	public  int Level;
	public  List<int> Moves;
}

