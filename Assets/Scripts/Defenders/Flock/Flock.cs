using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    //public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

//     [Range(1, 100)]
//     public int startingCount = 100;

    //How large Agent is. For random spawning.
    /*const float AGENT_DENSITY = 1f;*/

    //Multiply with the move Vector3 before agent Move(move) is called
    [Range(1.0f,  100.0f)]
    public float driveFactor = 10f;

    [Range(1.0f, 100.0f)]
    public float maxSpeed = 5f;

    //If you have many agent to go over 10.0f. For Leader following we might need higher value.
    //How far a single agent can detect other agents or entities.
    [Range(1f, 50f)]
    public float neighborRadius = 1.5f;

    //This should never be bigger than neighborRadius
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    public float squareMaxSpeed;
    public float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    public void Initialize()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;


    }

    public void GetCalculatedMove()
    {

    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
