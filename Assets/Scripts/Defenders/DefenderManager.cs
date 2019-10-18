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
    private List<Leader> leadersList = new List<Leader>();

    public void Initialize() {
        InitPrefabs();
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Defender_Spawner");
        foreach (GameObject spawner in spawners) {
            Spawner obj = spawner.GetComponent<Spawner>();
            this.spawnerList.Add(obj);
            obj.Initialize();
        }
    }

    private void InitPrefabs() {
        this.defenderPrefabs.Add(TypeDefender.MELEE, Resources.Load<GameObject>("Prefabs/Defenders/Guard"));
        this.defenderPrefabs.Add(TypeDefender.RANGE, Resources.Load<GameObject>("Prefabs/Defenders/Archer"));
        this.defenderPrefabs.Add(TypeDefender.SUPPORT, Resources.Load<GameObject>("Prefabs/Defenders/Support"));
        this.defenderPrefabs.Add(TypeDefender.LEADER, Resources.Load<GameObject>("Prefabs/Defenders/Leader"));
    }

    public void Refresh() {
        for (int i = this.leadersList.Count - 1; i >= 0; i--) {
            this.leadersList[i].Refresh();
        }
        for(int i  = this.spawnerList.Count - 1; i >= 0; i--) {
            this.spawnerList[i].Refresh();
        }
    }
    public void PhysicRefresh() {
        for (int i = this.leadersList.Count - 1; i >= 0; i--) {
            this.leadersList[i].PhysicRefresh();
        }
    }

    public void AddLeaderToList(Leader leader) {
        this.leadersList.Add(leader);
    }
    public void RemoveLeaderToList(Leader leader) {
        this.leadersList.Remove(leader);
    }
}
