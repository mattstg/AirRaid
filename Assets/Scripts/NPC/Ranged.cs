using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ranged : Npc
{ 
    readonly float Ranged_RANGE_PER_ENERGY = .25f;
    readonly float Ranged_ATTACK_SPEED = 2f;
    readonly float Ranged_SPIT_VELO = 40f;
    Enemy targetEnemy;
    float countdown;
    bool attackMode;

    Animator anim;
    public override void Initialize(float startingEnergy)
    {

        base.Initialize(startingEnergy);
        anim = GetComponent<Animator>(); 
        targetEnemy = getTargetEnemy();
        if(targetEnemy)
            navmeshAgent.SetDestination(targetEnemy.transform.position);
        countdown = 3;
    }
    
    

    public override void Refresh()
    {
        if (!attackMode)
            UpdateWanderMode();
        else
            UpdateAttackMode();
    }
    public override void PhysicRefresh()
    {

    }
    private void UpdateWanderMode()
    {
        anim.SetBool("isRangedRunning", true);
        anim.SetBool("isRangedAttacking", false);
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            countdown = 3;
            CheckEnemyStillExists();

            //In range to attack bulding
            if (Vector2.SqrMagnitude(transform.position- targetEnemy.transform.position) < Ranged_RANGE_PER_ENERGY * energy)
            {
                attackMode = true;
                countdown = Ranged_ATTACK_SPEED;
            }
        }
    }
    private void CheckEnemyStillExists()
    {
        if (!targetEnemy) //enemy was destroyed, find new one
        {
            targetEnemy = getTargetEnemy();
            navmeshAgent.SetDestination(targetEnemy.transform.position);
            attackMode = false;
        }
    }
    private void UpdateAttackMode()
    {
        anim.SetBool("isRangedRunning", false);
        anim.SetBool("isRangedAttacking", true);
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            CheckEnemyStillExists();
            if (!attackMode)
                return; //Lost track of the enemy

            BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.position + new Vector3(0, transform.localScale.y / 2, 0), (targetEnemy.transform.position - transform.position).normalized, Vector3.zero, 60, Ranged_SPIT_VELO);

            countdown = Ranged_ATTACK_SPEED;
        }
    }


}
