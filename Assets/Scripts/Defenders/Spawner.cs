using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private Dictionary<TypeDefender, GameObject> defenderPrefabs = new Dictionary<TypeDefender, GameObject>();
    private float lastTimeUnitSpawned;

    public void Initialise() {
        this.defenderPrefabs.Add(TypeDefender.MELEE, Resources.Load<GameObject>("Prefabs/Defenders/Melee"));
        this.defenderPrefabs.Add(TypeDefender.RANGE, Resources.Load<GameObject>("Prefabs/Defenders/Range"));
        this.defenderPrefabs.Add(TypeDefender.SUPPORT, Resources.Load<GameObject>("Prefabs/Defenders/Support"));
    }

    public void Refresh() {
        if (Time.time - this.lastTimeUnitSpawned >= GlobalDefendersVariables.Instance.DELAY_SPAWN_DEFENDER) {
            SpawnDefenser();
            this.lastTimeUnitSpawned = Time.time;
        }
    }

    private void SpawnDefenser() {

    }

    private TypeDefender GetTypeToSpawn() {
        return TypeDefender.MELEE;
    }

    private void GetSpawnSpot() {

    }

    private bool CheckIfSpotIsTaken() {
        return false;
    }
}
