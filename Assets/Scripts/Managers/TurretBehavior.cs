using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    //Turret Variables
    private float turretTimer = 0f;
    private float shotTimer;
    readonly float TURRET_RANGE = 50;
    readonly float TURRET_PROJECTILE_SPEED = 35;

    private GameObject turretPrefab;
    private Transform turretParent;
    private Transform bulletSpawn;

    LayerMask ennemyLayer;
    private bool targetFound;
    private GameObject target;
    private float targetDistance;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        targetFound = false;
        ennemyLayer = 1 << 13;
        turretTimer = 240f;
        target = null;
        shotTimer = 0f;
    }
    private void Update()
    {
        //Timer
        turretTimer -= Time.deltaTime;
        shotTimer -= Time.deltaTime;



        //Behavior
        if (target != null)
        {
            if (Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out RaycastHit hit, TURRET_RANGE))
            {
                if (hit.collider.gameObject == target)
                {
                    checkTargetDistance();
                }
                else
                {
                    DetectEnnemy();
                }
            }

        }
        if (transform.position.y < 3)
        {
            DetectEnnemy();
        }
        if (targetFound && shotTimer < 0)
        {
            Shoot(target);
            shotTimer = 0.8f;
        }
    }


    public void DetectEnnemy()
    {
        Collider[] colliderTab = Physics.OverlapSphere(gameObject.transform.position, TURRET_RANGE, ennemyLayer);

        foreach (Collider enemy in colliderTab)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (Physics.Raycast(transform.position, (enemy.gameObject.transform.position - transform.position).normalized, out RaycastHit hit, TURRET_RANGE))
            {
                if (hit.collider.gameObject == enemy.gameObject)
                {
                    if (target == null)
                    {
                        target = enemy.gameObject;
                        targetDistance = distance;
                        targetFound = true;
                    }
                    else if (distance < targetDistance)
                    {
                        target = enemy.gameObject;
                        targetDistance = distance;
                        targetFound = true;
                    }
                }
            }
        }
    }

    public void checkTargetDistance()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        if (targetDistance > TURRET_RANGE)
        {
            targetFound = false;
        }
    }

    public void Shoot(GameObject target)
    {
        if (target == null)
        {
            targetFound = false;
        }
        else
        {
            if (Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out RaycastHit hit, TURRET_RANGE))
            {
                if (hit.collider.gameObject == target)
                {
                    Vector3 aimingVector = target.transform.position - transform.position;
                    //The above gives us a perfect aiming vector, but I want some inaccuracy! 

                    //https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html
                    aimingVector = (Quaternion.Euler(Random.Range(-5, 5f), 0, Random.Range(-5, 5f)) * aimingVector).normalized;  //This is how we rotate a vector 10 degrees randomly around both x and z

                    //Calculate the lifespan of the flak, it should explode near the player
                    //distance divided by speed would give us time to explode  (draw the math with the correct units on paper if unsure, you will see)
                    float lifespan = Vector3.Distance(PlayerManager.Instance.player.transform.position, transform.position) / TURRET_PROJECTILE_SPEED;
                    transform.LookAt(target.transform);
                    BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.Find("TurretHead").Find("BulletSpawn").position, aimingVector, Vector3.zero, lifespan, TURRET_PROJECTILE_SPEED);

                }
            }
        }

    }
}
