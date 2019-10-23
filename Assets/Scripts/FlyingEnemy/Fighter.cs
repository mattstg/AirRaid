using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{
    Vector3 targetDir;
    GameObject player;
    public float speed;
    [SerializeField] Rigidbody rb;
    float angleToPlayer;
    [SerializeField] GameObject bullets;
    public override void Initialize(float startingEnergy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        base.Initialize(startingEnergy);
    }
    public override void PhysicRefresh()
    {
        if (player.transform != null)
        {
            transform.LookAt(player.transform);
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
        }

        RaycastHit hit = new RaycastHit();
        targetDir = player.transform.position - transform.position;
        angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
        if (angleToPlayer >= -60 && angleToPlayer <= 60 && Physics.Raycast(transform.position, transform.TransformDirection(targetDir), out hit, Mathf.Infinity) && hit.transform.CompareTag("Player"))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(targetDir) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
        }
        base.PhysicRefresh();
    }



}
