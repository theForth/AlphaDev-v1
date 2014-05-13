﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public static bool click = false;
	static bool jumpCool = true;

	public static Trainer trainer = null;
	public static Pokemon pokemon {get{return trainer.party.GetActivePokemon();} set{}}
	public static bool pokemonActive = false;
	public static PokemonGUI pokemonGUI = new PokemonGUI();
	//public PokemonGUI pokemonGUI;
	public static GameGUI gamegui;
	public static BattleGUI battleGUI;

	void Start(){
		trainer = GameObject.Find("Player").GetComponent<Trainer>();
		battleGUI = gameObject.AddComponent<BattleGUI> ();
		gameObject.AddComponent<CameraControl> ();
		trainer.gameObject.AddComponent<PlayerMovement> ();
		gamegui = gameObject.AddComponent<GameGUI> ();
		gameObject.AddComponent<BattleTarget> ();
	}

	void Update(){
		//do nothing if in dialog
		if (Dialog.inDialog){
			Screen.lockCursor = false;
			Screen.showCursor = true;
			trainer.SetVelocity(Vector3.zero);
			return;
		}

		//menu
		if ((GameGUI.menuActive && !pokemonActive) || CameraControl.releaseCursor) {
			Screen.lockCursor = false;
			Screen.showCursor = true;
		} else {
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
		if (Input.GetKeyDown(KeyCode.F) && !click){
			if (GameGUI.fpsWindow)
				GameGUI.fpsWindow = false;
			else
				GameGUI.fpsWindow = !GameGUI.fpsWindow;

		}
		//capture pokemon
		if(Input.GetKeyDown("c")) {
			CapturePokemon();
		}
		
		//chat window
		if(Input.GetKeyDown ("i")){
			if(GameGUI.chatActive)
				GameGUI.chatActive=false;
			else
				GameGUI.chatActive=true;
			
			click = true;
		}

		//player control
		click = CanClick();
		if (pokemonActive && pokemon.obj!=null){
			HandlePokemon();
		}else{
			HandleTrainer();
			//move trainer
			Vector3 vel = Quaternion.Euler(0,CameraControl.ay,0) * (Vector3.forward*Input.GetAxis("Vertical") + Vector3.right*Input.GetAxis("Horizontal"));
			trainer.SetVelocity(vel);
		}
	}

	public void HandlePokemon() {
		//move pokemon
		trainer.SetVelocity(Vector3.zero);
		
		Vector3 velocity = Vector3.zero;
		velocity += pokemon.obj.transform.forward * Input.GetAxis("Vertical");
		velocity += pokemon.obj.transform.right * Input.GetAxis("Horizontal");
		velocity *= pokemon.obj.speed;
		
		pokemon.obj.SetVelocity(velocity);
		pokemon.obj.transform.Rotate(pokemon.obj.transform.up, Input.GetAxis("Mouse X"));
		
		if(Input.GetButton("Jump") && jumpCool && Physics.Raycast(pokemon.obj.transform.position+Vector3.up*0.1f, Vector3.down, 0.2f)){
			pokemon.obj.rigidbody.AddForce(Vector3.up*3000);
			jumpCool = false;
		}
		if(!Input.GetButton("Jump"))	jumpCool = true;
		
		pokemon.pp -= Time.deltaTime/500;
		if (pokemon.pp<=0){
			pokemonActive = false;
			pokemon.obj.Return();
		}
		if(Input.GetKeyDown ("x")){
			if(PokemonGUI.HpBarToggle) 
				PokemonGUI.HpBarToggle=false;
			else
				PokemonGUI.HpBarToggle=true;
		}
	
		/*
		if (Input.GetKeyDown (KeyCode.K) && !GameGUI.menuActive) {
			if (pokemonActive) {
				GameGUI.dataWindow = !GameGUI.dataWindow;
			}
		}
		*/

		if (Input.GetKeyDown ("h")) {
			PokeCenter.HealPokemon ();
		}
		//if (Input.GetKeyDown(KeyCode.Escape) && !click){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (battleGUI.showEnemyTargetWindow) {
				battleGUI.CloseTargetWindow();
			}
			else if (pokemonActive) {
				pokemon.obj.Return();
				pokemonActive = false;
				Vector3 vel = Quaternion.Euler(0,CameraControl.ay,0) * (Vector3.forward*Input.GetAxis("Vertical") + Vector3.right*Input.GetAxis("Horizontal"));
				trainer.SetVelocity(vel);
			}
			else {
				GameGUI.menuActive = !GameGUI.menuActive;
			}
			click = true;
		}
	}

	public void HandleTrainer() {
		//swap pokemon
		if (!click && !pokemonActive){
			Pokemon oldPokemonSelection = pokemon;
			
			for(int i = 1; i <= trainer.party.Count(); i++) {
				if (Rebind.GetInputDown("SELECT_POKE_PARTY_" + i))
					trainer.party.Select(i - 1);
			}
			
			if (Rebind.GetInputDown("SELECT_POKE_PREV"))
				trainer.party.SelectPrev();
			else if (Rebind.GetInputDown("SELECT_POKE_NEXT"))
				trainer.party.SelectNext();
			
			if (oldPokemonSelection!=pokemon){
				click = true;
				if (oldPokemonSelection.obj!=null){
					oldPokemonSelection.obj.Return();
					trainer.ThrowPokemon(pokemon);
				}
			}
		}

		var itemsCount = trainer.inventory.items.Count;
		
		//throw pokemon
		if (!click && Input.GetKey(KeyCode.Return)){
		//Swap above line for this one if you need to break the code to work without RebindInspector.
		//Suspect errors with Player.click, but haven't really investigated.
		//if (Input.GetKey(KeyCode.Return)){
			if (pokemon != null && pokemon.obj==null){
				trainer.ThrowPokemon(pokemon);
			}else{
				if (pokemonActive){
					pokemon.obj.Return();
					pokemonActive = false;
				}else{
					pokemonActive = true;
				}
			}
			click = true;
		}
		
		//activate menu
		if (Input.GetKeyDown(KeyCode.Escape) && !click){
			if (battleGUI.showEnemyTargetWindow) {
				battleGUI.CloseTargetWindow();
			}
			else if (pokemonActive)
				pokemonActive = false;
			else
				GameGUI.menuActive = !GameGUI.menuActive;
			click = true;
		}
	
		
		//hides or reveals the stats(Data) popup on press of k
		if (Input.GetKeyDown (KeyCode.K) && !GameGUI.menuActive) {
			if (!pokemonActive)
			GameGUI.dataWindow = !GameGUI.dataWindow;

		}

		/*
	 * don't try using this right now, because it doesn't exist!
		if (Input.GetKeyDown ("k")) {
			Populate okasf = new Populate();
			okasf.Test();
		}
	*/
		//anticlick
		bool anti = false;
		for(int i = 1; i <= 10 && !anti; i++) {
			if (Rebind.GetInput("SELECT_POKE_PARTY_" + i))
				anti = true;
		}
	}

	public static bool CanClick() {
		bool anti = false;
		for(int i = 1; i <= 10 && !anti; i++) {
			if (Rebind.GetInput("SELECT_POKE_PARTY_" + i))
				anti = true;
		}

		return (!anti && !Rebind.GetInput("SELECT_POKE_PREV") && !Rebind.GetInput("SELECT_POKE_NEXT")
		    	&& !Rebind.GetInput("THROW_POKEBALL") && !Input.GetKey(KeyCode.Escape)
		        && !Input.GetMouseButton(0) && !Input.GetMouseButton(1));
	}

	public void CapturePokemon(){
		Pokeball pokeball = this.gameObject.AddComponent<Pokeball> ();
		//Vector3 pokemonPositon = pokemonObj.transform.position;
		Vector3 pokemonPositon = pokemon.obj.enemy.transform.position;
		GameObject ball = (GameObject)Instantiate(Resources.Load("Pokeball"));
		//ball.transform.position = GameObject.Find("_PokeballHolder").transform.position;
		GameObject.Find ("_PokeballHolder").transform.LookAt(pokemonPositon);
		ball.transform.position = GameObject.Find ("_PokeballHolder").transform.position;
		//ball.rigidbody.AddForce
		//	( Camera.main.transform.forward*500+ Camera.main.transform.up*300 );
		ball.rigidbody.AddForce(pokemonPositon*500 + Camera.main.transform.up*300);
		//Pokeball.CapturePokemon();
		pokeball.CapturePokemon ();
		Destroy (ball, 1);
		Destroy (pokeball);
	}
}
