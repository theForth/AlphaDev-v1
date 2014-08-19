using UnityEngine;
using System;

[System.Serializable]
public class MoveAssetData
{
    public int ID;
    public string Name;
    public Texture Icon;
    public string Description;
    public AudioClip audioClip;
    public int PokemonID;
    public GameObject VFXPrefab;
    public GameObject MoveUI;
 
}