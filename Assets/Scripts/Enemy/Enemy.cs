using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    [HideInInspector] public bool isAlive;
    public float hp;  //set in inspector

    public virtual void Initialize()
    {
        isAlive = true;
    }

    public virtual void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public virtual void Refresh()
    {

    }

    public virtual void PhysicRefresh()
    {

    }

    public virtual void Die()
    {
        EnemyManager.Instance.EnemyDied(this);
        isAlive = false;
    }


   
}
