using UnityEngine;
using System.Collections;

public class MenuSceneNGUI : MonoBehaviour {
	float startTime = 0;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;
		if (!GameObject.Find("MusicBox").audio.isPlaying){
			GameObject.Find("MusicBox").audio.Play();
		}
		
	}
}
