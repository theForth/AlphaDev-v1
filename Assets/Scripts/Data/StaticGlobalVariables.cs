using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StaticGlobalVariables
{
    public static string UserName;
    public static string sessionhash;
    public static string pokeJSON;
    public static int GTID;
    public static int trainerLevel;
    public static List<simplePokemon> dbPokemon;
    public static List<MoveData> moveData;
    public static List<PokeData> pokeData;
    public static Location playerLocation;

}

public class simplePokemon
{
    public int Id;
    public int Level;
    public List<int> Moves;
}



//public class PacketHeader{
//public string PTYPE;
//}

public class PACKETPLUD
{
    public int GTID;
    public Location LOC;
}

public class Location
{
    public string ZONE;
    public double X;
    public double Y;
    public double Z;
    public double P; //Pitch
    public double Ya; //Yaw
    public double R; //Roll
}
