using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : RootedEnemy
{
    static readonly float EGGHATCH_ENERGY_MIN = 10; //Energy per egg spawn
    static readonly float EGGSPAWN_SPAWN_TIME_MAX = 90f;


    public AnimationCurve hatchTimeAnimCurve; //balanced in inspector
    float timeOfHatch;

    [SerializeField] GameObject targetPrefab;
    Transform targetParent;

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        timeOfHatch = Time.time +  EGGSPAWN_SPAWN_TIME_MAX * hatchTimeAnimCurve.Evaluate(Random.value); //another way of using anim curve
      /*
        targetParent = new GameObject().transform;
        targetPrefab = Resources.Load<GameObject>("Prefabs/TargetPoint");
        */
    }

    public override void Refresh()
    {
        base.Refresh();

        if(isRooted && Time.time >= timeOfHatch)
            HatchEgg();
    }

    protected void HatchEgg()
    {
        if (energy > EGGHATCH_ENERGY_MIN)
        {
            //Special rule, in first minute of the game, they all become egg spitters
            if (Time.time < 60)
            {
                //Hatch an egg spitter
                Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.EggSpitter, transform.position, energy); //Spawn an egg spitter on this egg's location
                ((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);  //The egg spitter will inherit the egg's root system
            }
            else
            {
                float eggHatchChoice = Random.value;
                if (eggHatchChoice >= 0f && eggHatchChoice <= .1f)  //10% chance
                {
                    Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.Crawler, transform.position, energy); //Spawn an egg spitter on this egg's location
                    //Hatch a crawler
                }
                else if (eggHatchChoice >= .1 && eggHatchChoice <= .3f) //20% chance
                {
                    //Hatch a AA Turret
                    Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.AATurret, transform.position, energy); //Spawn an egg spitter on this egg's location
                    ((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);  //The egg spitter will inherit the egg's root system
                }
                else if (eggHatchChoice >= .3 && eggHatchChoice <= .5f)  //20% chance
                {
                    //Hatch an egg spitter
                    Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.EggSpitter, transform.position, energy); //Spawn an egg spitter on this egg's location
                    ((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);  //The egg spitter will inherit the egg's root system
                }
                else if(eggHatchChoice >= .5 && eggHatchChoice <= .7f) // 20% chance
                {
                    Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.Fighter, transform.position /*new Vector3(transform.position.x, 100f, transform.position.z)*/, energy); //Spawn an egg spitter on this egg's location
                    //((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);
                }
                else if (eggHatchChoice >= .7 && eggHatchChoice <= .75f) // 5% chance
                {
                    Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.Bomber, transform.position + new Vector3(transform.position.x, 100f, transform.position.z), energy); //Spawn an egg spitter on this egg's location
                    SpawnTarget();
                }
                else //Remaining probabilities
                {
                    Enemy e = EnemyManager.Instance.SpawnEnemy(EnemyType.Ghoul, transform.position + new Vector3(transform.position.x, 100f, transform.position.z), energy); //Spawn an egg spitter on this egg's location

                }
            }
            Die();   //Destroy this egg since it hatched, cannot just gameObject destroy since manager has the link, so killed it properly
        }
        else
            timeOfHatch = Time.time + EGGSPAWN_SPAWN_TIME_MAX * hatchTimeAnimCurve.Evaluate(Random.value);
    }

    public override void Die() {
        EnemyManager.Instance.EnemyDied(this);
        isAlive = false;
    }
   public void SpawnTarget()
    {
        GameObject newTarget = GameObject.Instantiate(targetPrefab, targetParent);
        newTarget.transform.position = new Vector3(Random.Range(-500, 600), Random.Range(100, 200), Random.Range(-800, 1000));
    }
}
