﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : AnimatedEnemy
{
    readonly float WALKING_SPEED = 6f;
    readonly float MIN_ENERGY_EVOLVE = 10f; //If dies, but has over 10 energy, should spawn a new Troll
    readonly float MAX_ENERGY_BEFORE_EVOLVE = 15f; //Should die and spawn a new Troll
    readonly float MAX_HP = 100f;
    public Rigidbody rb;
    public EnemyAbilityManager enemyAbilityManager;
    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        hp = MAX_HP;
        rb = GetComponent<Rigidbody>();
        enemyAbilityManager = new EnemyAbilityManager(this);
        enemyAbilityManager.AddAbility(new Ab_HeavyAttack(this));


    }
    public override void Refresh()
    {
        base.Refresh();
    }

    public override void PhysicRefresh()
    {
        base.PhysicRefresh();
    }

}
