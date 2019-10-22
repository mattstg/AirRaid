using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private Transform parent;
    private Vector3 spawnPoint;
    private float lastTimeUnitSpawned;
    private short amountToSpawnToFinishGroup;
    private Leader groupdLeader;

    public void Initialize() {
        this.parent = GameObject.Find("Defenders").transform;
        //Find SpawnPoint depending on building size
        Vector3 size = transform.GetComponent<BoxCollider>().bounds.extents;
        this.spawnPoint = new Vector3(transform.position.x + size.x + 1, 0, transform.position.z + size.z + 1);

        CreateNewGroup();
    }

    public void Refresh() {
        if (Time.time - this.lastTimeUnitSpawned >= GlobalDefendersVariables.Instance.DELAY_SPAWN_DEFENDER) {
            if (this.amountToSpawnToFinishGroup > 0) {
                SpawnDefender();
            }
            else if (this.groupdLeader == null) {
                CreateNewGroup();
            }
            else {
                this.groupdLeader.GoFight();
                this.groupdLeader = null;
            }
            this.lastTimeUnitSpawned = Time.time;
        }
    }

    private void SpawnDefender() {
        this.amountToSpawnToFinishGroup--;
        Defender defender = GameObject.Instantiate<GameObject>(DefenderManager.Instance.defenderPrefabs[GetTypeToSpawn()], this.spawnPoint, new Quaternion(), this.parent).GetComponent<Defender>();
        defender.Initialize(this.groupdLeader);
        this.groupdLeader.AddDefenderToMyGroup(defender);
    }

    private TypeDefender GetTypeToSpawn() {
        //Get type to spawn depending on how many of each on the map / or randomly...
        TypeDefender type;
        switch (Random.Range(0, 3)) {
            case 0:
                type = TypeDefender.MELEE;
                break;
            case 1:
                type = TypeDefender.RANGE;
                break;
            case 2:
                type = TypeDefender.SUPPORT;
                break;
            default:
                Debug.LogError("Not enough TypeDefender...");
                type = TypeDefender.MELEE;
                break;
        }
        return type;
    }

    private void GetSpawnSpot() {
        //Spawn randomly or alway at 1 spot decided at the beginnings
    }

    private bool CheckIfSpotIsTaken() {
        return false;
    }

    private void CreateNewGroup() {
        this.groupdLeader = GameObject.Instantiate<GameObject>(DefenderManager.Instance.defenderPrefabs[TypeDefender.LEADER], this.spawnPoint, new Quaternion(), this.parent).GetComponent<Leader>();
        this.groupdLeader.Initialize();
        DefenderManager.Instance.AddLeaderToList(this.groupdLeader);
        this.amountToSpawnToFinishGroup = GlobalDefendersVariables.Instance.GROUP_SIZE;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.spawnPoint, 1);
    }
}
