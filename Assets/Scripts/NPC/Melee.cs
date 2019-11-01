using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Melee :Npc
{
readonly float MELEE_RANGE_PER_ENERGY = .5f;
readonly float MELEE_ATTACK_SPEED = 8f;
readonly float MELEE_DAMAGE = 4f;
Enemy targetEnemy;
float countdown;
bool attackMode=false;
    Animator anim;
public override void Initialize(float startingEnergy)
{

    base.Initialize(startingEnergy);
    anim = GetComponent<Animator>();
    targetEnemy = getTargetEnemy();
    navmeshAgent.SetDestination(targetEnemy.transform.position);
    countdown = 3;

}


public override void Refresh()
{
            targetEnemy = getTargetEnemy();
           
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
        anim.SetBool("isMeleeRunning", true);
        anim.SetBool("isMeleeAttacking", false);

        Debug.Log("Melee wander");

        countdown -= Time.deltaTime;
    if (countdown < 0)
    {
        countdown = 3;
        CheckEnemyStillExists();

        //In range to attack enemy
        if (Vector2.SqrMagnitude(transform.position- targetEnemy.transform.position )< MELEE_RANGE_PER_ENERGY * energy)
        {
            attackMode = true;
            countdown = MELEE_ATTACK_SPEED;
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
        anim.SetBool("isMeleeRunning", true);
        anim.SetBool("isMeleeAttacking", true);

        Debug.Log("Melee attack");

        countdown -= Time.deltaTime;
    if (countdown <= 0)
    {
        CheckEnemyStillExists();
        if (!attackMode)
            return; //Lost track of the building
        //BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.position + new Vector3(0, transform.localScale.y / 2, 0), (targetEnemy.transform.position - transform.position).normalized, Vector3.zero, 60, MELEE_SPIT_VELO);
        targetEnemy.HitByProjectile(MELEE_DAMAGE);
        countdown = MELEE_ATTACK_SPEED;
    }
}
}