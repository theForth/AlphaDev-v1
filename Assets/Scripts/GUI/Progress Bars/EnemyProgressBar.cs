using UnityEngine;

public class EnemyProgressBar: ProgressBar
{

    void Update()
    {
        if(enabled)
        {
            
            if (currentValue > (ActivePokemon.pokemon.currentHealth) + 1)
            {

                currentValue = Mathf.Lerp(currentValue, ActivePokemon.pokemon.currentHealth, Time.deltaTime * 1);
                valueLabel.text = (int)currentValue + "/" + maxValue;
                bar.value = currentValue / maxValue;

            }
        }
    }


    public void EnableInit(PokeCore enemy)
    {
        this.ActivePokemon = enemy;
        this.enabled = true;
        SetHPValues();
        SetLabel();
    }
}