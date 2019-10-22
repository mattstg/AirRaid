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

    //store links
    public GameObject storePanel;
    public GameObject sellGrid;
    public Text sellAmount;
    public GameObject descriptionPanel;
    public Text itemTitle;
    public Text description;
    public Text cost;
    public GameObject switchInventoryType;
    public GameObject bodyPartPanel;
    public Text goTo;
    public Text title;
    public Image energyBarStore;
    public Text energyTextStore;
    public Text errorMessage;

    public static UILinks Instance; //The first instance to be created will set this static variable to point at it, so we can access it from everywhere
    public static UILinks instance  //Pseudo singleton for mono behaviour
    {
        get {
            return Instance ?? (Instance = GameObject.FindObjectOfType<UILinks>());
            }
    }

}
