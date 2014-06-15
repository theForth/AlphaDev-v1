using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trainer : MonoBehaviour
{ //Need to separate the Unity object from this object, like Pokemon has. (Target)
		public PokeParty party;
		//public Inventory inventory;
		//public Inventory.Item item {get{return inventory.selected;} set{}}
		private
		Vector3
				velocity = Vector3.zero;
	
		void Start ()
		{
				party = new PokeParty (this);
				//inventory = new Inventory(this);
		}
	
		public Target.TARGETS GetTargetType ()
		{
				return Target.TARGETS.TRAINER;
		}
	
		public void ThrowPokemon (Pokemon poke)
		{
				if (poke.thrown)
						return;
				poke.thrown = true;
				GameObject ball = (GameObject)Instantiate (Resources.Load ("Pokeball"));
				ball.transform.position = transform.position;
				ball.rigidbody.AddForce ((transform.forward * 2 + transform.up) * 400);
				ball.GetComponent<Pokeball> ().pokemon = poke;
				ball.GetComponent<Pokeball> ().trainer = this;
				//gamegui.SetChatWindow(ball.GetComponent<Pokeball>().pokemon.GetName() + "! I choose you!");
		}
	
	
}