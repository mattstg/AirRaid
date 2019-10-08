using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinks : MonoBehaviour
{
    //editor links
    public Image energyBar;
    public Image healthBar;




    public static UILinks instance; //The first instance to be created will set this static variable to point at it, so we can access it from everywhere

    
    //This is called by gameflow so it can setup it's static link, this UILink will be attached to an object (mainScripts)
    public void Initialize()
    {
        if (instance != null)
            Debug.LogError("UILinks was initialized twice, there should only be ever one UILinks or is being called twice");
        instance = this;
    }
}
