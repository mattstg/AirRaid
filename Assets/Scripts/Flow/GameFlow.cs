using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : IManagable
{
    #region Singleton
    private static GameFlow instance = null;

    private GameFlow()
    {
    }

    public static GameFlow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameFlow();
            }
            return instance;
        }
    }

    #endregion

    
    public void Initialize()
    {
        GameLinks.gl = GameObject.FindObjectOfType<GameLinks>();
        InputManager.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        EnemyManager.Instance.Initialize();
        NPCManager.Instance.Initialize();
        UIManager.Instance.Initialize(PlayerManager.Instance.player);
        BulletManager.Instance.Initialize();
        BuildingManager.Instance.Initialize();
        AudioManager.Instance.Initialize();
    }

    public void PostInitialize()
    {
        InputManager.Instance.PostInitialize();
        PlayerManager.Instance.PostInitialize();
        NPCManager.Instance.PostInitialize();
        EnemyManager.Instance.PostInitialize();
        BulletManager.Instance.PostInitialize();
        BuildingManager.Instance.PostInitialize();
        AudioManager.Instance.PostInitialize();
    }

    public void PhysicsRefresh()
    {
        InputManager .Instance.PhysicsRefresh();
        PlayerManager.Instance.PhysicsRefresh();
        EnemyManager .Instance.PhysicsRefresh();
        NPCManager.Instance.PhysicsRefresh();
        BulletManager.Instance.PhysicsRefresh();
        BuildingManager.Instance.PhysicsRefresh();
        AudioManager.Instance.PhysicsRefresh();
    }

    public void Refresh()
    {
        InputManager.Instance .Refresh();
        PlayerManager.Instance.Refresh();
        EnemyManager.Instance .Refresh();
        NPCManager.Instance.Refresh();
        UIManager.Instance.Refresh(PlayerManager.Instance.player.stats);
        BulletManager.Instance.Refresh();
        BuildingManager.Instance.Refresh();
        AudioManager.Instance.Refresh();
    }

    
}
