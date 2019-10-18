using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderManager {
    #region Singleton
    private static DefenderManager instance;
    private DefenderManager() { }
    public static DefenderManager Instance { get { return instance ?? (instance = new DefenderManager()); } }
    #endregion

    public Dictionary<TypeDefender, GameObject> defenderPrefabs = new Dictionary<TypeDefender, GameObject>();

    private List<Spawner> spawnerList = new List<Spawner>();
    private List<Defender> defendersList = new List<Defender>();

    public void Initialize() {
        InitPrefabs();
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Defender_Spawner");
        foreach (GameObject spawner in spawners) {
            this.spawnerList.Add(spawner.GetComponent<Spawner>());
        }
    }

    private void InitPrefabs() {
        this.defenderPrefabs.Add(TypeDefender.MELEE, Resources.Load<GameObject>("Prefabs/Defenders/Guard"));
        this.defenderPrefabs.Add(TypeDefender.RANGE, Resources.Load<GameObject>("Prefabs/Defenders/Archer"));
        this.defenderPrefabs.Add(TypeDefender.SUPPORT, Resources.Load<GameObject>("Prefabs/Defenders/Support"));
    }

    public void Refresh() {
        for (int i = this.defendersList.Count - 1; i >= 0; i--) {
            this.defendersList[i].Refresh();
        }
        for(int i  = this.spawnerList.Count - 1; i >= 0; i--) {
            this.spawnerList[i].Refresh();
        }
    }
    public void PhysicRefresh() {
        for (int i = this.defendersList.Count - 1; i >= 0; i--) {
            this.defendersList[i].PhysicRefresh();
        }
    }

    public void AddDefenderToList(Defender defender) {
        this.defendersList.Add(defender);
    }
    public void RemoveDefenderToList(Defender defender) {
        this.defendersList.Remove(defender);
    }
}
