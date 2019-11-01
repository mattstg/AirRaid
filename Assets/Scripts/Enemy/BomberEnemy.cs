using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Ship
{
    Transform player;
    Rigidbody rb;
    float speedOfBomber = 60f;
    float countdown;
    bool attackMode;
    readonly float BOMB_LIFESPAN = 6;
    readonly float BOMBER_ATTACK_SPEED = 10;
    readonly float TURNING_RATE = 5;  //time to preform a full 360
    [HideInInspector]public float distanceFromPlayer = 30;
    [HideInInspector]public float distanceFromBuilding = 40;

    public override void Initialize(float startingEnergy)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody>();
        base.Initialize(startingEnergy);
    }

    public override void PhysicRefresh()
    {
        base.PhysicRefresh();
        EvadePlayerAndTargetBuildings();
    }

    public override void Refresh()
    {
        base.Refresh();
        
    }

    void EvadePlayerAndTargetBuildings()
    {
        //evade player
        if (PlayerManager.Instance.player.isAlive && (transform.position - player.position).sqrMagnitude <= distanceFromPlayer* distanceFromBuilding)
        {
            attackMode = false;
            Vector3 dir = (transform.position - player.position).normalized;
            dir.y = 0;

            transform.forward = Vector3.RotateTowards(transform.forward, dir, 2 * Mathf.PI / TURNING_RATE * Time.fixedDeltaTime, 0);
            rb.velocity = transform.forward * speedOfBomber * Time.deltaTime;
        }
        else
        {
            Transform closestBuilding = ClosestBuilding();
            Vector3 dirVector = (closestBuilding.position - transform.position);
            dirVector.y = 0;

            //target buildings to be implemented
            if (dirVector.sqrMagnitude >= distanceFromBuilding * distanceFromBuilding)
            {
                transform.forward = Vector3.RotateTowards(transform.forward, dirVector.normalized, 2 * Mathf.PI / TURNING_RATE * Time.fixedDeltaTime, 0);
                rb.velocity = transform.forward * speedOfBomber * Time.deltaTime;
            }
                
            else
            {
                attackMode = true;
                UpdateAttackMode();
            }
        }
    }

    private void UpdateAttackMode()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            if (!attackMode)
                return; //Lost track of the player

            Projectile p = BulletManager.Instance.CreateProjectile(ProjectileType.Bomb, 
                                                    transform.position + new Vector3(0, -5f, 0), 
                                                    -transform.up,
                                                    rb.velocity,
                                                    BOMB_LIFESPAN, 
                                                    0);

            Collider c = p.gameObject.GetComponentInChildren<Collider>();
            Physics.IgnoreCollision(c, transform.GetComponent<Collider>());  //make bomb not collide with bomber
            countdown = BOMBER_ATTACK_SPEED;
        }
    }

    Transform ClosestBuilding()
    {
        Transform closestBuilding = null;

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Building building in BuildingManager.Instance.allBuildings)
        {
            Vector3 directionToTarget = building.gameObject.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestBuilding = building.gameObject.transform;
            }
        }
        return closestBuilding;
    }
}
