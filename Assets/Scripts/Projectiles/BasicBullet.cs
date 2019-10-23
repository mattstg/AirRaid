using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Projectile
{
    float bulletDmg = 1;

    public override void Initialize(Vector3 _firingDir, Vector3 _playerVelocity, float _lifespan, float _speed)
    {
        base.Initialize(_firingDir, _playerVelocity, _lifespan, _speed);
       
        //These calculations apply to all projectiles, maybe it would have been best to put this in Projectile.
    }

    //When bullet hits something, deal damage
    protected override void HitTarget(IHittable targetHit)
    {
        targetHit.HitByProjectile(bulletDmg);
    }

}
