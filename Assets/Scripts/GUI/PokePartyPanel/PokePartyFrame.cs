using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokePartyFrame : MonoBehaviour
{

    public enum State
    {
        None,
        InPokeball,
        InCombat,
        Fainted,
    }

    private const string restingOverlay = "PartyUnitFrame_InPokeballOverlay";
    private const string combatOverlay = "PartyUnitFrame_CombatOverlay";
    private const string normalLevel = "PartyUnitFrame_Level_Background";
    private const string disconnectedLevel = "PartyUnitFrame_Level_Disconnected";

    public State startingState;
    public UIWidget container;
    public UISprite offlineOverlay;
    public UISprite stateOverlay;
    public UISprite levelContainer;
    public UILabel levelLabel;
    public UILabel pokeName;

    public PlayerProgressBar HPBar;
    public PlayerProgressBar PPBar;
    private State currentState = State.None;
    public int frameID;
    private Pokemon pokemon;
    private static Dictionary<int, PokePartyFrame> pokePartyFrames= new Dictionary<int, PokePartyFrame>();
    private static int _activeFrameID = -1;
    public static int ActiveFrameID { get { return _activeFrameID; } private set { _activeFrameID = value; } }
    void Start()
    {
        if (this.container == null)
            this.container = this.GetComponent<UIWidget>();

        if (pokePartyFrames.ContainsKey(this.frameID))
        {
            Debug.LogWarning("IconSlot: An IconSlot with ID (" + this.frameID + ") already exists, please consider changing it.");

            // Fix it, make sure we dont make a dead loop
            while (pokePartyFrames.ContainsKey(this.frameID) && this.frameID < 100)
                this.frameID++;
        }

        pokePartyFrames.Add(this.frameID, this);

        // Set the starting state
        this.SetState(this.startingState);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pokeName"></param>
    public void AssignPartyFrame(Pokemon pokemon)
    {
       
        
        this.pokemon = pokemon;
        AssignName();
        AssignEnergyBars();
        AssignLevel();
        

    }
    void AssignName()
    {
        pokeName.text = pokemon.name;
    }
    void AssignEnergyBars()
    {
        HPBar.SetHPValues(pokemon);
        HPBar.SetHPValues(pokemon);
    }
    void AssignLevel()
    {
        levelLabel.text = pokemon.level.ToString();
    }

    public void SetState(State state)
    {
        if (this.currentState == state)
            return;

        if (state == State.InPokeball || state == State.InCombat)
        {
            // Set the sprite
            this.stateOverlay.spriteName = (state == State.InPokeball) ? restingOverlay : combatOverlay;

            // Enable the state overlay
            this.stateOverlay.enabled = true;
        }
        else
        {
            // Disable the state overlay
            this.stateOverlay.enabled = false;
        }

        // Set the offline state
        this.SetFainted(state == State.Fainted);

        // Save the state as current
        this.currentState = state;
    }

    private void SetFainted(bool state)
    {
        if (state)
        {
            // Enable the offline overlay
            if (this.offlineOverlay != null)
                this.offlineOverlay.enabled = true;

            // Change the level
            if (this.levelLabel != null)
                this.levelLabel.enabled = false;

            if (this.levelContainer != null)
                this.levelContainer.spriteName = disconnectedLevel;
        }
        else
        {
            // Disable the offline overlay
            if (this.offlineOverlay != null)
                this.offlineOverlay.enabled = false;

            // Change the level
            if (this.levelLabel != null)
                this.levelLabel.enabled = true;

            if (this.levelContainer != null)
                this.levelContainer.spriteName = normalLevel;
        }
    }

    public void UnAssignPartyFrame()
    {

    }
}
