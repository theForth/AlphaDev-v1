using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Holoville.HOTween;
public class ProgressBar : MonoBehaviour
{

    //**********Pokemon*********
    public ProgressBarType progressBarType = new ProgressBarType();
    public PokeCore ActivePokemon;
    public bool IsActivePokemon;
    private bool AssignedPokemon = false;
    // public UIWidget target;
    private bool _enabled = false;
    public UIProgressBar bar;
    public int offset = 0;
    public float smoothingFactor = 2f;
    public bool fading = true;
    public float fadeDuration = 0.5f;
    public float currentValue;
    public float newValue;
    public float maxValue;
    [SerializeField]
    bool isLabel = true;
    public UILabel valueLabel;
    private string test = "";
    StringBuilder sb = new StringBuilder();


    void Start()
    {

        //if (this.target == null || this.bar == null)
        //{
        //  Debug.LogWarning(this.GetType() + " requires target UIWidget and UIProgressBar in order to work.", this);
        // this.enabled = false;
        //return;
        //}

        // Get the default with of the target widget
        // this.defaultWidth = this.target.width;

        // Get the default alpha of the target widget
        // this.defaultAlpha = this.target.alpha;
        this.currentValue = this.bar.value;
        // Hook on change event
        //this.bar.onChange.Add(new EventDelegate(OnBarChange));
    }
    /// <summary>
    /// progressBartype 0 = HP
    /// progressBartype 1 = PP
    /// </summary>
    /// <param name="pokeBattler"></param>




    public IEnumerator ActivateHPBar()
    {
        SetHPValues();
        this.bar.value = currentValue / maxValue;
        valueLabel.text = (int)currentValue + "/" + maxValue;
        while (IsActivePokemon)
        {
            if (ActivePokemon != null)
            {


                //this.bar.value = ActivePokemon.pokemon.currentHealth / maxValue;
                if (currentValue > (ActivePokemon.pokemon.currentHealth) + 1)
                {

                    currentValue = Mathf.Lerp(currentValue, ActivePokemon.pokemon.currentHealth, Time.deltaTime * 1);
                    UpdateValueText();
                    bar.value = currentValue / maxValue;

                }


            }
            yield return 0;
        }
    }
    public IEnumerator ActivatePPBar()
    {
        SetPPValues();
        SetLabel();
        while (IsActivePokemon)
        {
            if (ActivePokemon != null)
            {


                //this.bar.value = ActivePokemon.pokemon.currentHealth / maxValue;
                /*   if (currentValue > (ActivePokemon.pokemon.currentPP) + 1)
                   {
                       currentValue = Mathf.Lerp(currentValue, ActivePokemon.pokemon.currentPP, Time.deltaTime * 1);
                        valueLabel.text = (int)currentValue + "/" + maxValue;
                       bar.value = currentValue / maxValue;
                   }
                 */
                if (currentValue > (ActivePokemon.pokemon.currentPP + 0.1) || currentValue < (ActivePokemon.pokemon.currentPP))
                {

                    currentValue = Mathf.Lerp(currentValue, ActivePokemon.pokemon.currentPP, Time.deltaTime * 1);
                    UpdateValueText();
                    
                    bar.value = currentValue / maxValue;

                }
                /*
                if (currentValue > ActivePokemon.pokemon.currentPP )
                {
                    if (!HOTween.IsTweening(this))
                    HOTween.To(this, 0.5f, "currentValue", ActivePokemon.pokemon.currentPP);

                }
                else if (!HOTween.IsTweening(this))
                {

                    HOTween.To(this, 0.5f, "currentValue", ActivePokemon.pokemon.currentPP);

                
                valueLabel.text = (int)currentValue + "/" + maxValue;
                bar.value = currentValue / maxValue;
                */

            }
            yield return 0;
        }
    }
    void UpdateValueText()
    {
        if (valueLabel!=null)
        valueLabel.text = (int)currentValue + "/" + maxValue;
    }
  
    public void SetHPValues(Pokemon pokemon)
    {
        maxValue = pokemon.health;
        currentValue = pokemon.currentHealth;
    }
    public void SetPPValues(Pokemon pokemon)
    {
        maxValue = pokemon.PP;
        currentValue = pokemon.currentPP;
    }
    public void SetHPValues()
    {
        maxValue = ActivePokemon.pokemon.health;
        currentValue = ActivePokemon.pokemon.currentHealth;
    }
    public void SetPPValues()
    {
        maxValue = ActivePokemon.pokemon.PP;
        currentValue = ActivePokemon.pokemon.currentPP;
    }
    public void SetLabel()
    {
        this.bar.value = currentValue / maxValue;
        valueLabel.text = (int)currentValue + "/" + maxValue;
    }
    private bool AssignPokemon()
    {
        return false;
    }
    void OnEnable()
    {

        if (currentValue != 0)
            newValue = currentValue;
    }
    void OnDisable()
    {

        currentValue = -1;
        newValue = -1;
        //totalValue= -1;
    }
    public void onBarChange()
    {

    }
    //A few Extra utilities
    public void Enable(PokeCore pokeCore)
    {
        this.ActivePokemon = pokeCore;
        enabled = true;
    }
    public virtual void DisableReset()
    {
        if (this.ActivePokemon == null)
            return;
        this.ActivePokemon = null;
        enabled = false;
    }
    public void Disable()
    {

        enabled = false;
    }


}
