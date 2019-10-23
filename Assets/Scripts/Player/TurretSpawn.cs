using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    //Turret Variables
    public float turretTimer = 0f;
    public GameObject turretPrefab;
    private Transform turretParent;
    LayerMask ennemyLayer;
   
    // Start is called before the first frame update
    void Start()
    {

        turretParent = (new GameObject("TurretParent")).transform;

    }

    public void SpawnTurret()
    {
        Instantiate(turretPrefab, new Vector3(transform.position.x,(transform.position.y-5),transform.position.z), Quaternion.identity, turretParent);

    }

}
