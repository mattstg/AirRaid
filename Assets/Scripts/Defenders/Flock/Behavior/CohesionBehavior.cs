using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    /*
     READ ME
     If you don't want the agent to flicker. (weird rotation behavior)
     Do not use this Behavior.
    */

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Could be the original context list or might be filtered context list. 
        //Is there any ScriptableFilter attached?
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //If no neighbor found return nothing
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

        return cohesionMove;
    }
}
