using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trainer : MonoBehaviour
{ //Need to separate the Unity object from this object, like Pokemon has. (Target)
    private Transform hand;
    public PokeParty pokeParty;
    public TrainerData trainerData;

    public GameObject[] TemporaryPokemonSlot;          //TODO this will be list of slots.
    public static PokeCore ActivePokemon;          //TODO need to solve how we deal with PlayerTrainer,Cached in here for quick reference during game and for GUI updates.
    private PokeballProjectileController pokeballProjectileController;
    //public Inventory inventory;
    //public Inventory.Item item {get{return inventory.selected;} set{}}


    void Start()
    {
        hand = transform.Find("GameObject");                                      //Pokeball must be attached to the hand. Projectile script will be on that hand 
        pokeballProjectileController = hand.GetComponent<PokeballProjectileController>();
        pokeballProjectileController.trainer = this;
        pokeParty = new PokeParty(this);
        pokeParty.AddPokemon(Temp.PopulateDB.instance.getBasicPokemon(25));  //this is pikachu for now
        pokeParty.AddPokemon(Temp.PopulateDB.instance.getBasicPokemon(1));
        Debug.Log(pokeParty.GetSlot(0).pokemon.name + "Created");
        //we add all the pokemon into the pokeParty here with a for loop. The pokeParty also can return a list of ID's which can be used to 
        //prepopulate the pokemon into the RAM instead of hard-disk.
        //populate pokeParty
        //populate inventory
        //inventory = new Inventory(this);
    }

    public Target.TARGETS GetTargetType()
    {
        return Target.TARGETS.TRAINER;
    }

    public int ThrowPokemon(int pokePartyIndex)
    {/*
       // Pokemon throwPokemon = pokeParty.GetPokemon(PokePartyIndex);
        if (throwPokemon.thrown)
        {

            return 1;
        }*/

        pokeballProjectileController.Shoot(pokePartyIndex);
        return 1;
    }
    /*public void ThrowPokemon (Pokemon poke)
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
	
*/
}