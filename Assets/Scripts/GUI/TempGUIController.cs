using UnityEngine;
using System.Collections;

public class TempGUIController : MonoBehaviour
{
		//TODO this will be replaced by menu states.

		public GameObject chatWindow;

		public bool toggleChatWindow = false;
		private UIPanel chatWindowPanel ;
		// Use this for initialization
		void Start ()
		{
				chatWindowPanel = chatWindow.GetComponent<UIPanel> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				if (Input.GetKeyDown (KeyCode.C)) {
						toggleChatWindow = !toggleChatWindow;
				}

				if (toggleChatWindow == true && chatWindow.activeSelf == true)
						FadeScreenAway ();
				if (toggleChatWindow == false && chatWindowPanel.alpha < 0.90)					
						FadeScreenInto ();
				

		}
		private void FadeScreenAway ()
		{
				chatWindowPanel.alpha = Mathf.Lerp (chatWindowPanel.alpha, 0f, 5 * Time.deltaTime);
				if (chatWindowPanel.alpha < 0.1)
						chatWindow.SetActive (false);
		}
		private void FadeScreenInto ()
		{
				chatWindow.SetActive (true);
				chatWindowPanel.alpha = Mathf.Lerp (chatWindowPanel.alpha, 1f, 5 * Time.deltaTime);
			
						
		}
}