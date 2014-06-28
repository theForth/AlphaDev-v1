public enum PokeballState{

    None = 0,
    Selecting = 1,                    //Selection State
    Releasing = 2,                    //Pokeball Releasing animation etc
    Released = 3,                     //After animation and particle effect, Pokemon released
    Capturing= 4,                     //Release Pokeball for capture
    Returning = 5,                    //Return Pokeball after faint or on keystroke
    ReturningOnCapture = 6           // Return Pokeball after capture    
}
