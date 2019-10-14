using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    GameObject physicalProjectile;  //not all projectiles will have physical versions
    Vector3 projPosition;           //will not be set if projectile doesnt have physical version
    public virtual Vector3 ProjectilePos { set { projPosition = value; } get { return (physicalProjectile) ? physicalProjectile.transform.position : projPosition; } }   //used when the projectile does not have a visual version
        
    public Projectile()
    {

    }

    public virtual void UpdateProjectile()
    {

    }

    public virtual void DestroyProjectile()
    {
        BulletManager.Instance.ProjectileDied(this);
    }

}
