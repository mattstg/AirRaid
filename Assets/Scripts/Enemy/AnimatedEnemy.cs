using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEnemy : MobileEnemy, IHittable
{
    Animator anim;
    Vector3 targetBuilding;
    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        anim = GetComponent<Animator>();
    }
    public override void ModEnergy(float energyMod)
    {
        hp += energyMod;
        energy = 1;
    }
    public void SetTriggerAttack()
    {
        anim.SetTrigger("Attack");
    }
    public void SetAnimeVelocity(float velocity)
    {
        
        anim.SetFloat("Velocity",velocity);
    }
    public void DetectEnemyNearby()
    {
        int radius = 1;
        float buildingDistance = 0;
        float defenderDistance = 0;
        float distance;
        Vector3 positionBuilding = Vector3.zero;
        
        Collider[] collid = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Wall", "Building"));
        if(collid != null)
        {
            buildingDistance = Vector3.Distance(transform.position, collid[0].transform.position);
            foreach(Collider entity in collid)
            {
                distance = Vector3.Distance(transform.position, entity.transform.position);
                if(buildingDistance > distance)
                {
                    buildingDistance = distance;
                    positionBuilding = entity.transform.position;

                }
            }
            if (positionBuilding != targetBuilding)
            {
                targetBuilding = positionBuilding;
                navmeshAgent.SetDestination(targetBuilding);
            }
        }
    }
}
