using UnityEngine;
using System.Collections;

public class TempPlaylistController : MonoBehaviour
{


		public AudioClip[] audioClips;
		private int songNumber = 1;
		// Use this for initialization
		void Start ()
		{
				PlaySong (songNumber);
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (Input.GetKeyDown (KeyCode.PageUp)) {

						songNumber++;
						songNumber = songNumber > 3 ? 0 : songNumber;
					
						PlaySong (songNumber);
				
						
				}
				if (Input.GetKeyDown (KeyCode.PageDown)) {
			
						songNumber--;
						songNumber = songNumber < 0 ? 3 : songNumber;
						PlaySong (songNumber);
				}

				
	
		}
		private void PlaySong (int songNumber)
		{
				audio.clip = audioClips [songNumber];
				audio.Play ();
				Debug.Log (audioClips [songNumber].name + " is being played. ");
		}


}
