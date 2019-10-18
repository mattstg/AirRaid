using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{

    Vector3 dir;
    GameObject player;
    float timer;
    public float speed;
    [SerializeField] Rigidbody rb;
    // [SerializeField] GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        transform.LookAt(player.transform);

        rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.Acceleration);

    }



}
