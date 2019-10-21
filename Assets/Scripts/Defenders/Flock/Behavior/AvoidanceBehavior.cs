using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return nothing
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //Counter for normalizing the Vector
        int numAvoid = 0;

        //Could be the original context list or might be filtered context list. 
        //Is there any ScriptableFilter attached?
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        Vector3 avoidanceMove = Vector3.zero;
        foreach (Transform item in filteredContext)
        {
         
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                numAvoid++;
                //Offset is calculated
                avoidanceMove += (agent.transform.position - item.position);
            }

        }
       
        if (numAvoid > 0)
        {
            avoidanceMove /= numAvoid;
        }
        return avoidanceMove;
    }
}
