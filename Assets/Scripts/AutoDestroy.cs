using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float Delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
