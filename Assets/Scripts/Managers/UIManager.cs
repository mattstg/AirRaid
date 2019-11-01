using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager 
{
    #region Singleton
    private static UIManager instance;
    private UIManager() { }
    public static UIManager Instance { get { return instance ?? (instance = new UIManager()); } }
    #endregion

    PlayerController player;
    UILinks ui;  //still the same ui links. just a shortcut for less typing
    Ability _ability;
    [HideInInspector]
    public LivesManager lm;
    [HideInInspector]
    public GameObject[] AbilityIMG;



    public void Initialize(PlayerController _player)
    {
        ui = UILinks.instance;
        //_ability = Ability
        player = _player;
        for(int i = 0; i < player.stats.abilities.Count; i++)
        {
            //UIAbility.CreateAbilityUI(null,)
        }
        lm = GameObject.FindGameObjectWithTag("LivesManager").GetComponent<LivesManager>();
        AbilityIMG  = GameObject.FindGameObjectsWithTag("AbilityIcon");
    }

   

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh(){}

    public void Refresh(PlayerController.PlayerStats statsToUse)
    {
        //PlayerController.PlayerStats statsToUse = player.stats; //Get stats from player to use in UI
        if (statsToUse.player.isAlive)
        {
            //Changes fill amount of EnergyBar Slider
            ui.energyBar.value = Mathf.Clamp01(statsToUse.currentEnegy / statsToUse.maxEnergy);
            ui.energyText.text = $"{statsToUse.currentEnegy.ToString("00.0")}/{statsToUse.maxEnergy.ToString("00.0")}";
            //Changes fill amount of HealthBar Slider
            ui.healthBar.value = Mathf.Clamp01(statsToUse.hp / statsToUse.maxEnergy);
            ui.healthText.text = $"{statsToUse.hp.ToString("00.0")}/{statsToUse.maxHp.ToString("00.0")}";
            //ui.abilityGridParent;
            ui.speedText.text = statsToUse.relativeLocalVelo.z.ToString();
            ui.TurretImage.fillAmount = player.abilityManager.abilities[0].stats.chargePercentage;
            ui.BombImage.fillAmount = player.abilityManager.abilities[1].stats.chargePercentage;
            ui.Speedometer.fillAmount = float.Parse(statsToUse.relativeLocalVelo.z.ToString())/player.stats.maxSpeed; //Sets Raange of speedometer depending on maxSPeed vs currentSpeed
            ui.DropTurretImage.fillAmount = player.abilityManager.abilities[2].stats.chargePercentage;
            ui.RewindImage.fillAmount = player.abilityManager.abilities[3].stats.chargePercentageRewind;
        }
        else
        {
            ui.energyBar.value = 0;
            ui.healthBar.value = 0;
            //ui.abilityGridParent;
            ui.speedText.text = "0";
            ui.speedEnergyCostThreshold.value = 0;
            Initialize(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>());
        }

        player = GameObject.FindObjectOfType<PlayerController>();
}

    
    
}
