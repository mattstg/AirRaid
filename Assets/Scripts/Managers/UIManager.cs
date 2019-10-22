using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance;
     #region Singleton
    private UIManager() { }
    public static UIManager Instance { get { return instance ?? (instance = new UIManager()); } }
    #endregion

    PlayerController player;
    UILinks ui;  //still the same ui links. just a shortcut for less typing

    [HideInInspector] public bool isErrorMessageActive; //so it doesnt have to check everytime the gameobject
    private float cooldownTime = 5f;
    private float timeLeft;

    public void Initialize(PlayerController _player)
    {
        ui = UILinks.instance;
        player = _player;
        for(int i = 0; i < player.stats.abilities.Count; i++)
        {
            //UIAbility.CreateAbilityUI(null,)
        }
    }

   

    public void PostInitialize()
    {
        timeLeft = cooldownTime;
    }

    public void PhysicsRefresh() {}

    public void Refresh(PlayerController.PlayerStats statsToUse)
    {
        //PlayerController.PlayerStats statsToUse = player.stats; //Get stats from player to use in UI
        if (statsToUse.player.isAlive)
        {
            ui.energyBar.fillAmount = Mathf.Clamp01(statsToUse.currentEnegy / statsToUse.maxEnergy);
            ui.energyBarStore.fillAmount = Mathf.Clamp01(statsToUse.currentEnegy / statsToUse.maxEnergy);
            ui.energyTextStore.text = $"{statsToUse.currentEnegy.ToString("00.0")} Energy";
            ui.energyText.text = $"{statsToUse.currentEnegy.ToString("00.0")}/{statsToUse.maxEnergy.ToString("00.0")}";
            ui.healthBar.fillAmount = Mathf.Clamp01(statsToUse.hp / statsToUse.maxEnergy);
            ui.healthText.text = $"{statsToUse.hp.ToString("00.0")}/{statsToUse.maxHp.ToString("00.0")}";
            //ui.abilityGridParent;
            ui.speedText.text = statsToUse.relativeLocalVelo.z.ToString();
            ui.speedEnergyCostThreshold.value = statsToUse.speedPerctangeThresholdToCostEnergy;
            if (StoreManager.Instance.openStore)
            {
                ui.storePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else 
            {
                ui.storePanel.SetActive(false);
                Time.timeScale = 1;
            }
            //doesnt work because of time scale is set to 0, rather than setting time scale to 0 stop everything else from working in gameflow
            if (isErrorMessageActive) {
                if (timeLeft <= 0) {
                    timeLeft -= Time.deltaTime;
                } else {
                    UILinks.instance.errorMessage.text = "Error Message";
                    UILinks.instance.errorMessage.gameObject.SetActive(false);
                    isErrorMessageActive = false;
                    timeLeft = cooldownTime;
                }
            }
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
