using System.Collections;
using System.Collections.Generic;
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


    public void Initialize(PlayerController _player)
    {
        ui = UILinks.instance;
        player = _player;
        //for(int i = 0; i < player.stats.abilities.Count; i++)
        //{
        //    //UIAbility.CreateAbilityUI(null,)
        //}
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
            ui.energyBar.fillAmount = Mathf.Clamp01(statsToUse.currentEnegy / statsToUse.maxEnergy);
            ui.energyText.text = $"{statsToUse.currentEnegy.ToString("00.0")}/{statsToUse.maxEnergy.ToString("00.0")}";
            ui.healthBar.fillAmount = Mathf.Clamp01(statsToUse.hp / statsToUse.maxEnergy);
            ui.healthText.text = $"{statsToUse.hp.ToString("00.0")}/{statsToUse.maxHp.ToString("00.0")}";
            //ui.abilityGridParent;
            ui.speedText.text = statsToUse.relativeLocalVelo.z.ToString();

        }
        else
        {
            ui.energyBar.fillAmount = 0;
            ui.healthBar.fillAmount = 0;
            //ui.abilityGridParent;
            ui.speedText.text = "0";
            ui.speedEnergyCostThreshold.value = 0;
        }
}

    
    
}
