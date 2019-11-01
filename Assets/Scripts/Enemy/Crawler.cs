using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MobileEnemy
{
    readonly float CRAWLER_RANGE_PER_ENERGY = .25f;
    readonly float CRAWLER_ATTACK_SPEED = 2f;
    readonly float CRAWLER_SPIT_VELO = 6f;

    Building targetBuilding;
    Npc targetNpc;
    Transform targetTransform;

    float countdown;
    bool attackMode;

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        targetBuilding = BuildingManager.Instance.GetRandomBuilding();
        targetNpc = NPCManager.Instance.GetRandomNPC();
        if (Random.value > 0.5f)
            targetTransform = targetBuilding.transform;
        else
            targetTransform = targetNpc.transform;

        navmeshAgent.SetDestination(targetTransform.position);
        countdown = 3;
    }

    public override void Refresh()
    {
        base.Refresh();

        //Dont need to check every frame, so just check every 3 seconds


        if (!attackMode)
            UpdateWanderMode();
        else
            UpdateAttackMode();
    }

    private void UpdateWanderMode()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            countdown = 3;
            CheckTargetStillExists();


            //In range to attack bulding
            if (Vector2.Distance(transform.position, targetTransform.position) < CRAWLER_RANGE_PER_ENERGY * energy)
            {
                attackMode = true;
                countdown = CRAWLER_ATTACK_SPEED;
            }

        }
    }

    private void CheckTargetStillExists()
    {
        if (!targetTransform) //building was destroyed, find new building
        {

            if (Random.value > 0.5f)
                targetTransform = BuildingManager.Instance.GetRandomBuilding().transform;
            else
                targetTransform = NPCManager.Instance.GetRandomNPC().transform;

            navmeshAgent.SetDestination(targetTransform.position);

            attackMode = false;
        }
    }

    private void UpdateAttackMode()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            CheckTargetStillExists();
            if (!attackMode)
                return; //Lost track of the building

            BulletManager.Instance.CreateProjectile(ProjectileType.EnemySpit, transform.position + new Vector3(0, transform.localScale.y / 2, 0), (targetTransform.position - transform.position).normalized, Vector3.zero, 60, CRAWLER_SPIT_VELO);

            countdown = CRAWLER_ATTACK_SPEED;

        }
    }
}
