using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : Ship
{

    Transform player;
    Rigidbody rb;
    float speedOfFighter = 85f;

    GameObject randomBuilding;

    float countdown;
    bool attackMode;
    readonly float MIN_ANGLE_FOR_ATTACK = 15;
    readonly float FIGHTER_ATTACK_SPEED = 2f;
    readonly float FIGHTER_ATTACK_VELO = 6f;
    readonly float TURNING_RATE = 5;  //time to preform a full 360

    public override void Initialize(float startingEnergy)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = gameObject.GetComponent<Rigidbody>();
        base.Initialize(startingEnergy);
        randomBuilding = BuildingManager.Instance.GetRandomBuilding().gameObject;
    }

    public override void Refresh()
    {
        base.Refresh();

        if(PlayerManager.Instance.player.isAlive)
        {
            if (!attackMode)
                UpdateFindPlayer();
            else
                UpdateAttackMode();
        }
    }

    public override void PhysicRefresh()
    {
        base.PhysicRefresh();
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPos = (PlayerManager.Instance.player.isAlive ? player.position : new Vector3(400, 20, 20)); //random location in space
        transform.forward = Vector3.RotateTowards(transform.forward, (targetPos - transform.position).normalized, 2 * Mathf.PI / TURNING_RATE * Time.fixedDeltaTime, 0);
        rb.velocity = transform.forward * speedOfFighter * Time.deltaTime;
        if((transform.position - targetPos).sqrMagnitude <= 80 && PlayerManager.Instance.player.isAlive == false)
        {
            DestroyPlane();
        }
    }

    private void UpdateFindPlayer()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            countdown = 3;
            //Check if player is in sights
            if (Mathf.Abs(Vector3.SignedAngle(transform.position, player.position, transform.right)) < MIN_ANGLE_FOR_ATTACK)
            {
                attackMode = true;
                countdown = FIGHTER_ATTACK_SPEED;
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

            BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.position + new Vector3(0, 0, transform.localScale.z/2), (player.position - transform.position).normalized, rb.velocity, 60, FIGHTER_ATTACK_VELO);
            countdown = FIGHTER_ATTACK_SPEED;
        }
    }

    void DestroyPlane()
    {
        EnemyManager.Instance.EnemyDied(this);
        //Destroy(this);
    }

}    

