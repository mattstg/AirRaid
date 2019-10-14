using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Enemy
{
    readonly float EGGSPAWN_ENERGY = 10; //Energy per egg spawn
    readonly Vector2 EGGSPAWN_VELOCITY_RANGE = new Vector2(10, 50);
    readonly Vector2 EGGSPAWN_SPAWN_RANGE = new Vector2(5, 20);

    public AnimationCurve hatchTimeAnimCurve; //balanced in inspector
    float hatchInterval;
    float timeOfNextHatch;
    float eggEnergy;
    

    public override void Initialize()
    {
        hatchInterval = Mathf.Lerp(EGGSPAWN_SPAWN_RANGE.x, EGGSPAWN_SPAWN_RANGE.y, hatchTimeAnimCurve.Evaluate(Random.value));
        timeOfNextHatch = Time.time + hatchInterval;
        base.Initialize();

    }

    public override void Refresh()
    {
        base.Refresh();
        eggEnergy += Time.deltaTime; //Atm, just for testing, have eggs grow in energy 1 per second.
        if(Time.time >= timeOfNextHatch)
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
                Vector3 launchDir = Random.onUnitSphere;
                launchDir.y = Mathf.Abs(launchDir.y); //We want the launch direction y to always be postive,
                EnemyManager.Instance.CreateEnemyEgg(transform.position, launchDir, Random.Range(EGGSPAWN_VELOCITY_RANGE.x, EGGSPAWN_VELOCITY_RANGE.y));
                eggEnergy -= EGGSPAWN_ENERGY;
            }
        }
    }


}
