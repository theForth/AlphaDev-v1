using UnityEngine;
using System.Collections;
/** Managing Player States.
*/

    public  class PlayerManager : MonoBehaviour
    {



        private ThirdPersonCameraControl thirdPersonCameraControl;
        private GameObject trainer;
        private TrainerController trainerController;
        private PokemonController pokemonController;
        public static PlayerControlState playerControlState = PlayerControlState.Trainer;
        




        void Start()
        {
            trainer = GameObject.FindGameObjectWithTag("Player");
            trainerController = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainerController>();               //Disable The component as the player will eventuall become AI.
            thirdPersonCameraControl = Camera.main.GetComponent<ThirdPersonCameraControl>();
        }

        void Update()
        {
          
        }

        public  void TriggerPlayerToPokemonControl(PokemonObj pokemon)
        {

            trainerController.enabled = false;
        }
             

    
    }
