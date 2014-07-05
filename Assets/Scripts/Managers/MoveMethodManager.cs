using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class MoveMethodManager : MonoBehaviour
    {
    public static Dictionary<int, Action<Transform, PokemonObj>> MoveMethods = new Dictionary<int, Action<Transform, PokemonObj>>();


    void OnAwake()
    {
     
    }
   void Start()
    {
        MoveMethods[85] = CastThunder;
        MoveMethods[45] = Growl;
    }
  
    public void CastThunder(Transform transform, PokemonObj pokemonObj)
    {
        Debug.Log("Casting " + pokemonObj.moves[1].Name + ' ' + transform.name);
    }
    public void Growl(Transform transform, PokemonObj pokemonObj)
    {
        Debug.Log("Casting " + pokemonObj.moves[1].Name + ' ' + transform.name);
    }
    }

