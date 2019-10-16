using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { BasicBullet, Rocket, EnemyFlak, EnemySpit }
public class BulletManager
{

    #region Singleton
    private static BulletManager instance;
    private BulletManager() { }
    public static BulletManager Instance { get { return instance ?? (instance = new BulletManager()); } }
    #endregion

    public Dictionary<ProjectileType, GameObject> prefabDict;  //dictionary containing all prefabs
    public List<Projectile> projectiles = new List<Projectile>();  //Would be more efficent as a hashset, but can you figure out how to implement it?
    Transform bulletParent;

    public void Initialize()
    {
        bulletParent = new GameObject("BulletParent").transform;
        prefabDict = new Dictionary<ProjectileType, GameObject>();
        prefabDict.Add(ProjectileType.BasicBullet, Resources.Load<GameObject>("Prefabs/Bullet"));
        prefabDict.Add(ProjectileType.Rocket, Resources.Load<GameObject>("Prefabs/Rocket"));
        prefabDict.Add(ProjectileType.EnemyFlak, Resources.Load<GameObject>("Prefabs/EnemyFlak"));
        prefabDict.Add(ProjectileType.EnemySpit, Resources.Load<GameObject>("Prefabs/EnemySpit"));
        //See, wouldnt this be better with an auto filling dictionary?
    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {
        //Cannot use the above since we cannot modify a collection as we loop through it, 
        //foreach (Projectile p in projectiles)
        //    p.UpdateProjectile();

        for (int i = projectiles.Count - 1; i >= 0; i--)
            projectiles[i].UpdateProjectile();
    }

    

    public void Refresh()
    {
    }

    //Can be called by UpdateProjectile
    public void ProjectileDied(Projectile pDied)
    {
        projectiles.Remove(pDied);
    }

    //Function to create projectile, returns it incase the caller wants it
    public Projectile CreateProjectile(ProjectileType pType, Vector3 pos, Vector3 firingDir, Vector3 launchersVelocity, float lifespan, float speed)
    {
        Projectile p = GameObject.Instantiate(prefabDict[pType]).GetComponent<Projectile>();
        p.gameObject.transform.position = pos;
        p.gameObject.transform.SetParent(bulletParent);
        p.Initialize(firingDir, launchersVelocity, lifespan, speed);
        projectiles.Add(p);
        return p;
    }

}
