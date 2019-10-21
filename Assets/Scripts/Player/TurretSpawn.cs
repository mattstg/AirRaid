using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    //Turret Variables
    public float turretTimer = 0f;
    private GameObject turretPrefab;
    private Transform turretParent;
    Transform turretSpawnPoint;
    LayerMask ennemyLayer;
    readonly float TURRET_RANGE = 45;
    readonly float TURRET_PROJECTILE_SPEED = 40;
    public float radiusRangeDetection = 90;

    // Start is called before the first frame update
    void Start()
    {

        ennemyLayer = 1 << 13;

    }
    private void Update()
    {
        detecteEnnemy();
        
        Debug.Log("openis");
    }
    public void SpawnTurret()
    {
        Instantiate(turretPrefab, transform.position, transform.rotation, turretParent);

    }

    public void detecteEnnemy()
    {
        Collider[] ennemyColliderTab = Physics.OverlapSphere(gameObject.transform.position, TURRET_RANGE, ennemyLayer);

        if (ennemyColliderTab.Length > 0)
        {
            Debug.Log(ennemyColliderTab[0].name);
            checkObstacles(ennemyColliderTab[0].gameObject);
            ShootAtEnemy(ennemyColliderTab[0].gameObject);
        }

    }

    public void ShootAtEnemy(GameObject ennemy)
    {
        Vector3 aimingVector = ennemy.transform.position - transform.position;
        //The above gives us a perfect aiming vector, but I want some inaccuracy! 

        //https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html
        aimingVector = (Quaternion.Euler(Random.Range(-10, 10f), 0, Random.Range(-10, 10f)) * aimingVector).normalized;  //This is how we rotate a vector 10 degrees randomly around both x and z

        //Calculate the lifespan of the flak, it should explode near the player
        //distance divided by speed would give us time to explode  (draw the math with the correct units on paper if unsure, you will see)
        float lifespan = Vector3.Distance(PlayerManager.Instance.player.transform.position, transform.position) / TURRET_PROJECTILE_SPEED;

        Projectile p = BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.position, aimingVector, Vector3.zero, lifespan, TURRET_PROJECTILE_SPEED);
    }

    public bool checkObstacles(GameObject ennemy)
    {
        bool isDetected = false;
        ennemyLayer = ~ennemyLayer;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ennemyLayer))
        {
            Debug.DrawLine(this.transform.position, ennemy.transform.position);
            Debug.Log("Did Hit");
            isDetected = true;
        }
        else
        {
            Debug.DrawLine(this.transform.position, ennemy.transform.position);
            Debug.Log("Did not Hit");
        }
        return isDetected;
    }
}
