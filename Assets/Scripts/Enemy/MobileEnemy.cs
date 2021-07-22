using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobileEnemy : Enemy
{
    protected NavMeshAgent navmeshAgent;
    

    public override void Initialize(float startingEnergy, Vector3 pos)
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        base.Initialize(startingEnergy, pos);
    }

    public override void SetPosition(Vector3 newPos)
    {
        //Instead of setting position normally, this object uses a navmesh, so we must use the Navmeshagent to reposition it!

        bool b = navmeshAgent.Warp(newPos);
        if(!b)
        {
            NavMeshHit nmh;
            if(NavMesh.SamplePosition(newPos, out nmh, 20, NavMesh.AllAreas))
            {
                navmeshAgent.Warp(nmh.position);
            }
            else
            {
                hp = 0; //we just kill it if we cant find a spawn spot, somethings fucky
            }
        }
    }

}
