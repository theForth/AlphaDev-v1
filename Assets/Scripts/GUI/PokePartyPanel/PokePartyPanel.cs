using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PokePartyPanel : MonoBehaviour
{
    private PokeParty pokeParty;
    public List<PokePartyFrame> PokePartyFrame;


    void Start()
    {
        pokeParty = GameObject.Find("Player").GetComponent<Trainer>().pokeParty;

        Init();

    }
    private void Init()
    {

        if (pokeParty.HasPokemon())
        {
            for (int i = 0; i < pokeParty.SlotCount(); i++)
            {
                PokePartyFrame[i].gameObject.SetActive(true);
                PokePartyFrame[i].AssignPartyFrame(pokeParty.GetPokeBySlot(i));
            }
        }
    }
    public void OnPokemonRelease(int selectedIndex)
    {

    }
}

