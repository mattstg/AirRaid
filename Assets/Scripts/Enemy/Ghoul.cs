using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : AnimatedEnemy
{
    Building targetBuilding;
    
    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        targetBuilding = BuildingManager.Instance.GetRandomBuilding();
        navmeshAgent.SetDestination(targetBuilding.transform.position);
    }
    public override void Refresh()
    {
        base.Refresh();
        SetAnimeVelocity(navmeshAgent.velocity.magnitude / navmeshAgent.speed);
        DetectEnemyNearby();
        
    }
    public void FixedRefresh()
    {

    }
}
