using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return Forward Vector
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }
        
        Vector3 alignmentMove = Vector3.zero;
        //Could be the original context list or might be filtered context list. 
        //Is there any ScriptableFilter attached?
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.forward;
        }
        //Stay normalized here
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
