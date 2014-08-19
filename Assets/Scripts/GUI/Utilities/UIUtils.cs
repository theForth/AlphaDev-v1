using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public static class UIUtils 
{
    public static void TriggerPanel(bool condition, int windowID)
    {
        if (condition)
        {
            UIWindow.GetWindow(windowID).Show(false);
        }
        else
        {
            UIWindow.GetWindow(windowID).Hide(false);
        }
    }
}