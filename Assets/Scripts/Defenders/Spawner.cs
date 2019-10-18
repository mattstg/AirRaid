using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private float lastTimeUnitSpawned;

    public void Initialize() {
    }

    public void Refresh() {
        if (Time.time - this.lastTimeUnitSpawned >= GlobalDefendersVariables.Instance.DELAY_SPAWN_DEFENDER) {
            SpawnDefenser();
            this.lastTimeUnitSpawned = Time.time;
        }
    }

    private void SpawnDefenser() {
        Defender defender = GameObject.Instantiate<GameObject>(DefenderManager.Instance.defenderPrefabs[GetTypeToSpawn()], new Vector3(0, 5, 0), new Quaternion()).GetComponent<Defender>();
        defender.Initialize();
        DefenderManager.Instance.AddDefenderToList(defender);
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
}
