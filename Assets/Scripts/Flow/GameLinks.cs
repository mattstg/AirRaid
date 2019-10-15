using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLinks : MonoBehaviour
{
    public static GameLinks gl;

    public Transform worldFloor;
    public Transform spawnLocationParent;
    public Transform playerSpawn;
    public Camera postDeathCam;

    [Header("Building parent")]
    public Transform tentParent;
    public Transform buildingParent;
    public Transform wallParent;
    public Transform towerParent;
    public Transform castle;
}
