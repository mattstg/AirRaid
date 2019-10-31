using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Melee :MonoBehaviour, IHittable
{
    protected NavMeshAgent navmeshAgent;

[HideInInspector] public bool isAlive;
protected float hp;
protected float energy;

readonly float MELEE_RANGE_PER_ENERGY = .25f;
readonly float MELEE_ATTACK_SPEED = 8f;
readonly float MELEE_DAMAGE = 4f;
Enemy targetEnemy;
float countdown;
bool attackMode;
public virtual void Initialize(float startingEnergy)
{
    Debug.Log("NPCManager Spawn");

    isAlive = true;
    ModEnergy(startingEnergy);
    navmeshAgent = GetComponent<NavMeshAgent>();

    if (EnemyManager.Instance.enemies.Count > 0)
    {
        if (EnemyManager.Instance.CrawlerEnemies.Count > 0)
        {
            targetEnemy = EnemyManager.Instance.CrawlerEnemies.GetRandomElement<Enemy>();
        }
        else
        {
            targetEnemy = EnemyManager.Instance.enemies.GetRandomElement<Enemy>();
        }
        if(targetEnemy)
            navmeshAgent.SetDestination(targetEnemy.transform.position);
    }
    //   ToArray()[Random.Range(0, EnemyManager.Instance.toAdd.Count)];
    //
    countdown = 3;
}
public void HitByProjectile(float damage)
{
    hp -= damage;
    if (hp <= 0)
        Die();
}
public virtual void ModEnergy(float energyMod)
{
    hp += energyMod;
    energy += energyMod;
}

public virtual void Refresh()
{



    if (!attackMode)
        UpdateWanderMode();
    else
        UpdateAttackMode();
}
public virtual void PhysicRefresh()
{

}
private void UpdateWanderMode()
{
    countdown -= Time.deltaTime;
    if (countdown < 0)
    {
        countdown = 3;
        CheckEnemyStillExists();

        //In range to attack bulding
        if (Vector2.Distance(transform.position, targetEnemy.transform.position) < MELEE_RANGE_PER_ENERGY * energy)
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
        bool enemyAssigned = false;
        foreach (Enemy item in EnemyManager.Instance.enemies)
        {
            if ((transform.position - item.transform.position).sqrMagnitude <= 16)
            {
                targetEnemy = item;
                enemyAssigned = true;
                break;
            }
        }
        if (!targetEnemy)
        {
            targetEnemy = EnemyManager.Instance.enemies.GetRandomElement<Enemy>();

        }

        navmeshAgent.SetDestination(targetEnemy.transform.position);
        attackMode = false;

    }
}
private void UpdateAttackMode()
{
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

public virtual void Die()
{
    NPCManager.Instance.NpcDiedMelee(this);
    isAlive = false;
}
}