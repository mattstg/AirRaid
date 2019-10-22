using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    readonly float BOMB_DMG = 250;
    public override void UpdateProjectile()
    {
        //base.UpdateProjectile(); special projectile, we want it to use physics instead, it has a rb
        if (Time.time >= timeOfExpire)
            LifespanExpired(); //will contiously trigger if we dont kill projectile
    }
    protected override void LifespanExpired()
    {
        foreach (Collider c in Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Enemy", "Building", "Floor", "Wall", "Player")))
        {
            IHittable hittable = c.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.HitByProjectile(BOMB_DMG);
            }
        }
        base.LifespanExpired(); //Destroys projectile
    }
}
