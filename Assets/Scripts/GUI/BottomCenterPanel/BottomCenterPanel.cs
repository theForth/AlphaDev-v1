using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class BottomCenterPanel : MonoBehaviour
{

    public List<MoveData> moveData;
    bool activate = true;
    bool deactivate = false;
  
    public PokeCore ActivePokemon; //Caching it to an easier to access reference
    public PlayerProgressBar HPbar;
    public PlayerProgressBar PPbar;
    private int windowID;
    void Start()
    {
        //PlayerControlManager.eventPokemonRelease += OnPokemonRelease;
        //PlayerControlManager.eventPokemonReturn += OnPokemonReturn;
    }


    public void OnPokemonRelease(PokeCore pokeCore)
    {
        this.ActivePokemon = pokeCore;
        UIUtils.TriggerPanel(activate, windowID);
        moveData = Trainer.ActivePokemon.moves;
        for (int i = 0; i < moveData.Count; i++)
            MoveSlot.GetSlot(i).Assign(Trainer.ActivePokemon.moves[i]);
        HPbar.OnPokemonRelease(pokeCore);
        PPbar.OnPokemonRelease(pokeCore);
    }
    public void  InitializeBottomPanel()
    {

    }
  
 
    public void OnPokemonReturn()
    {
        HPbar.OnPokemonReturn();
        PPbar.OnPokemonReturn();
        UIUtils.TriggerPanel(deactivate, windowID);
    }
}
