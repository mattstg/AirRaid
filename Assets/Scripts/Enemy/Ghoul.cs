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
