using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrerBehavior : MonoBehaviour
{
    //Turret Variables
    public float turretTimer = 0f;
    private GameObject turretPrefab;
    private Transform turretParent;
    LayerMask ennemyLayer;
    private bool targetFound;
    private GameObject target;
    private float targetDistance;

    readonly float TURRET_RANGE = 45;
    readonly float TURRET_PROJECTILE_SPEED = 40;

    // Start is called before the first frame update
    void Start()
    {
        targetFound = false;
        ennemyLayer = 1 << 13;

    }
    private void Update()
    {
        
        if (targetFound)
        {
            Shoot();
        }
        else
        {
            DetectEnnemy();
        }
    }
   

    public void DetectEnnemy()
    {
        Collider[] colliderTab = Physics.OverlapSphere(gameObject.transform.position, TURRET_RANGE, ennemyLayer);

        foreach(Collider enemy in colliderTab)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(Physics.Raycast(transform.position,(enemy.gameObject.transform.position - transform.position).normalized, out RaycastHit hit, TURRET_RANGE))
            {
                if(hit.collider.gameObject == enemy)
                {
                    if (target == null)
                    {
                        target = enemy.gameObject;
                        targetDistance = distance;
                        targetFound = true;
                    }else if (distance < targetDistance)
                    {
                        target = enemy.gameObject;
                        targetDistance = distance;
                        targetFound = true;
                    }
                }
            }
        }

        

    }

    public void Shoot()
    {
        Vector3 aimingVector = target.transform.position - transform.position;
        //The above gives us a perfect aiming vector, but I want some inaccuracy! 

        //https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html
        aimingVector = (Quaternion.Euler(Random.Range(-10, 10f), 0, Random.Range(-10, 10f)) * aimingVector).normalized;  //This is how we rotate a vector 10 degrees randomly around both x and z

        //Calculate the lifespan of the flak, it should explode near the player
        //distance divided by speed would give us time to explode  (draw the math with the correct units on paper if unsure, you will see)
        float lifespan = Vector3.Distance(PlayerManager.Instance.player.transform.position, transform.position) / TURRET_PROJECTILE_SPEED;

        Projectile p = BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.position, aimingVector, Vector3.zero, lifespan, TURRET_PROJECTILE_SPEED);
    }

   
}
