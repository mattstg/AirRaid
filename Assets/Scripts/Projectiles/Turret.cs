using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Projectile, IHittable
{
    PlayerController pc;
    public float range = 15f;

    //Enemy[] allEnemies;
    private Transform head;
    private Transform tBase;
    public float rotationSpeed = 50f;
    bool isEnemyFound = false;
    bool isGrounded = false;
    Vector3 outDirection;
    GameObject closestEnemy = null;
    float cooldown = 0.5f;
    float health = 20;
   
    public void Start()
    {
        //allEnemies = GameObject.FindObjectsOfType<Enemy>();

        head = this.gameObject.transform.GetChild(0);
        tBase = this.gameObject.transform.GetChild(1);

    }

    public void Update()
    {
        cooldown -= Time.deltaTime;
        if (closestEnemy)
            head.forward = Vector3.RotateTowards(head.forward, (closestEnemy.transform.position - head.position).normalized, 2 * Mathf.PI * Time.deltaTime, 0);
        else
            head.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);



        FindTarget();

    }

    public override void UpdateProjectile()
    {
        //base.UpdateProjectile(); special projectile, we want it to use physics instead, it has a rb
        if (Time.time >= timeOfExpire)
            LifespanExpired(); //will contiously trigger if we dont kill projectile
    }
    protected override void LifespanExpired()
    {

         base.LifespanExpired(); //Destroys projectile
        TurretManager.turretList.Remove(this);
        Debug.Log(TurretManager.turretList.Count);
    }
    private void OnCollisionEnter(Collision collision)
    {

        isGrounded = true;

    }

    private void FindTarget()
    {
        isEnemyFound = false;
        float maxDistance = 30f;
        outDirection = head.transform.position;
        Color color = Color.red;


        Collider[] Coll = Physics.OverlapSphere(outDirection, maxDistance, LayerMask.GetMask("Enemy"));

        if (isGrounded)
        {

            if (Coll.Length > 0)
            {
                closestEnemy = Coll[0].gameObject;
                for (int i = 0; i < Coll.Length; i++)
                {
                    if (Vector3.Distance(Coll[i].transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
                    {
                        closestEnemy = Coll[i].gameObject;
                    }
                }
                isEnemyFound = true;
            }


            if (isEnemyFound && cooldown <= 0)
            {
                BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, outDirection, (closestEnemy.transform.position - transform.position).normalized, (closestEnemy.transform.position - transform.position)* 5f, 1, 10);
                cooldown = 0.5f;
            }


        }


    }

    public void HitByProjectile(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TurretManager.turretList.Remove(this);
        }
    }
}













//if (Physics.Raycast(outRay, direction, out rchInfo, maxDistance))
//{


//   Debug.DrawRay(outRay, direction,color);
//    try
//    {
//       if (rchInfo.collider.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
//        {
//         isEnemyFound = true;
//          Debug.Log(rchInfo.collider.transform.name);
//           float nearestEnemy = rchInfo.distance;
//         Vector3 otherDirection = rchInfo.transform.position;
//            foreach (Enemy currentEnemy in allEnemies)
//           {
//                float distance = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
//              if (distance < nearestEnemy)
//               {
//                   distance = nearestEnemy;
//                    Debug.Log(distance);
//                  otherDirection = currentEnemy.transform.position;
//                  Debug.Log(otherDirection);
//                  BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, outRay, otherDirection, otherDirection * 80f, 1, 10);

//                }
//            }

//        }
//    }
//    catch(System.Exception e)
//    {
//        Debug.Log(e.ToString());
//    }

//}