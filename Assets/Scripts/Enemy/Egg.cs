using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Enemy
{
    readonly float EGGSPAWN_ENERGY = 10; //Energy per egg spawn
    readonly Vector2 EGGSPAWN_VELOCITY_RANGE = new Vector2(10, 50);
    readonly float EGGSPAWN_SPAWN_TIME_MAX = 90f;
    readonly float EGGSPAWN_ENERGY_GAIN_PER_ROOT = .3f;

    public AnimationCurve hatchTimeAnimCurve; //balanced in inspector
    RootSystem rootNodeSystem;
    float hatchInterval;
    float timeOfNextHatch;
    float eggEnergy;
    bool isRooted;


    public override void Initialize()
    {
        hatchInterval = EGGSPAWN_SPAWN_TIME_MAX * hatchTimeAnimCurve.Evaluate(Random.value); //another way of using anim curve
        timeOfNextHatch = Time.time + hatchInterval;
        
        base.Initialize();

    }

    public override void Refresh()
    {
        base.Refresh();
        if (isRooted)
        {
            eggEnergy += rootNodeSystem.numberOfRoots * Time.deltaTime * EGGSPAWN_ENERGY_GAIN_PER_ROOT; //More energy relative to number of roots
            rootNodeSystem.Refresh();
        }
        if(isRooted && Time.time >= timeOfNextHatch)
        {
            HatchEgg();
            timeOfNextHatch = Time.time + hatchInterval;
        }
    }

    protected void HatchEgg()
    {
        if (eggEnergy > EGGSPAWN_ENERGY)
        {
            int numberOfEggs = Random.Range(1, (int)eggEnergy / (int)EGGSPAWN_ENERGY);
            for (int i = 0; i < numberOfEggs; i++)
            {
                //Can you think of a better equation for making the egg always launch in a good range? for example always higher than 30 degrees and less than 80 for the x and z axis?
                Vector3 launchDir = Random.onUnitSphere;
                launchDir.y = Mathf.Abs(launchDir.y); //We want the launch direction y to always be postive
                EnemyManager.Instance.CreateEnemyEgg(transform.position + launchDir*1.5f, launchDir, Random.Range(EGGSPAWN_VELOCITY_RANGE.x, EGGSPAWN_VELOCITY_RANGE.y));
                eggEnergy -= EGGSPAWN_ENERGY;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isRooted && !collision.transform.CompareTag("Player"))
        {
            Destroy(GetComponent<Rigidbody>()); //To root it in place, we remove it from the dynamic physics system, since it shouldnt move anymore once rooted
            isRooted = true;
            rootNodeSystem = new RootSystem(transform.position);
        }
    }
}
