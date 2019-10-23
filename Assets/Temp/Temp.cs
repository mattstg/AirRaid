using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        EnemyManager.Instance.toRemove.Push(collision.gameObject.GetComponent<Enemy>());
    }
}
