using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpitter : RootedEnemy
{
    readonly int SPAWN_EGG_MAX = 4;
    readonly int EGGSPAWN_ENERGY = 10;                          //amount of energy to spawn an egg, half of that energy is given to the new egg
    readonly Vector2 EGGSPAWN_VELOCITY_RANGE = new Vector2(10, 20); //How fast to yeet an egg when created, random range
    readonly Vector2 EGGSPAWN_COOLDOWN = new Vector2(20, 90); //How long between egg creations, random range

    float timeToNextEggSpit;

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        timeToNextEggSpit = Time.time + Mathf.Lerp(EGGSPAWN_COOLDOWN.x, EGGSPAWN_COOLDOWN.y, Random.value); //Another way of producing a random value between two values, just like Random.range

    }

    public override void Refresh()
    {
        base.Refresh();
        if (isRooted && Time.time >= timeToNextEggSpit)
        {
            SpawnEggs();
            timeToNextEggSpit = Time.time + Mathf.Lerp(EGGSPAWN_COOLDOWN.x, EGGSPAWN_COOLDOWN.y, Random.value); //Another way of producing a random value between two values, just like Random.range
        }
    }

    protected void SpawnEggs()
    {
        if (energy > EGGSPAWN_ENERGY)
        {
            int numberOfEggs = Mathf.Clamp(Random.Range(1, (int)energy / (int)EGGSPAWN_ENERGY),0, SPAWN_EGG_MAX);
            for (int i = 0; i < numberOfEggs; i++)
            {
                //Can you think of a better equation for making the egg always launch in a good range? for example always higher than 30 degrees and less than 80 for the x and z axis?
                Vector3 launchDir = Random.onUnitSphere;
                launchDir.y = Mathf.Abs(launchDir.y); //We want the launch direction y to always be postive
                EnemyManager.Instance.CreateEnemyEgg(transform.position + launchDir * 1.5f, launchDir, Random.Range(EGGSPAWN_VELOCITY_RANGE.x, EGGSPAWN_VELOCITY_RANGE.y), EGGSPAWN_ENERGY/2f);
                energy -= EGGSPAWN_ENERGY;
            }
            Resize();
        }
    }
    
}
