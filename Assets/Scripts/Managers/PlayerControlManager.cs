using UnityEngine;
using System.Collections;
/** Managing Player/Pokeball and ActivePokemonStates.

Hierarhcy Level 2
 * Pokemon Selection happens here as Control is defined by pokemon selection. 
 * PlayerControlManager controls use of trainer or pokemon and disables/Enables scripts accordling.
 * Trainer will go into an AI pathfinding follow mode when pokemon is released. 
 * Its trainercontroller (Movement) script will be disabled to improve perforance.
 */


    public  class PlayerControlManager : MonoBehaviour
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
        //******************************************************\\

        //***********KeyBinding*********************************\\
        public string triggerAxis = "Fire1";
        public string triggerPokeReturn = "PokeReturn";
        private KGFMapSystem itsKGFMapSystem; 
 
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

                  if(pokeballState == PokeballState.Released)                 // State required to communicate between pokeball release since its not directly after KeyPress.
                                                                              // We can consider it a trigger state that pulls it back to none and only after this state is over a 
                                                                         //player may select another pokemon
            {

                playerControlState = PlayerControlState.Pokemon;   //Set Global Control State
                thirdPersonCameraControl.StartTransition(Trainer.ActivePokemon.transform);       //Camera Transition to Pokemon
                //Transition State to not disturb Camera
                   //Set Camera's new target    
                //Set Camera's new target 
                itsKGFMapSystem.SetTarget(Trainer.ActivePokemon.gameObject);
                trainerController.ani.SetFloat("Speed", 0f);
                trainerController.enabled = false;                    //Disable the control script
                      //Set Camera's new target to the newly Active Pokemon   
                pokeballState = PokeballState.None;                    // The pokemon
            }
            //****************************************************************************\\


            //***************************Pokemon Returning********************************\\

             
                  if (Input.GetKeyDown(KeyCode.R))
                  {
                      if (CanReturnPokemon())
                      {

                          playerControlState = PlayerControlState.Trainer;             //Set Global Control State
                          thirdPersonCameraControl.StartTransition(trainerController.transform);     //Camera Transition to Pokemon +  Transition State to not disturb Camera  + Set Camera's new target back to the Trainer    
                          trainerController.enabled = true;
                          pokeballState = PokeballState.None;
                          itsKGFMapSystem.SetTarget(trainerObject);
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
                          PokePartyIndex = Input.GetKeyDown(KeyCode.Alpha1) ? 0 : PokePartyIndex;
                          PokePartyIndex = Input.GetKeyDown(KeyCode.Alpha2) ? 1 : PokePartyIndex;
                          PokePartyIndex = Input.GetKeyDown(KeyCode.Alpha3) ? 2 : PokePartyIndex;
                          PokePartyIndex = Input.GetKeyDown(KeyCode.Alpha4) ? 3 : PokePartyIndex;
                          PokePartyIndex = Input.GetKeyDown(KeyCode.Escape) ? -1 : PokePartyIndex;
                          pokeballState = (PokePartyIndex >= 0 && PokePartyIndex < 4) ? PokeballState.Selecting : PokeballState.None;

                          if (pokeballState == PokeballState.Selecting) //Animation State for Throw
                          {
                              if (Input.GetAxis(triggerAxis) > 0)
                              {
                                  if (trainer.ThrowPokemon(PokePartyIndex) == 1)  //Animation for release should happen here +The Throw was succesful
                                  {

                                      //ThirdPersonCameraControl.Controllable = false;



                                  }

                              }

                          }

                          //*****************************************************************************//


                      }
 #endregion
                  }
        }

      
        private bool CanReturnPokemon()
        {
            return Trainer.ActivePokemon.canReturn;
        }
             

    
    }
