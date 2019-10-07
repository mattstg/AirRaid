using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IManagable
{
    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }

    public void Refresh()
    {
    }

    public void PostInitialize()
    {
        
    }
}
