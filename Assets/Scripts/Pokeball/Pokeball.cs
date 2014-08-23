using UnityEngine;
using System.Collections;




/**Primary Pokeball Release component
 * 1)Will only be evoked for release if the required slot is not Null or pokemon is dead.
 * 2)The PokemonGameObject will be mapped to the pokemon data thats generated by the trainer.
*/
public class Pokeball : MonoBehaviour
{
		public Trainer trainer = null;
		public Pokemon pokemon = null;
        public int pokePartyIndex = 0;
        public AudioClip openPokeball;
		public bool isActive = true;  //MonoBehaviour also defines a field called active, which has a different purpose. Use the 'new' keyword if you decide to call it active.
		bool fired = false;
		float lifetime = 1;

        
    //***********************Intializing Values on release*****************************
    public void Init(Trainer trainer,int pokePartyIndex,AudioClip openPokeball)

        {
            this.trainer = trainer;

            this.pokePartyIndex = pokePartyIndex;
            this.openPokeball = openPokeball;

        }
    //**********************************************************************************
	
    
    
    
    void Update ()
		{
				/*	if (lifetime < 2.9f && collider != null)
					collider.enabled = true;
		
				if (pokemon != null) {*/

            // 
		
            lifetime -= Time.deltaTime;

	        if (lifetime < 0 && !fired) 
            {
                   // audio.PlayOneShot(openPokeball);
                MasterAudio.PlaySound("OpenPokeball", 100);
					Transform particles = transform.FindChild ("Particles");
								
                if (particles)
                                
                {
                                   
										
                    particles.parent = null;
										
                    particles.GetComponent<ParticleSystem> ().Play ();
										
                    Destroy (particles.gameObject, 1);
								
                }
                                
                renderer.enabled = false;	//Pokeball Dissappears when the pokemon is released			
                Destroy (gameObject,0.9f);	//sound effect is still being played after pokeball is destroyed and hence is destroyed 0.90s later			
                fired = true;
                GameObject pokeObj = (GameObject)Instantiate(trainer.TemporaryPokemonSlot[pokePartyIndex], transform.position, Camera.main.transform.rotation);              
                Trainer.ActivePokemon = pokeObj.AddComponent<PokeBattler>();//Caching in the Active Pokemons Object so data can easily be retreived.
                Trainer.ActivePokemon.pokemon = trainer.pokeParty.GetPokeSlot(pokePartyIndex).pokemon;                          
                PlayerControlManager.pokeballState = PokeballState.Released;
								/*
                                 (GameObject)Instantiate (projectile, transform.position, transform.rotation);
0								if (pokemon != null) {
										GameObject pokeObj = (GameObject)Instantiate (Resources.Load ("Pokemon/" + Pokemon.GetName (pokemon.number)));
										pokeObj.transform.position = transform.position;
										pokeObj.transform.rotation = Quaternion.Euler (0, Random.value * 360, 0);
										pokeObj.GetComponent<PokeBattler> ().pokemon = pokemon;
										pokeObj.name = pokemon.name;
										pokemon.obj = pokeObj.GetComponent<PokeBattler> ();
										PokemonDomesticated pokeDom = pokeObj.AddComponent<PokemonDomesticated> ();
										PokemonGUI pokeGui = pokeObj.AddComponent<PokemonGUI> ();
										pokeDom.trainer = trainer;
					
										//assuming direct control
										if (trainer == Player.trainer) {
												Player.pokemonActive = true;
										}
								}*/
						}
				}
		
	
		public static void ThrowPokeBall (Trainer trainer)
		{
				//find the nearest pokemon to capture, withing the correct direction I guess
				float dist = 1000000;
				GameObject pokemonOb = null;
				foreach (GameObject poke in GameObject.FindGameObjectsWithTag("pokemon")) {
						Vector3 direct = trainer.transform.position - poke.transform.position;
						if (direct.sqrMagnitude < dist) {
								dist = direct.sqrMagnitude;
								pokemonOb = poke;
						}
				}
		
				GameObject ball = (GameObject)Instantiate (Resources.Load ("Pokeball"));
				ball.transform.position = GameObject.Find ("_PokeballHolder").transform.position;
		
				if (pokemonOb != null) {
						Vector3 direct = pokemonOb.transform.position - ball.transform.position;
						ball.rigidbody.AddForce (direct.normalized * 500 + Vector3.up * direct.magnitude / 50);
						ball.GetComponent<Pokeball> ().trainer = trainer;
				}
		}
		/*
		public void CapturePokemon ()
		{
				string printme = "";
				PokeBattler targetPokemon = Player.pokemon.obj.enemy;
				if (targetPokemon != null) {
						if (targetPokemon.GetComponent<PokemonWild> () != null) {
								float statusAilment = 0;	//statusAilment = 12 if poisoned/burned/paralyzed, 25 if frozen or asleep, 0 otherwise.
								float ballMod = 150;		//ballMod = 255 if using a Poké Ball, 200 if using a Great Ball, and 150 otherwise.
								float captureOne = statusAilment / (ballMod + 1);
								float captureRate = 22;	//need to put this into DB: http://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_catch_rate
								float ballFactor = 12;
								float f = (((targetPokemon.pokemon.TotalHP ()) * 255) / ballFactor) / (targetPokemon.pokemon.hp / 4);
								/*
				 * 	f = (HPmax * 255 / Ball Factor) / (HPcurrent / 4), where all divisions 
				 * are rounded down to the nearest integer (the denominator is set to 1 if 
				 * it is 0 as a result). The Ball Factor is 8 if a Great Ball is used, and 
				 * 12 otherwise. The resulting value is capped at a maximum of 255. 
				 *
								float captureTwo = ((captureRate + 1) / (ballMod + 1)) * ((f + 1) / 256);
				
								//printme = "capture " + targetPokemon.pokemon.name + ". It has " + targetPokemon.pokemon.hp + "hp remaining!";
								//if (targetPokemon.pokemon.hp*100 < 15) {
								float captureChance = captureOne + captureTwo;
								if (captureChance >= Random.value) {
										if (Player.trainer.party.Count () < PokeParty.PARTY_MAX) {
												//printme = printme + "\n Okay!";
												printme = "You've captured a " + targetPokemon.pokemon.GetName () + "!";
												targetPokemon.Return ();
												Player.trainer.party.AddPokemon (new Pokemon (targetPokemon.pokemon.number, true));
										} else {
												printme = "You would have captured a " + targetPokemon.pokemon.GetName () + ", but you have too many pokemon, and we haven't built" +
														"out a way to choose which pokemon you'd like to keep!";
										}
								} else {
										//printme = printme + "\n It's too strong!";
										printme = "You tried to capture " + targetPokemon.pokemon.GetName () + ", but it broke free!";
								}
								//printme += "\n " + captureChance;
						} else {
								printme += "You can't capture that pokemon! That would be stealing!";
						}
				} else {
						//printme = "Nothing found to capture!";
				}
				
		}*/
}