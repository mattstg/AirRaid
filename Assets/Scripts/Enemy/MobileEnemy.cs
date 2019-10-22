using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MobileEnemy : Enemy
{
    [HideInInspector] public NavMeshAgent navmeshAgent;
    

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    
}
