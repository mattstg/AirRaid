using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Rigidbody agentRB;
    public Rigidbody AgentRB { get { return agentRB; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; }  }

    public bool useRigidBody = false;
    public bool useTransformForwardRotation = false;

    public void Initialize()
    {
        agentCollider = GetComponent<Collider>();
        if (useRigidBody)
        {
            agentRB = GetComponent<Rigidbody>();
        }
    }

    public void PostInitialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        if(velocity != Vector3.zero)
        {
            if (useTransformForwardRotation)
            {
                transform.forward = velocity;
            }
            else
            {
                velocity.y = 0;
                transform.rotation = Quaternion.LookRotation(velocity);
            }
//             //agentRB.velocity = velocity;
//             agentRB.AddForce(velocity * Time.deltaTime, ForceMode.Impulse);
        }
        if (useRigidBody)
        {
            agentRB.velocity += velocity * Time.deltaTime;
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
        }
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 1.0f));
    }
}
