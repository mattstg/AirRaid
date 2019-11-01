using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Healer : Npc
{
    Animator anim;
  

    HashSet<Npc> targetedNpcs;
    float countdown;
    bool helpMode;

    public override void Initialize(float startingEnergy)
    {   

        Debug.Log("HEaler NPc Spawn");
        base.Initialize(startingEnergy);

        anim = GetComponent<Animator>();

      //  targetedNpcs = getTargetNpcs();
        navmeshAgent.SetDestination(NPCManager.Instance.npcs.GetRandomElement<Npc>().transform.position);
        countdown = 3;      
        ModEnergy(startingEnergy);

        
        countdown = 3;
    
    }

    public HashSet<Npc> getTargetNpcs()
    {
        HashSet<Npc> targetNpcArr=null;
        if (NPCManager.Instance.npcs.Count > 0)
        {
            foreach (Npc item in NPCManager.Instance.npcs)
            {
                if ((transform.position - item.transform.position).sqrMagnitude <= 100)
                {
                    targetNpcArr.Add(item);
                }
            }
        }
        else
        {
            targetNpcArr.Add(NPCManager.Instance.npcs.GetRandomElement<Npc>());
        }
        return targetNpcArr;
    }

    public override void Refresh()
    {        
            UpdateHelpMode();
    }
    public override void PhysicRefresh()
    {

    }
    
    private void UpdateHelpMode()
    {
        anim.SetBool("isSupportHeal", true);
        anim.SetBool("isSupportRunning", true);

        foreach (Npc item in NPCManager.Instance.npcs)
        {
            item.ModEnergy(3f);
        }        
    }
}