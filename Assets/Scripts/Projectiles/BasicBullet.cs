using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Projectile
{
    Vector3 trueBulletDirectionVector;
    float bulletSpeed = 250;
    float bulletDmg = 1;

    public override void Initialize(Vector3 _firingDir, Vector3 _playerVelocity)
    {
        base.Initialize(_firingDir, _playerVelocity);
        trueBulletDirectionVector = (firingDir * bulletSpeed + playerVelocityOnLaunch).normalized;  //Its own speed, but also the speed the player shot it at
        bulletSpeed = (firingDir * bulletSpeed + playerVelocityOnLaunch).magnitude;                 //the players initial speed needs to be taken into account
    }

    public override void UpdateProjectile()
    {
        base.UpdateProjectile();

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, trueBulletDirectionVector, out rayHit, bulletSpeed*Time.fixedDeltaTime,LayerMask.GetMask("Enemy","Building","Floor", "Wall")))
        {
            rayHit.transform.GetComponent<IHittable>()?.HitByProjectile(bulletDmg);  //At the moment, only one building has IHittable, for testing purposes
            DestroyProjectile();
        }
        else
        {
            transform.position += trueBulletDirectionVector * bulletSpeed* Time.fixedDeltaTime;  //else move forward
        }
    }

}
