using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class MoveMethodManager : MonoBehaviour
    {
    public static Dictionary<int, Action<Transform, PokeBattler>> MoveMethods = new Dictionary<int, Action<Transform, PokeBattler>>();
    public LayerMask collisionLayers = new LayerMask();

    void OnAwake()
    {
     
    }
   void Start()
    {
       // MoveMethods[85] = CastThunder;
       // MoveMethods[45] = Growl;
    }
  

    }

