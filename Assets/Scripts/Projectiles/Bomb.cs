using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    readonly float BOMB_DMG = 250;
    readonly float BOMB_RAIDUS = 7;
    GameObject explosionPrefab;
    public override void UpdateProjectile()
    {
        //base.UpdateProjectile(); special projectile, we want it to use physics instead, it has a rb
        if (Time.time >= timeOfExpire)
            LifespanExpired(); //will contiously trigger if we dont kill projectile
    }
    protected override void LifespanExpired()
    {
        MakeExplosion(transform.position);
        base.LifespanExpired(); //Destroys projectile
    }

    protected void MakeExplosion(Vector3 pos)
    {
        //lazy initialization
        if (!explosionPrefab)
        {
            explosionPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject.Destroy(explosionPrefab.GetComponent<Collider>());
            explosionPrefab.GetComponent<Renderer>().material.color = Color.yellow;
            explosionPrefab.transform.localScale = Vector3.one * BOMB_RAIDUS * 2;
            explosionPrefab.SetActive(false); //Since it is not actually a prefab, we deactivate it
        }
        GameObject explosion = GameObject.Instantiate(explosionPrefab);
        explosion.SetActive(true);
        explosion.transform.position = pos;
        GameObject.Destroy(explosion, .2f);
        foreach (Collider c in Physics.OverlapSphere(pos, BOMB_RAIDUS, LayerMask.GetMask("Enemy", "Building", "Floor", "Wall")))
        {
            IHittable hittable = c.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.HitByProjectile(BOMB_DMG);
            }
        }
    }
}
