using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : AnimatedEnemy
{
    //readonly float WALKING_SPEED = 6f;
    //readonly float MIN_ENERGY_EVOLVE = 10f; //If dies, but has over 10 energy, should spawn a new Troll
    //readonly float MAX_ENERGY_BEFORE_EVOLVE = 15f; //Should die and spawn a new Troll
    readonly float MAX_HP = 100f;

    public EnemyAbilityManager enemyAbilityManager;

    Building targetBuilding;
    
    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        hp = MAX_HP;
        enemyAbilityManager = new EnemyAbilityManager(this);
        targetBuilding = BuildingManager.Instance.GetRandomBuilding();
        navmeshAgent.SetDestination(targetBuilding.transform.position);
    }
    public override void Refresh()
    {
        base.Refresh();
        SetAnimeVelocity(navmeshAgent.velocity.magnitude / navmeshAgent.speed);
        Collider[] bob = Physics.OverlapSphere(transform.position, 1, LayerMask.GetMask("Wall", "Building"));
        foreach(Collider bob1 in bob)
        {
            Debug.Log(bob1.transform.name);
        }
        
    }
    public void FixedRefresh()
    {

    }
}
