using UnityEngine;
using System.Collections;

public class PokemonGUI: MonoBehaviour
{

		private EnergyBar energyBar;
		private EnergyBarRenderer energyBarRenderer;
		public static bool HpBarToggle = false;
		BattleGUI battleGUI;
		PokemonObj pokemonObj;
		public GameObject HpBar;
		public GameObject PpBar;
		public GameObject HpBarEnemy;

		//GameGUI gamegui = new GameGUI();
		void Awake ()
		{
				pokemonObj = GetComponent<PokemonObj> ();
				//battleGUI = new BattleGUI(pokemonObj);

		}

		void Start ()
		{
				//spawnObject = (GameObject)Instantiate(Resources.Load("HpBar"));
		}


		void OnGUI ()
		{

				ToggleHpBar ();
	
		}

		private void ToggleHpBar ()
		{
				if (HpBarToggle) {
						if (HpBar == null && pokemonObj != null) {
								HpBar = (GameObject)Instantiate (Resources.Load ("HpBar"));
								PpBar = (GameObject)Instantiate (Resources.Load ("PPBar"));
								HpBar.GetComponent<EnergyBar> ().valueMin = 0;
								HpBar.GetComponent<EnergyBar> ().valueMax = (int)pokemonObj.pokemon.health;
								PpBar.GetComponent<EnergyBar> ().valueMin = 0;
								PpBar.GetComponent<EnergyBar> ().valueMax = 1;
						}
			
			
						if (HpBar != null && pokemonObj != null) {
								Vector2 xy;
								xy = Camera.main.WorldToScreenPoint (pokemonObj.transform.position);
								float x = xy.x;  
								float y = Screen.height - xy.y;
				
								HpBar.GetComponent<EnergyBarRenderer> ().screenPosition = new Vector2 (x - 40, y - 61);
								HpBar.GetComponent<EnergyBar> ().valueCurrent = pokemonObj.pokemon.currentHealth;
								PpBar.GetComponent<EnergyBarRenderer> ().screenPosition = new Vector2 (x - 40, y - 55);
								PpBar.GetComponent<EnergyBar> ().valueCurrent = pokemonObj.pokemon.pp;

								if (pokemonObj.enemy && pokemonObj.enemy.pokemon != null) {
										//moveCast = pokemonObj.enemy.pokemon.moveCast;
										//damage = (int)pokemonObj.enemy.pokemon.damage;
										if (!HpBarEnemy) {
												HpBarEnemy = (GameObject)Instantiate (Resources.Load ("HpBar"));
												HpBarEnemy.GetComponent<EnergyBar> ().valueMin = 0;
												HpBarEnemy.GetComponent<EnergyBar> ().valueMax = (int)pokemonObj.enemy.pokemon.health;
										}

										if (pokemonObj.enemy && HpBarEnemy != null) {
												Vector2 xy1;
												xy1 = Camera.main.WorldToScreenPoint (pokemonObj.enemy.transform.position);
												float x1 = xy1.x;
												float y1 = Screen.height - xy1.y; 
												HpBarEnemy.GetComponent<EnergyBarRenderer> ().screenPosition = new Vector2 (x1 - 40, y1 - 52);
												HpBarEnemy.GetComponent<EnergyBar> ().valueCurrent = pokemonObj.enemy.pokemon.currentHealth;
												//GUImgr.DrawBar(new Rect(x1-40,y1-60,85,8), pokemonObj.enemy.pokemon.hp, GUImgr.hp); //Draw Enemy HP Bar
												//GUI.Label(new Rect(x1+70,y1-100 ,250,40),  damage.ToString()+" damage taken from " +  moveCast + " !" );
										}

								}
								if (pokemonObj.enemy == null && HpBarEnemy) {
										Destroy (HpBarEnemy, 0);
								}

						}

			
						return;
				}

				if (pokemonObj == null) {
						if (HpBar != null) {
								Destroy (HpBar, 0);
								Destroy (PpBar, 0);
						}
						if (HpBarEnemy) {
								Destroy (HpBarEnemy, 0);
						}
			

				}


		}

		public static void ToggleHpbar ()
		{

				//GUI.Label (new Rect (x - 40, y - 200, 200, 20), "HP: " + currentHealth.ToString () + "/" + health.ToString ());
		
		}
	
}