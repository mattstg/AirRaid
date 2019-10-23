using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Egg, EggSpitter, AATurret, Crawler}
public class EnemyManager
{
    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    Transform enemyParent;
    public Transform rootNodeParent;    
    public HashSet<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;
    readonly float initialEggSpawnHeight = 50;
    public static GameObject rootPrefab;

    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs


    public void Initialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new HashSet<Enemy>();
        rootPrefab = Resources.Load<GameObject>("Prefabs/RootNode");
        enemyParent = new GameObject("EnemyParent").transform;
        rootNodeParent = new GameObject("RootNodeParent").transform;
        foreach(EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }

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
            try {
                Enemy e = toRemove.Pop();
                enemies.Remove(e);
                GameObject.Destroy(e.gameObject);
            }
            catch {
                Debug.Log("hey this happened");
            }
        }

        while (toAdd.Count > 0) //Add new ones
            enemies.Add(toAdd.Pop());
    }

   

    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);

    }

    

    public Enemy SpawnEnemy(EnemyType eType, Vector3 spawnLoc, float startingEnergy)
    {
        if (eType == EnemyType.Egg)
        {
            Debug.LogError("Do not use SpawnEnemy to spawn an egg, use CreateEnemyEgg instead, eggs require more parmeters");
            return CreateEnemyEgg(spawnLoc,new Vector3(),0,startingEnergy);
        }

        GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[eType],enemyParent);       //create from prefab
        newEnemy.transform.position = spawnLoc;     //set the position
        Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
        e.Initialize(startingEnergy);               //initialize the enemy
        toAdd.Push(e);                              //add to update list
        return e;
    }


    //Used to start the game, summons eggs from the sky which lands on a each defined random planes
    public void SpawnInitialSkyEggs()
    {
        foreach(Transform spawnPlane in GameLinks.gl.spawnLocationParent)  //foreach child in the spawn location parent
        {
            //spawn an egg and launch it towards the random spot
            Vector3 spawnLocation = new Vector3(Random.Range(-spawnPlane.localScale.x*10 / 2, spawnPlane.localScale.x*10 / 2), initialEggSpawnHeight, Random.Range(-spawnPlane.localScale.z*10 / 2, spawnPlane.localScale.z*10 / 2));
            CreateEnemyEgg(spawnLocation + new Vector3(spawnPlane.transform.position.x, 0, spawnPlane.transform.position.z), -Vector3.up, 1, 5); //gravity will drop them anyways
        }
        GameObject.Destroy(GameLinks.gl.spawnLocationParent.gameObject);
    }

    public Enemy CreateEnemyEgg(Vector3 spawnPos, Vector3 spawnDir, float speed, float startEnergy)
    {
        GameObject newEgg = GameObject.Instantiate(enemyPrefabDict[EnemyType.Egg]);
        newEgg.transform.position = spawnPos;
        newEgg.transform.SetParent(enemyParent);
        newEgg.GetComponent<Rigidbody>().velocity = spawnDir.normalized * speed;
        Enemy e = newEgg.GetComponent<Enemy>();
        e.Initialize(startEnergy);
        toAdd.Push(e);
        return e;
    }
}
