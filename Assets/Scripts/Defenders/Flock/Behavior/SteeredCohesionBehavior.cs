using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{

    Vector3 currentVelocity;
    public float agentSmoothingTime = 0.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    { 
        //Could be the original context list or might be filtered context list. 
        //Is there any ScriptableFilter attached?
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //if no neighbors, return nothing
        if (filteredContext.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= filteredContext.Count;
        
        //Create offset from agent position
        cohesionMove -= agent.transform.position;

        //Smooth Agent Rotation
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothingTime);
       
        return cohesionMove;
    }
}
