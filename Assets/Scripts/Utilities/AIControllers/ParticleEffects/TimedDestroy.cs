using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour
{
		private float destroyTime = 0.5f;
		// Use this for initialization
        public void Init(float destroyTime)
        {
            this.destroyTime = destroyTime;
        }
		void Start ()
		{
				Destroy (gameObject, destroyTime);
		}
	
		// Update is called once per frame
	
}
