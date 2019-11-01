using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Npc : MonoBehaviour, IHittable
{
    protected NavMeshAgent navmeshAgent;

    [HideInInspector] public bool isAlive;
    protected float hp;
    protected float energy;    
   

    public virtual void Initialize(float startingEnergy)
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        isAlive = true;
        ModEnergy(startingEnergy);
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
    public float getHp()
    {
        return hp;
    }
    public Enemy getTargetEnemy()
    {
        HashSet<Enemy> enemiesTarget = EnemyManager.Instance.enemies;
        foreach (Enemy item in enemiesTarget)
        {
            if ((transform.position - item.transform.position).sqrMagnitude <= 25)
            {
                return item;
            }
        }
        return EnemyManager.Instance.enemies.GetRandomElement<Enemy>();
    }
    

    public virtual void Refresh()
    {
    }
    public virtual void PhysicRefresh()
    {
    }
    public virtual void Die()
    {
        NPCManager.Instance.NpcDied(this);
        isAlive = false;
    }
}
