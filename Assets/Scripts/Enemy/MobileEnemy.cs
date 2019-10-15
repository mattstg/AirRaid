using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobileEnemy : Enemy
{
    NavMeshAgent navmeshAgent;
    

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    
}
