﻿using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour
{
		public float destroyTime = 0.5f;
		// Use this for initialization
		void Start ()
		{
				Destroy (gameObject, destroyTime);
		}
	
		// Update is called once per frame
	
}