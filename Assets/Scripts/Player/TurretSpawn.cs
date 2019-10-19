using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{
    //Turret Variables
    public float turretTimer = 0f;
    private GameObject turretPrefab;
    private Transform turretParent;
    Transform turretSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        turretPrefab = Resources.Load<GameObject>("Prefabs/Turret");
        turretParent = (new GameObject("TurretParent")).transform;

    }

    public void SpawnTurret()
    {
        Instantiate(turretPrefab, transform.position, transform.rotation, turretParent);

    }
}
