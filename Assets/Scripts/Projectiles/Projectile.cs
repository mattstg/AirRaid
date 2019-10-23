using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 firingDir;
    protected Vector3 currentMovementVector;
    protected float projectileSpeed;
    protected Vector3 playerVelocityOnLaunch;  //to be added to the speed
    float timeOfExpire;

    public virtual void Initialize(Vector3 _firingDir, Vector3 _playerVelocityOnLaunch, float _lifespan, float _speed)
    {
        firingDir = _firingDir.normalized;  //just in case the caller didnt give us a normalized one, but they should
        playerVelocityOnLaunch = _playerVelocityOnLaunch;
        timeOfExpire = _lifespan + Time.time;
        projectileSpeed = _speed;
        currentMovementVector = (firingDir * projectileSpeed + playerVelocityOnLaunch).normalized; //Its own speed, but also the speed the shooters shot it at
        projectileSpeed = (firingDir * projectileSpeed + playerVelocityOnLaunch).magnitude;                 //the shooters initial speed needs to be taken into account
    }

    public virtual void UpdateProjectile()
    {
        if (Time.time >= timeOfExpire)
            LifespanExpired(); //will contiously trigger if we dont kill projectile

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, currentMovementVector, out rayHit, projectileSpeed * Time.fixedDeltaTime, LayerMask.GetMask("Enemy", "Building", "Floor", "Wall","Map")))
        {
            IHittable ihittable = rayHit.transform.GetComponent<IHittable>();  //If the thing we hit has implemented "IHittable"
            if (ihittable != null)
                HitTarget(ihittable, LayerMask.LayerToName(rayHit.transform.gameObject.layer));
            else
                HitNonTarget(rayHit.point, LayerMask.LayerToName(rayHit.transform.gameObject.layer));
            DestroyProjectile();
        }
        else
        {
            transform.position += currentMovementVector * projectileSpeed * Time.fixedDeltaTime;  //else move forward
        }
    }

    protected virtual void LifespanExpired()
    {
        DestroyProjectile();
    }

    
    protected virtual void HitTarget(IHittable targetHit, string layerName)
    {
        //implement in child
    }

    //Things without IHittable, for example, floor
    protected virtual void HitNonTarget(Vector3 pos, string layerName)
    {
        //implement in child

        if(layerName == "Map")
        {
            DestroyProjectile();
        }
    }

    public virtual void DestroyProjectile()
    {
        BulletManager.Instance.ProjectileDied(this);
        GameObject.Destroy(gameObject);
    }

}
