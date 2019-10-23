using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Projectile
{
    static GameObject explosionPrefab;

    readonly float explosionRadius;
    float bulletDmg = 12;

    public override void Initialize(Vector3 _firingDir, Vector3 _playerVelocity, float _lifespan, float _speed)
    {
        base.Initialize(_firingDir, _playerVelocity, _lifespan, _speed);
       
        //These calculations apply to all projectiles, maybe it would have been best to put this in Projectile.
    }

    //When bullet hits something, deal damage
    protected override void HitTarget(IHittable targetHit, string layerName)
    {
        targetHit.HitByProjectile(bulletDmg);
    }

    
    protected override void HitNonTarget(Vector3 pos, string layerName)
    {
        base.HitNonTarget(pos, layerName);
        MakeTinyExplosion(pos);
    }

    protected void MakeTinyExplosion(Vector3 pos)
    {
        //lazy initialization
        if (!explosionPrefab)
        {
            explosionPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject.Destroy(explosionPrefab.GetComponent<Collider>());
            explosionPrefab.GetComponent<Renderer>().material.color = Color.yellow;
            explosionPrefab.transform.localScale = Vector3.one * .65f;
            explosionPrefab.SetActive(false); //Since it is not actually a prefab, we deactivate it
        }
        GameObject explosion = GameObject.Instantiate(explosionPrefab);
        explosion.SetActive(true);
        explosion.transform.position = pos;
        GameObject.Destroy(explosion, .08f);
        foreach (Collider c in Physics.OverlapSphere(pos, .65f, LayerMask.GetMask("Enemy", "Building", "Floor", "Wall")))
        {
            IHittable hittable = c.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.HitByProjectile(bulletDmg);
            }
        }
    }
}
