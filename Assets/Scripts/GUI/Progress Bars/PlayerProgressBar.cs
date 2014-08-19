using UnityEngine;

public class PlayerProgressBar : ProgressBar
{
    public void OnPokemonReturn()
    {
       
        switch ((int)progressBarType)
        {
            case 0: StopCoroutine(ActivateHPBar()); break;
            case 1: StopCoroutine(ActivatePPBar()); break;
        }
        this.ActivePokemon = null;

    }
    public void OnPokemonRelease(PokeCore pokeCore)
    {
        IsActivePokemon = true;
        this.ActivePokemon = pokeCore;

        switch ((int)progressBarType)
        {
            case 0: StartCoroutine(ActivateHPBar()); break;
            case 1: StartCoroutine(ActivatePPBar()); break;
        }

    }
}