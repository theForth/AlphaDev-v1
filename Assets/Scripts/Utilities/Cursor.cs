using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{

		public Vector2 mouse;
		public int width = 32;
		public int height = 32;
		private Texture2D cursor ;
	
		void Start ()
		{
				Screen.lockCursor = false;
				Screen.showCursor = false;
				cursor = Resources.Load ("Pokeball", typeof(Texture2D)) as Texture2D;
				cursor.SetPixel (0, 0, new Color (1, 1, 1, 0.1f));
				cursor.Apply ();
				
			
				//renderer.material.mainTexture = cursor;
		}
	
		void Update ()
		{
				mouse = new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		}
	
		void OnGUI ()
		{
				GUI.DrawTexture (new Rect (mouse.x - (width / 2), mouse.y - (height / 2), width, height), cursor);
		}
}
