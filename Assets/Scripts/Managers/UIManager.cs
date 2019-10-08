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


    public void Initialize(PlayerController _player)
    {
        Debug.Log("UIManager initialize called");
        player = _player;
        for(int i = 0; i < player.stats.abilities.Count; i++)
        {
            //UIAbility.CreateAbilityUI(null,)
        }
    }

   

    public void PostInitialize()
    {
    }

    public void PhysicsRefresh(){}

    public void Refresh()
    {
        PlayerController.Stats statsToUse = player.stats; //Get stats from player to use in UI
        Debug.Log("We would refresh the players UI here");
    }

    
    
}
