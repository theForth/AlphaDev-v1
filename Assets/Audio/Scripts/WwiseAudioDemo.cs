using UnityEngine;
using System.Collections;

public class WwiseAudioDemo : MonoBehaviour {

	public string bankName;
	public string eventName;
	float musicVolLevel = 100;
	uint musicVolSlider = 2346531308U;
	float sfxVolLevel = 100;
	uint sfxVolSlider = 988953028U;
	//rtpc control
	bool leftBracket = Input.GetKey(KeyCode.LeftBracket);
	bool rightBracket = Input.GetKey(KeyCode.RightBracket);

	// Use this for initialization
	void Start () {
		uint bankID; // Not used
		//sets initial volume level
		AkSoundEngine.SetRTPCValue (musicVolSlider, musicVolLevel);
		AkSoundEngine.SetRTPCValue (sfxVolSlider, sfxVolLevel);
		//loads the sound bank for a specific level
		AkSoundEngine.LoadBank( bankName, AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID );
		AkSoundEngine.SetSwitch ("AdaptiveDemo", "BaseLoops", gameObject);
	}

	void Update(){

		if(leftBracket){
			musicVolLevel -= 1;
			sfxVolLevel -= 1;
		}
		if(rightBracket){
			musicVolLevel += 1;
			sfxVolLevel += 1;
		}
		AkSoundEngine.SetRTPCValue (musicVolSlider, musicVolLevel);
		AkSoundEngine.SetRTPCValue (sfxVolSlider, sfxVolLevel);
		/*
		 * //footsteps control
		if(Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")){
			AkSoundEngine.PostEvent ("Stop_Footsteps", gameObject);	
			AkSoundEngine.PostEvent ("Play_Footsteps", gameObject);
		}//stops footsteps
		if(Input.GetKey("w")!=true && Input.GetKey("a")!=true && Input.GetKey("s")!=true && Input.GetKey("d")!=true){
			AkSoundEngine.PostEvent ("Stop_Footsteps", gameObject);	
		}
		*/	
	}
	void OnGUI () {
			
		//PokeBall
		GUI.Box(new Rect(10,10,140,150), "PokeBall");	
		//Plays all in a row
		if(GUI.Button(new Rect(20,40,120,20), "Play All")) {
			AkSoundEngine.PostEvent ("Stop_PokeBall", gameObject);
			AkSoundEngine.PostEvent ("Play_PokeBall", gameObject);
		}//Release
		if(GUI.Button(new Rect(20,70,120,20), "Release PokeBall")) {
			AkSoundEngine.PostEvent ("Stop_PokeBall", gameObject);
			AkSoundEngine.PostEvent ("Play_ReleasePokeBall", gameObject);
		}//Throw
		if(GUI.Button(new Rect(20,100,120,20), "Throw PokeBall")) {
			AkSoundEngine.PostEvent ("Stop_PokeBall", gameObject);
			AkSoundEngine.PostEvent ("Play_ThrowPokeBall", gameObject);
		}//Open
		if(GUI.Button(new Rect(20,130,120,20), "Open Pokeball")) {
			AkSoundEngine.PostEvent ("Stop_PokeBall", gameObject);
			AkSoundEngine.PostEvent ("Play_OpenPokeBall", gameObject);
		}


		//music
		GUI.Box(new Rect(150,10,140,210), "Music Menu");		
		//starts music playback and makes sure music doesn't play on top of each other
		if(GUI.Button(new Rect(160,40,120,20), "Start Music")) {
			AkSoundEngine.PostEvent ("Stop_MusicDemo", gameObject);
			AkSoundEngine.PostEvent ("Play_MusicDemo", gameObject);
		}//stops all music
		if(GUI.Button(new Rect(160,70,120,20), "Stop Music")) {
			AkSoundEngine.PostEvent ("Stop_MusicDemo", gameObject);
		}
		if(GUI.Button(new Rect(160,100,120,20), "Indigo Plateau")) {
			AkSoundEngine.PostEvent ("Stop_MusicDemo", gameObject);
			AkSoundEngine.PostEvent ("PLAY_MUSIC_INDIGOPLATEAU", gameObject);
		}
		if(GUI.Button(new Rect(160,130,120,20), "Pewter City")) {
			AkSoundEngine.PostEvent ("Stop_MusicDemo", gameObject);
			AkSoundEngine.PostEvent ("PLAY_MUSIC_PEWTERCITY", gameObject);
		}
		if(GUI.Button(new Rect(160,160,120,20), "Route 1")) {
			AkSoundEngine.PostEvent ("Stop_MusicDemo", gameObject);
			AkSoundEngine.PostEvent ("PLAY_MUSIC_ROUTE1", gameObject);
		}
		if(GUI.Button(new Rect(160,190,120,20), "Hero Theme")) {
			AkSoundEngine.PostEvent ("Stop_MusicDemo", gameObject);
			AkSoundEngine.PostEvent ("PLAY_MUSIC_HEROTHEME", gameObject);
		}

		GUI.Box(new Rect(150,220,140,180), "Adaptive Menu");		
		//starts music playback and makes sure music doesn't play on top of each other
		if(GUI.Button(new Rect(160,250,120,20), "Start Music")) {
			AkSoundEngine.PostEvent ("Stop_AdaptiveDemoSwitch", gameObject);
			AkSoundEngine.PostEvent ("Play_AdaptiveDemoSwitch", gameObject);
		}//stops all music
		if(GUI.Button(new Rect(160,280,120,20), "Base Music")) {
			AkSoundEngine.SetSwitch ("AdaptiveDemo", "BaseLoops", gameObject);
		}
		if(GUI.Button(new Rect(160,310,120,20), "Battle Loops")) {
			AkSoundEngine.SetSwitch ("AdaptiveDemo", "BattleLoops", gameObject);
		}
		if(GUI.Button(new Rect(160,340,120,20), "Explore Loops")) {
			AkSoundEngine.SetSwitch ("AdaptiveDemo", "ExploreLoops", gameObject);
		}
		if(GUI.Button(new Rect(160,370,120,20), "Stop Music")) {
			AkSoundEngine.PostEvent ("Stop_AdaptiveDemoSwitch", gameObject);
		}


		GUI.Box(new Rect(290,10,140,120), "Volume Music");
		if(GUI.Button(new Rect(300,40,120,20), "+")) {
			musicVolLevel += 10;
			AkSoundEngine.SetRTPCValue (musicVolSlider, musicVolLevel);
		}
		if(GUI.Button(new Rect(300,70,120,20), "-")) {
			musicVolLevel -= 10;
			AkSoundEngine.SetRTPCValue (musicVolSlider, musicVolLevel);
		}

		GUI.Box(new Rect(430,10,140,120), "Volume SFX");
		if(GUI.Button(new Rect(440,40,120,20), "+")) {
			sfxVolLevel += 10;
			AkSoundEngine.SetRTPCValue (sfxVolSlider, sfxVolLevel);
		}
		if(GUI.Button(new Rect(440,70,120,20), "-")) {
			sfxVolLevel -= 10;
			AkSoundEngine.SetRTPCValue (sfxVolSlider, sfxVolLevel);
		}
	}
}
