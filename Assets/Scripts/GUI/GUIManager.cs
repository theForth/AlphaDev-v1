using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GUIManager : MonoBehaviour {


    public static bool FloatingHealthBarActive;
	// Use this for initialization
	void Start () {

        StartCoroutine(KeyListener());
	
	}
	
	// Update is called once per frame
	void Update () {
	

        
	}

    IEnumerator KeyListener()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                FloatingHealthBarActive = !FloatingHealthBarActive;
                Debug.Log("ToggleSuccessFull");
                    
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
