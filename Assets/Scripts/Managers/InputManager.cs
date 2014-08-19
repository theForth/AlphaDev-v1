using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
   

    void Start()
    {
       
        cInput.Init();
      
        

        // first we setup the allowed modifier keys, by default there will be no modifiers. If you don't want modifier keys skip this step
        // keep in mind that if a key is set as modifier it can't be used as a normal input anymore!
        cInput.AddModifier(KeyCode.LeftShift);
        cInput.AddModifier(KeyCode.RightShift);
        cInput.AddModifier(KeyCode.LeftAlt);
        cInput.AddModifier(KeyCode.RightAlt);
        cInput.AddModifier(KeyCode.LeftControl);
        cInput.AddModifier(KeyCode.RightControl);

        //cInput.SetKey("Pause", "P"); // sets the 'Pause' input to "P" - notice we didn't set up a secondary input-this will be defaulted to 'None'
        cInput.SetKey("Left", "A", ""); // sets the 'Left' primary input to 'A' and the secondary input to 'LeftArrow'
        cInput.SetKey("Right", "D", Keys.RightArrow); // inputs can be set as string or as Key, using the Keys class
        cInput.SetKey("Up", "W", Keys.UpArrow); // using the Keys class allows you to autocomplete the inputs
        cInput.SetKey("Down", "S", Keys.DownArrow);
        cInput.SetKey("Jump", Keys.Space, Keys.X, Keys.None, Keys.LeftShift); // here we set up a default modifier key for "X" so ACTION "Shoot" will default to 'SPACE' & 'LeftShift+X' as default inputs 

        // The Keys class can be very helpful in getting the correct name.
        cInput.SetKey("Move Slot 0", Keys.Alpha1);
        cInput.SetKey("Move Slot 1", Keys.Alpha2);
        cInput.SetKey("Move Slot 2", Keys.Alpha3);
        cInput.SetKey("Move Slot 3", Keys.Alpha4);
        cInput.SetKey("Poke Slot 0", Keys.Alpha1);
        cInput.SetKey("Poke Slot 1", Keys.Alpha2);
        cInput.SetKey("Poke Slot 2", Keys.Alpha3);
        cInput.SetKey("Poke Slot 3", Keys.Alpha4);
        cInput.SetKey("Weapon 5", Keys.Alpha5);
        cInput.SetKey("Weapon 6", Keys.Alpha6);
        // Note that the aboveinputs aren't actually used in this demo.
        // They're just defined here to show you how it's done.

        // we define an axis like this:
        cInput.SetAxis("Horizontal", "Left", "Right"); // we set up the 'Horizontal' axis with 'Left' and 'Right'as inputs
        cInput.SetAxis("Vertical", "Up", "Down"); // we set up 'Vertical' axis with 'Up' and 'Down' as inputs. 
        cInput._SaveInputs();
        // Notice we don't use the 'Vertical' axis in our control code in plane.cs but we don't want to allow modifier keys for inputs UP and DOWN. Any inputs that are part of an axis are ignoring modifiers
    }
}
