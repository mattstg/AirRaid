using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    readonly float ENEMY_SIZE_MULT = .75f;
    [HideInInspector] public bool isAlive;
    protected float hp;
    protected float energy;
    float updateSizeTimeCountdown;

    public virtual void Initialize(float startingEnergy)
    {
        isAlive = true;
        ModEnergy(startingEnergy);
        Resize();
    }

    public virtual void HitByProjectile(float damage)
    {
        if (!isAlive)
            return;

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
        //This is bad, since growing changes my size, it messes with the collider size, which makes us move into the floor and re-calculates colliders
        //so we only do it every couple of seconds
        updateSizeTimeCountdown -= Time.deltaTime;
        if (updateSizeTimeCountdown <= 0)
        {
            updateSizeTimeCountdown = 5;
            //size is a volume, pretending its a cube check based on energy
            Resize();
        }
    }

    protected void Resize()
    {
        float size = Mathf.Pow(energy, 1 / 3f);
        transform.localScale = Vector3.one * ENEMY_SIZE_MULT * size;
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
