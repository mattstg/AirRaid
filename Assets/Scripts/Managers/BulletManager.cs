using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { BasicBullet, Rocket }
public class BulletManager
{

    #region Singleton
    private static BulletManager instance;
    private BulletManager() { }
    public static BulletManager Instance { get { return instance ?? (instance = new BulletManager()); } }
    #endregion

    public HashSet<Projectile> projectiles = new HashSet<Projectile>();

    public void Initialize()
    {
    }

    public void PostInitialize()
    {
    }

    public void PhysicsRefresh()
    {
        foreach (Projectile p in projectiles)
            p.UpdateProjectile();
    }

    

    public void Refresh()
    {
    }

    public void ProjectileDied(Projectile pDied)
    {

    }

    public void CreateProjectile(ProjectileType pType)
    {
        
        switch (pType)
        {
            case ProjectileType.BasicBullet:
                break;
            case ProjectileType.Rocket:
                break;
            default:
                Debug.LogError("Unhandled switch: " + pType);
                break;
        }
    }

}
