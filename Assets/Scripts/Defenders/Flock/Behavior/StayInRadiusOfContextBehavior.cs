﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius Of Leader")]

public class StayInRadiusOfContextBehavior : FilteredFlockBehavior
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        if (filter == null)
        {
            Debug.LogError("This Behavior needs a Filter" + this.name);
        }

        if(filteredContext.Count == 0)
        {
            return Vector3.zero;
        }
        center = filteredContext[filteredContext.Count -1].transform.position;

        Vector3 centerOffSet = center - agent.transform.position;
        //Less optimal sqrMagnitude... maybe change this at some point
        float t = centerOffSet.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffSet * t * t;
    }
}
