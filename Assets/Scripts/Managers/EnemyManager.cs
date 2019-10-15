using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager
{
    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    Transform enemyParent;
    GameObject enemyEggPrefab;
    public HashSet<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;
    readonly float initialEggSpawnHeight = 50;

    public void Initialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new HashSet<Enemy>();
        enemyEggPrefab = Resources.Load<GameObject>("Prefabs/Egg");
        enemyParent = new GameObject("EnemyParent").transform;
        SpawnInitialSkyEggs();
    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {
        foreach (Enemy e in enemies)
            if (e.isAlive)
                e.PhysicRefresh();
    }

    public void Refresh()
    {
        foreach (Enemy e in enemies)
            if (e.isAlive)
                e.Refresh();


        while (toRemove.Count > 0) //remove all dead ones
        {
            Enemy e = toRemove.Pop();
            enemies.Remove(e);
            GameObject.Destroy(e.gameObject);
        }

        while (toAdd.Count > 0) //Add new ones
            enemies.Add(toAdd.Pop());
    }

   

    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);

    }

    //Used to start the game, summons eggs from the sky which lands on a each defined random planes
    public void SpawnInitialSkyEggs()
    {
        foreach(Transform spawnPlane in GameLinks.gl.spawnLocationParent)  //foreach child in the spawn location parent
        {
            //spawn an egg and launch it towards the random spot
            Vector3 spawnLocation = new Vector3(Random.Range(-spawnPlane.localScale.x*10 / 2, spawnPlane.localScale.x*10 / 2), initialEggSpawnHeight, Random.Range(-spawnPlane.localScale.z*10 / 2, spawnPlane.localScale.z*10 / 2));
            CreateEnemyEgg(spawnLocation + new Vector3(spawnPlane.transform.position.x, 0, spawnPlane.transform.position.z), -Vector3.up, 1); //gravity will drop them anyways
        }
        GameObject.Destroy(GameLinks.gl.spawnLocationParent.gameObject);
    }

    public void CreateEnemyEgg(Vector3 spawnPos, Vector3 spawnDir, float speed)
    {
        GameObject newEgg = GameObject.Instantiate(enemyEggPrefab);
        newEgg.transform.position = spawnPos;
        newEgg.transform.SetParent(enemyParent);
        newEgg.GetComponent<Rigidbody>().velocity = spawnDir.normalized * speed;
        Enemy e = newEgg.GetComponent<Enemy>();
        e.Initialize();
        toAdd.Push(e);
    }
}
