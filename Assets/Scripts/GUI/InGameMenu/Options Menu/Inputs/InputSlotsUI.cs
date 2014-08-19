using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputSlotsUI : MonoBehaviour
{

    public UILabel labelInput;
    bool isWaitingForKeyPress = false;
    public int inputSlotNumber;
    public InputType inputType = InputType.Primary;
    void Start()
    {

    }
   void OnClickInput()
   
   {
       if(!isWaitingForKeyPress)
       {
            labelInput.text = "[Enter Key]";
            InputControllerUI.ActiveInputSlot = inputSlotNumber;
            StartCoroutine(WaitForKeyPress());
            cInput.ChangeKey(inputSlotNumber, (int)inputType);
        }
    }

    IEnumerator WaitForKeyPress()
    {
        yield return new WaitForSeconds(0.05f);
        while (InputControllerUI.ActiveInputSlot == inputSlotNumber)
        {
            isWaitingForKeyPress = true;

            if (Input.anyKeyDown)
            {
                InputControllerUI.instance.LoadInputList();
                InputControllerUI.ActiveInputSlot = -99;
            }
            yield return null;

        }
        yield return new WaitForSeconds(0.2f);
        isWaitingForKeyPress = false; 

    }

   

}
