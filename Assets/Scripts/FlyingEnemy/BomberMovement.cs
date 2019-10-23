using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMovement : MonoBehaviour
{
    public Transform target;
    [SerializeField] Rigidbody rg;
    float speedRotation = 0.1f;
    float speedMove = 2f;
    float timer = 0.5f;
    float stepRotation;
    float stepMove;
    
    Vector3 dir;
    Vector3 newDir;
    Vector3 targetDir;
    void Start()
    {
        rg.GetComponent<Rigidbody>();
        // SetDir();
    }


    void FixedUpdate()
    {
        targetDir = target.position - transform.position;

        // The step size is equal to speed times frame time.
        stepRotation = speedRotation * Time.fixedDeltaTime;
        stepMove = speedMove * Time.fixedDeltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, stepMove);
        newDir = Vector3.RotateTowards(transform.forward, targetDir, stepRotation, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
       if(transform.position == target.position)
        {
            target.transform.position = new Vector3(Random.Range(-500, 600), Random.Range(100,200), Random.Range(-800,1000));
        }
    }
}
