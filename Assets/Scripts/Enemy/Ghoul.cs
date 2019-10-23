﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : AnimatedEnemy
{
    public bool die;
    Rigidbody rb;
    GameObject ghouleEvolution;
    //readonly float WALKING_SPEED = 6f;
    readonly float MIN_ENERGY_EVOLVE = 10f; //If dies, but has over 10 energy, should spawn a new Troll
    //readonly float MAX_ENERGY_BEFORE_EVOLVE = 15f; //Should die and spawn a new Troll
    readonly float MAX_HP = 100f;

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(1);
        hp = MAX_HP;
        rb = GetComponent<Rigidbody>();
        ghouleEvolution = Resources.Load<GameObject>("Prefabs/Enemy/Troll");
    }
    public override void Refresh()
    {
        base.Refresh();
        if (die)
        {
            die = false;
            Die();
            //navmeshAgent.enabled = false;
            Destroy(rb);
        }


    }
    public void FixedRefresh()
    {

    }
    public override void Die()
    {
        DieProcess(EnemyType.Troll);
    }
    public virtual void DieProcess(EnemyType type)
    {
        StartCoroutine(SpawnEvolution(type, death.length));
        anim.SetTrigger("isDead");
    }
    public virtual IEnumerator SpawnEvolution(EnemyType type, float time)
    {
        yield return new WaitForSeconds(time);
        if(energy > MIN_ENERGY_EVOLVE)
            EnemyManager.Instance.SpawnEnemy(type, transform.position, 100);
        base.Die();

        //troll.transform.position = transform.position;
    }
}