using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AATurret : RootedEnemy
{
    readonly float AATURRET_FIRE_ENERGY_MIN = 2;
    readonly Vector2 AATURRET_FIRE_RATE = new Vector2(2f, 7);
    readonly float AATURRET_RANGE = 90;
    readonly float AATURRET_PROJECTILE_SPEED = 40;

    float timeNextShotReady;
    bool canFire { get { return Time.time >= timeNextShotReady; } }

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        timeNextShotReady = Time.time + Random.Range(AATURRET_FIRE_RATE.x, AATURRET_FIRE_RATE.y);
    }

    public override void Refresh()
    {
        base.Refresh();

        if(isRooted)
            if(canFire && energy > AATURRET_FIRE_ENERGY_MIN && Vector2.Distance(PlayerManager.Instance.player.transform.position,transform.position) <= AATURRET_RANGE)
            {
                float energySpend = energy*Random.Range(.1f, .8f); //Shoot between 10% and 80% energy
                ShootAtPlayer(energySpend);
                timeNextShotReady = Time.time + Random.Range(AATURRET_FIRE_RATE.x, AATURRET_FIRE_RATE.y);
                energy -= energySpend;
            } 

    }

    private void ShootAtPlayer(float energyIntoShot)
    {
        Vector3 aimingVector = PlayerManager.Instance.player.transform.position - transform.position;
        //The above gives us a perfect aiming vector, but I want some inaccuracy! 

        //https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html
        aimingVector = (Quaternion.Euler(Random.Range(-10, 10f), 0, Random.Range(-10, 10f)) * aimingVector).normalized;  //This is how we rotate a vector 10 degrees randomly around both x and z

        //Calculate the lifespan of the flak, it should explode near the player
        //distance divided by speed would give us time to explode  (draw the math with the correct units on paper if unsure, you will see)
        float lifespan = Vector3.Distance(PlayerManager.Instance.player.transform.position, transform.position) / AATURRET_PROJECTILE_SPEED;

        Projectile p = BulletManager.Instance.CreateProjectile(ProjectileType.EnemyFlak, transform.position, aimingVector, Vector3.zero, lifespan, AATURRET_PROJECTILE_SPEED);
        ((EnemyFlak)p).InitializeEnemyFlak(energyIntoShot);

    }

}
