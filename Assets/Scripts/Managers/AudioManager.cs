using UnityEngine;
using System.Collections;


    public class AudioManager : MonoBehaviour
    {
        void Start()
        {
            PlayerControlManager.eventPokemonRelease += OnPokemonRelease;
            PlayerControlManager.eventPokemonReturn += OnPokemonReturn;
            //TODO Add event for OnEventFire
        }



        void OnPokemonRelease(int PokePartyIndex, PokeCore pokeCore)
        {
            //TODO music change
        }
        void OnPokemonReturn()
        {
            //TODO music change
        }
    }

