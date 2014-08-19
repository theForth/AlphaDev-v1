using UnityEngine;
using System.Collections;
using Utilities.PokeUtils;
/** Managing Player/Pokeball and ActivePokemonStates.

Hierarhcy Level 2
 * Player Battle Controller
 * Every action corresponding to a battle manevour controlled by the player takes place here
 * Movement is handled in its movement controller.
 * Pokemon Selection happens here as Control is defined by pokemon selection. 
 * PlayerControlManager controls use of trainer or pokemon and disables/Enables scripts accordling.
 * Trainer will go into an AI pathfinding follow mode when pokemon is released. 
 * Its trainercontroller (Movement) script will be disabled to improve perforance.
 * 
 */


public class PlayerControlManager : MonoBehaviour
{

    //************Core Variables****************************\\
    public static PlayerControlState playerControlState = PlayerControlState.Trainer;
    private ThirdPersonCameraControl thirdPersonCameraControl;

    //*********Trainer variables***************************\\
    private GameObject trainerObject;
    private TrainerController trainerController;
    private Trainer trainer;
    //******************************************************\\


    //*********Pokemon  variables***************************\\
    public static int PokePartyIndex = -1;                  //GUI controller will listen to this number
    public static PokeballState pokeballState = PokeballState.None;
    private PokemonController pokemonController;
    public static int selectedMoveIndex = 0;
    //******************************************************\\

    //***********KeyBinding*********************************\\
    public string triggerAxis = "Fire1";
    public string triggerPokeReturn = "PokeReturn";
    private KGFMapSystem itsKGFMapSystem;
    //****************** Event Delegation*****************
    public delegate void EventPokemonRelease(int PokePartyIndex, PokeCore pokeCore);
    public delegate void EventCastMove(int selection);
    public delegate void EventPokemonReturn();
    public delegate void EventSelectMove(int selectedMoveIndex);
    public static event EventSelectMove SelectMove = new EventSelectMove((int selectedMoveIndex) => { });
    public static event EventCastMove CastMove = new EventCastMove((int selection) => { });
    public static event EventPokemonRelease eventPokemonRelease = new EventPokemonRelease((int PokePartyIndex, PokeCore pokeCore) => { });
    public static event EventPokemonReturn eventPokemonReturn = new EventPokemonReturn(() => { });
    void Start()
    {
        itsKGFMapSystem = KGFAccessor.GetObject<KGFMapSystem>();
        trainerObject = GameObject.FindGameObjectWithTag("Player");
        trainer = trainerObject.GetComponent<Trainer>();
        trainerController = trainerObject.GetComponent<TrainerController>();               //Disable The component as the player will eventuall become AI.
        thirdPersonCameraControl = Camera.main.GetComponent<ThirdPersonCameraControl>();

    }

    void Update()
    {
        //***************************Pokemon Released*********************************\\
        //Pokemon can only be released again after 
        //1) Not being hit in battle 
        //2) After the pokeball has returned back

        if (pokeballState == PokeballState.Released)                 // State required to communicate between pokeball release since its not directly after KeyPress.
        // We can consider it a trigger state that pulls it back to none and only after this state is over a 
        //player may select another pokemon
        {

            playerControlState = PlayerControlState.Pokemon;   //Set Global Control State
            thirdPersonCameraControl.StartTransition(Trainer.ActivePokemon.transform);       //Camera Transition to Pokemon
            itsKGFMapSystem.SetTarget(Trainer.ActivePokemon.gameObject);  //Set The Minimap Target
            trainerController.ani.SetFloat("Speed", 0f);
            trainerController.enabled = false;                    //Disable the control script                                                        //Set Camera's new target to the newly Active Pokemon   
            eventPokemonRelease(trainer.pokeParty.SelectedIndex,Trainer.ActivePokemon);
            pokeballState = PokeballState.None;                    // The pokemon
        }
        //****************************Move Casting****************************\\
        for (int i = 0; i < 4; i++)
        {
            if (cInput.GetKeyDown("Move Slot " + i))
            {
                SelectMove(i);
                selectedMoveIndex = i;
            }
        }


        if (playerControlState == PlayerControlState.Pokemon)
        {
            selectedMoveIndex = Input.GetKeyDown(KeyCode.Alpha1) ? 0 : selectedMoveIndex;
            selectedMoveIndex = Input.GetKeyDown(KeyCode.Alpha2) ? 1 : selectedMoveIndex;
            selectedMoveIndex = Input.GetKeyDown(KeyCode.Alpha3) ? 2 : selectedMoveIndex;
            selectedMoveIndex = Input.GetKeyDown(KeyCode.Alpha4) ? 3 : selectedMoveIndex;

            if (Input.GetMouseButtonDown(1))
            {
                Trainer.ActivePokemon.pokemon.currentHealth -= 5;
                switch (PokeUtils.CanCastMove(Trainer.ActivePokemon, selectedMoveIndex))
                {
                    case 1: Debug.Log("Not enough PP"); break;
                    case 2: ; InitiateMove(); break;

                }

                // Trainer.ActivePokemon.CastMove(selectedMoveIndex);
            }
        }
        //******************************************************************************
        //***************************Pokemon Returning********************************\\


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CanReturnPokemon())
            {

                playerControlState = PlayerControlState.Trainer;             //Set Global Control State
                thirdPersonCameraControl.StartTransition(trainerController.transform);     //Camera Transition to Pokemon +  Transition State to not disturb Camera  + Set Camera's new target back to the Trainer    
                EnableTrainerMovementScript();
                pokeballState = PokeballState.None;
                itsKGFMapSystem.SetTarget(trainerObject);
                eventPokemonReturn();
                Destroy(Trainer.ActivePokemon.gameObject);

                // After the PokemonGameObject is disabled/Destroyed and the particle effect
                // and animation is finished the statewill change
            }
        }

        //****************************************************************************\\


        #region Pokemon Selection
        //***************************Pokemon Selection*********************************//
        //This can be moved to the seperate Pokemon Selector script
        //Quick access as PokeIndex can easily updated and sent to GUI. GUI requires quick updates. Having to check into pokeparty everytime to update with slot info is CPU heavy.
        if (playerControlState == PlayerControlState.Trainer)
        {
            if (pokeballState == PokeballState.None || pokeballState == PokeballState.Selecting)  //two states for now. We might decide to have a selecting state
            {
                for (int i = 0; i < 4; i++)
                {
                    if (cInput.GetKeyDown("Poke Slot " + i))
                    {
                        PokePartyIndex = i;
                        pokeballState = PokeballState.Selecting;
                    }
                  
                }
          

                if (pokeballState == PokeballState.Selecting) //Animation State for Throw
                {
                    if (Input.GetAxis(triggerAxis) > 0)
                    {
                        if (trainer.ThrowPokemon(PokePartyIndex) == 1)  //Animation for release should happen here +The Throw was succesful
                        {

                            OnPokeballThrow();
                            //ThirdPersonCameraControl.Controllable = false;


                        }

                    }

                }

                //*****************************************************************************//


            }
        #endregion
        }
    }
    private void OnPokeballThrow()
    {

        trainerController.ani.SetFloat("Speed", 0f);
        //playerControlState = PlayerControlState.Pokemon;
        DisableTrainerMovementScript();
    }
    private void DisableTrainerMovementScript()
    {
        trainerController.enabled = false;
    }
    private void EnableTrainerMovementScript()
    {
        trainerController.enabled = true;
    }
    private void InitiateMove()
    {
        CastMove(selectedMoveIndex);

    }
    private bool CanReturnPokemon()
    {
        return Trainer.ActivePokemon.canReturn;
    }



}
