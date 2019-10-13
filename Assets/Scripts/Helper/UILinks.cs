using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinks : MonoBehaviour
{
    //editor links
    public Image energyBar;
    public Text energyText;
    public Image healthBar;
    public Text healthText;
    public Transform abilityGridParent;
    public Text speedText;
    public Slider speedEnergyCostThreshold;

    public static UILinks Instance; //The first instance to be created will set this static variable to point at it, so we can access it from everywhere
    public static UILinks instance  //Pseudo singleton for mono behaviour
    {
        get {
            return Instance ?? (Instance = GameObject.FindObjectOfType<UILinks>());
            }
    }

}
