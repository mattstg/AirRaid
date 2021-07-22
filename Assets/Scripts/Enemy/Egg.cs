using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : RootedEnemy
{
    static readonly float EGGHATCH_ENERGY_MIN = 10; //Energy per egg spawn
    static readonly float EGGSPAWN_SPAWN_TIME_MAX = 90f;

    public bool specialSkyEgg = false; //The first ever eggs are only egg spitters, sky eggs force this true
    public AnimationCurve hatchTimeAnimCurve; //balanced in inspector
    float timeOfHatch;


    public override void Initialize(float startingEnergy, Vector3 pos)
    {
        base.Initialize(startingEnergy, pos);
        timeOfHatch = Time.time +  EGGSPAWN_SPAWN_TIME_MAX * hatchTimeAnimCurve.Evaluate(Random.value); //another way of using anim curve
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
            Enemy e; //New enemy created from egg, is null at this moment, create using EnemyManager below

            //Special rule, in first minute of the game, they all become egg spitters
            if (specialSkyEgg)
            {
                //Hatch an egg spitter
                e = EnemyManager.Instance.SpawnEnemy(EnemyType.EggSpitter, transform.position, energy); //Spawn an egg spitter on this egg's location
                ((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);  //The egg spitter will inherit the egg's root system
            }
            else
            {
                float eggHatchChoice = Random.value;
                if (eggHatchChoice >= .5f && eggHatchChoice <= 1f)  //50% chance, magic values are terrible, was it harder to find than if it was a constant at the top? Thats why we use constants
                {
                    e = EnemyManager.Instance.SpawnEnemy(EnemyType.Crawler, transform.position, energy); //Spawn an egg spitter on this egg's location
                    //Hatch a crawler
                }
                else if (eggHatchChoice >= .2 && eggHatchChoice <= .5f) //30% chance
                {
                    //Hatch a AA Turret
                    e = EnemyManager.Instance.SpawnEnemy(EnemyType.AATurret, transform.position, energy); //Spawn an egg spitter on this egg's location
                    ((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);  //The egg spitter will inherit the egg's root system
                }
                else  //20% chance
                {
                    //Hatch an egg spitter
                    e = EnemyManager.Instance.SpawnEnemy(EnemyType.EggSpitter, transform.position, energy); //Spawn an egg spitter on this egg's location
                    ((RootedEnemy)e).LinkToRootSystem(rootNodeSystem);  //The egg spitter will inherit the egg's root system
                }
            }
            Die();   //Destroy this egg since it hatched, cannot just gameObject destroy since manager has the link, so killed it properly
        }
        else
            timeOfHatch = Time.time + EGGSPAWN_SPAWN_TIME_MAX * hatchTimeAnimCurve.Evaluate(Random.value);
    }

    public override void Die()
    {
        //Dont want to call rootEnemy die, will destroy the rootNode system!
        EnemyManager.Instance.EnemyDied(this);
        isAlive = false;
        //base.Die();
    }

}
