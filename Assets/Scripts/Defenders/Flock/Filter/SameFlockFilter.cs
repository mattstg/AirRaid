using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class SameFlockFilter : ContextFilter
{
    //Do not use this Filter 
    //GetComponent in update.

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach(Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if(itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
