using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret :MonoBehaviour, IHittable
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
    float hp = 20;
    public  float timeOfExpire;
    AudioSource turretSound;

    public void Initialize()
    {
            head = this.gameObject.transform.GetChild(0);
        turretSound = GameObject.Find("AudioObject3").GetComponent<AudioSource>();
    }
    public void PostInitialize()
    {

    }

    public void Refresh()
    {
        cooldown -= Time.deltaTime;
        if (closestEnemy)
            head.forward = Vector3.RotateTowards(head.forward, (closestEnemy.transform.position - head.position).normalized, 2 * Mathf.PI * Time.deltaTime, 0);
        else
            head.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

        FindTarget();
        LifespanExpired();
        
    }
    public void PhysicsRefresh()
    {
       
    }


    protected virtual void LifespanExpired()
    {

        if ((timeOfExpire -= Time.deltaTime) <=  0)
        {
            TurretManager.Instance.turrets.Remove(this);
            GameObject.Destroy(gameObject);
        }
        
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
                turretSound.transform.position = outDirection;
                turretSound.PlayOneShot(Resources.Load<AudioClip>("Music/Gunshot"));
                BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, outDirection, (closestEnemy.transform.position - transform.position).normalized, (closestEnemy.transform.position - transform.position)* 5f, 1, 10);
                cooldown = 0.5f;
            }


        }


    }

    public void HitByProjectile(float damage)
    {
        hp -= damage;
        if(damage <= 0)
        {
            Destroy(gameObject);
            TurretManager.Instance.turrets.Remove(this);
        }
    }
}
