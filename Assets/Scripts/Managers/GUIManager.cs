using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
//*************************************************************
//** P  *********************************************************
//** A  *********************************************************
//** R  ********************************************************
//** T  **********************************************************
//** Y  *********************************************************
//************************************************************
//** P *********************************************************
//** A ********************************************************
//** N *********************************************************
//** E ********************************************************
//** L *********************************************************
//************************************************************
//****************   BottomCenterPanel    **********************
//**************************************************************  

/// <summary>
/// Instead of all Panels constantly listening to events. THE GUI Manager panel will be the leader listening to the events and sending messages 
/// to other handlers.
/// </summary>


public class GUIManager : Singleton<GUIManager>
{

    //******************Consts******************************
    private bool deactivate = false;
    private bool activate = true;
    public static bool FloatingHealthBarActive;
    private List<MoveData> moveData;
     //******************panels******************************
    public BottomCenterPanel bottomCenterPanel;
    public PokePartyPanel pokePartyPanel;
    public  EnemyStatusPanel enemyStatusPanel;
    private bool tempflag = true;


    // Use this for initialization
    void Start()
    {
        //HOTween.EnableOverwriteManager();
        PlayerControlManager.eventPokemonRelease += OnPokemonRelease;
        PlayerControlManager.eventPokemonReturn += OnPokemonReturn;
    }

    // Update is called once per frame
    void Update()
    {


        
    }
    /// <summary>
    /// CALL all the GUI methods to start processing the required info
    /// </summary>
    /// <param name="pokeCore"></param>
    private void OnPokemonRelease(int selectedIndex,PokeCore pokeCore)
    {
        
        bottomCenterPanel.OnPokemonRelease(pokeCore);
        pokePartyPanel.OnPokemonRelease(selectedIndex);
        enemyStatusPanel.Enable();
        PlayerControlManager.eventPokemonRelease -= OnPokemonRelease;
        //enemyStatusPanel.enabled = true;

    }

    
    private void OnPokemonReturn()
    {
        PlayerControlManager.eventPokemonRelease += OnPokemonRelease;
        bottomCenterPanel.OnPokemonReturn();
        enemyStatusPanel.Disable();
        //enemyStatusPanel.enabled = false; 
    }
}
