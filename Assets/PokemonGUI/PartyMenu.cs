using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyMenu : MonoBehaviour
{
		public static Pokemon pokemon;
		Rect rect;
		static bool click = false;
		static bool doPopup = false;
		public GUIStyle label;
		List<Rect> rectList;
		List<Pokemon> pokemonList;
		public static float mouseX;
		public static float mouseY;
	
		void Start ()
		{
				GUImgr.Start ();
				label = new GUIStyle ();
				label.fontSize = 12;
				label.fontStyle = FontStyle.Bold;
				rectList = new List<Rect> ();
				pokemonList = new List<Pokemon> ();
		}
	
		//This should handle keys and clicking
		void OnGUI ()
		{
				DrawMenu ();
				if (click && (Input.GetKeyDown (KeyCode.Escape) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1))) {
						doPopup = false;
						click = false;
				}

				if (Input.GetMouseButtonDown (1) && !doPopup) { // when button clicked...
						click = true;
						Vector2 mousePos = Event.current.mousePosition;
						mouseX = mousePos.x;
						mouseY = mousePos.y;

						/* This iterates through Rect list and checks to see if the mouse is cotained in one of them */
						for (int x = 0; x<rectList.Count; x++) {
								if (rectList [x].Contains (mousePos)) {
										pokemon = pokemonList [x];
										doPopup = true;
										break;
								}
						}
				}

				if (doPopup) {
						Popup (pokemon);
				}
		}

		/* Popup
	 * Pops up menu containing pokemon info and actions
	 * 
	 * Why?
	 * I'm thinking we put option to view stats here
	 * Option to release/recall Pokemon here
	 * Option to use items on Pokemon quickly
	 * 
	 * TO-DO
	 * Menu options are not selectable
	 * Menu lacks useful features!
	 * 
	 */
		void Popup (Pokemon pokemon)
		{
				GUI.depth = 0;
				GUI.DrawTexture (new Rect (mouseX, mouseY, 100, 100), GUImgr.gradRight);
				GUI.Label (new Rect ((mouseX + 5), (mouseY + 5), 40, 25), pokemon.name, label);
				GUI.Label (new Rect ((mouseX + 5), (mouseY + 15), 40, 25), "menu will go here", label);
		}
	
		/* 
	 * DrawMenu draws a pokemon icon, level, hp, xp, pp on screen
	 * Also creates temporary lists containing a Rect[angle] and Pokemon info for that party menu slot
	 * When user right clicks, the tmp list containing Rect info is checked
	 * If user has clicked in one of those Rect's, then the list of Pokemon info is queried
	 * We should put this in a draggable window!
	*/
		void DrawMenu ()
		{
				rectList.Clear ();
				pokemonList.Clear ();
				var party = Player.trainer.party;
				float yPos = 0;
				int x = 0;
				foreach (var slot in Player.trainer.party.GetSlots()) {
						var thisPokemon = slot.pokemon;
						GUI.DrawTexture (new Rect (0, yPos, 72, 70), GUImgr.statOk);
						if (thisPokemon.icon != null) {
								GUI.DrawTexture (new Rect (0, yPos, 48, 48), thisPokemon.icon);
						}
						GUI.Label (new Rect (36, yPos, 40, 25), "Lv: " + thisPokemon.level, label);
						GUImgr.DrawBar (new Rect (0, yPos + 53, 48, 4), thisPokemon.xp, GUImgr.xp, true);
						GUImgr.DrawBar (new Rect (53, yPos + 20, 4, 36), thisPokemon.pp, GUImgr.pp, false);
						GUImgr.DrawBar (new Rect (58, yPos + 20, 4, 36), thisPokemon.hp, GUImgr.hp, false);
						//rect = new Rect (0, yPos, 72, 70);
						Rect tmprect = new Rect (0, yPos, 72, 70);
						rectList.Add (tmprect);
						pokemonList.Add (thisPokemon);
						yPos += 70;
						x++;
				}
		}
}