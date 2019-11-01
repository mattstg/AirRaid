using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 0, 1) * speed*Time.deltaTime;
        //transform.Rotate(new Vector3(0,0,1*speed),Space.Self);
    }
}
