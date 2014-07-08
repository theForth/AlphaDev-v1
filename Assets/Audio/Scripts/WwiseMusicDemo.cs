using UnityEngine;
using System.Collections.Generic;
using System;

public class WwiseMusicDemo : MonoBehaviour {

	public string bankName;
	public string eventName;
	float volLevel = 100;
	// Use this for initialization
	void Start () {
		uint bankID; // Not used

		//sets initial volume level
		AkSoundEngine.SetRTPCValue (2346531308U, volLevel);
		//loads the sound bank for a specific level
		AkSoundEngine.LoadBank( bankName, AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID );
		//plays specific events within the currently loaded soundbank.
		AkSoundEngine.PostEvent (eventName, gameObject);
	}

	private void Update()
	{
		//in this instance, this real-time parameter control(rtpc) controls the volume level of audio playback
		if(Input.GetKey(KeyCode.LeftBracket)){
			volLevel -= 1;
		}
		if(Input.GetKey(KeyCode.RightBracket)){
			volLevel += 1;
		}
		AkSoundEngine.SetRTPCValue (2346531308U, volLevel);
	}
}