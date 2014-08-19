using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputControllerUI : MonoBehaviour
   
{
    /// <summary>
    /// Populates all the input names
    /// </summary>
    public List<UILabel> labelDesc;
    /// <summary>
    /// Populates all inputs
    /// </summary>
    public List<UILabel> labelPrimaryInputs;
    /// <summary>
    /// Populates all secondary Inputs
    /// </summary>
    public List<UILabel> labelSecondaryInputs;
    /// <summary>
    /// Global int to access which Input box is active
    /// </summary>
    public static int ActiveInputSlot = -99;

    public static InputControllerUI instance;

    void Start()
    {
        instance = this;
        LoadInputList();
    
      
    }
  
    public void LoadInputList()
    {
        for (int i = 0; i < GUIConsts.InputCount; i++)
        {
            labelDesc[i].text = GetRegisteredKey(i, 0);
            labelPrimaryInputs[i].text = GetRegisteredKey(i, 1);
            labelSecondaryInputs[i].text = GetRegisteredKey(i, 2);
            Debug.Log(GetRegisteredKey(i, 1));
        }
    }
       
    public void ActivateControlPanel()
    {
       
        UIWindow.GetWindow(7).Show(false);

    }
    private string GetRegisteredKey(int index, int value)
    {
        if (cInput.GetText(index, value) != "" || cInput.GetText(index, value) != null)
            return cInput.GetText(index, value);
        //else
            //return "None";
        return "Empty";

    }

}

