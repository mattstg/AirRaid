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
        UIManager.Instance.Initialize(PlayerManager.Instance.player);
    }

    public void PhysicsRefresh()
    {
        InputManager .Instance.PhysicsRefresh();
        PlayerManager.Instance.PhysicsRefresh();
        EnemyManager .Instance.PhysicsRefresh();
        
    }

    public void Refresh()
    {
        InputManager.Instance .Refresh();
        PlayerManager.Instance.Refresh();
        EnemyManager.Instance .Refresh();
        UIManager.Instance.Refresh();
    }

    public void PostInitialize()
    {
        InputManager .Instance.PostInitialize();
        PlayerManager.Instance.PostInitialize();
        EnemyManager .Instance.PostInitialize();
    }
}
