using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(1, 100)]
    public int startingCount = 100;

    //How large Agent is. For random spawning.
    const float AGENT_DENSITY = 1f;

    //Multiply with the move Vector3 before agent Move(move) is called
    [Range(1.0f, 100.0f)]
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

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    public void Start() {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        //         for (int i = 0; i < startingCount; i++)
        //         {
        //             FlockAgent newAgent = Instantiate(agentPrefab,
        //                 new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * startingCount * AGENT_DENSITY,
        //                 Quaternion.Euler(Vector3.up * Random.Range(0f,360f)),
        //                 transform);
        //             newAgent.name = "agent" + i;
        //             newAgent.Initialize(this);
        //             agents.Add(newAgent);
        //         }
    }

    public void Update() {
        foreach (FlockAgent agent in agents) {
            List<Transform> context = GetNearbyObjects(agent);

            //Testing only :
            //agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            //End Testing

            //All behaviors have different CalculateMove()
            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed) {
                move = move.normalized * maxSpeed;

            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent) {
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach (Collider c in contextColliders) {
            if (c != agent.AgentCollider) {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
