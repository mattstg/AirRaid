using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum NPCType { Melee,Ranged }//, Ranged, Helper }
public class NPCManager 
{

    #region Singleton
    private static NPCManager instance;
    private NPCManager() { }
    public static NPCManager Instance { get { return instance ?? (instance = new NPCManager()); } }
    #endregion
    Transform NpcSpawnlocation;
    float timeToSpawnRanged = 30f;
    float timeToSpawnMelee = 60.0f;

    Transform npcParent;
    //npc ranged
    public HashSet<Npc> npcsRanged;
    public Stack<Npc> toRemoveRanged;
    public Stack<Npc> toAddRanged;
    //npc melee
    public HashSet<Melee> npcMelees;
    public Stack<Melee> toRemoveMelees;
    public Stack<Melee> toAddMelees;

    Dictionary<NPCType, GameObject> npcPrefabDict = new Dictionary<NPCType, GameObject>(); //all Npc prefabs

    // Start is called before the first frame update
    public void Initialize()
    {

        //range
        toRemoveRanged = new Stack<Npc>();
        toAddRanged = new Stack<Npc>();
        npcsRanged = new HashSet<Npc>();
        //melee
        toRemoveMelees = new Stack<Melee>();
        toAddMelees = new Stack<Melee>();
        npcMelees = new HashSet<Melee>();
        
        npcParent = new GameObject("NPCParent").transform;
        npcParent.position = new Vector3(-2,3,85);//GameObject.FindGameObjectWithTag("npcspawnlocation").transform.position;
        foreach (NPCType npctype in System.Enum.GetValues(typeof(NPCType))) //fill the resource dictionary with all the prefabs
        {
            Debug.Log("Prefabs loaded"+npctype);
            npcPrefabDict.Add(npctype, Resources.Load<GameObject>("Prefabs/npc/" + npctype.ToString())); //Each enum matches the name of the NPC perfectly
        }
        Debug.Log("Initialize done");
        //SpawnNpc(NPCType.Melee,npcParent.position,10f);
        NpcSpawnlocation = GameObject.FindGameObjectWithTag("npcspawnlocation").transform;
    }
    public void PostInitialize()
    {

    }
    public void PhysicsRefresh()
    {

    }
    public void Refresh()
    {
        foreach (Npc e in npcsRanged)
            if (e.isAlive)
                e.Refresh();


        while (toRemoveRanged.Count > 0) //remove all dead ones
        {
            Npc e = toRemoveRanged.Pop();
            npcsRanged.Remove(e);
            GameObject.Destroy(e.gameObject);
        }

        while (toAddRanged.Count > 0)
        {//Add new ones
            npcsRanged.Add(toAddRanged.Pop());
        }

        timeToSpawnRanged -= Time.deltaTime;
        if (timeToSpawnRanged <= 0)
        {
            timeToSpawnRanged = 10f;
             NPCManager.Instance.SpawnNpc(NPCType.Ranged, NpcSpawnlocation.position, 10f); //Spawn an npc on spawn location
        }
        timeToSpawnMelee -= Time.deltaTime;

        ///////////////////////////////////////////////////////////

        foreach (Melee e in npcMelees)
            if (e.isAlive)
                e.Refresh();


        while (toRemoveMelees.Count > 0) //remove all dead ones
        {
            Melee e = toRemoveMelees.Pop();
            npcMelees.Remove(e);
            GameObject.Destroy(e.gameObject);
        }

        while (toAddMelees.Count > 0)
        {
            npcMelees.Add(toAddMelees.Pop());            
        }
        if (timeToSpawnMelee <= 0)
        {
            timeToSpawnMelee = 30f;
             NPCManager.Instance.SpawnMelee(NPCType.Melee, NpcSpawnlocation.position, 20f); //Spawn an npc on spawn location
        }

    }

    public void NpcDiedRanged(Npc npcDied)
    {
        toRemoveRanged.Push(npcDied);
    }
    public void NpcDiedMelee(Melee npcDied)
    {
        toRemoveMelees.Push(npcDied);
    }
    public Npc SpawnNpc(NPCType npcType, Vector3 spawnLoc, float startingEnergy)
    {
        Debug.Log("NPC ranged spawned");
        GameObject newNpc;        
        newNpc = GameObject.Instantiate(npcPrefabDict[npcType], npcParent);       //create from prefab
        newNpc.transform.position = spawnLoc;     //set the position
        Npc e= newNpc.GetComponent<Npc>();   //get the ranged component on the newly created obj        
        e.Initialize(startingEnergy);               //initialize the Npc
        toAddRanged.Push(e);                              //add to update list
        return e;
    }
    public Melee SpawnMelee(NPCType npcType, Vector3 spawnLoc, float startingEnergy)
    {
        Debug.Log("NPC Melee spawned");
        GameObject newNpc;
        newNpc = GameObject.Instantiate(npcPrefabDict[npcType], npcParent);       //create from prefab
        newNpc.transform.position = spawnLoc;     //set the position
        Melee e = newNpc.GetComponent<Melee>();   //get the melee component on the newly created obj
        e.Initialize(startingEnergy);               //initialize the Npc
        toAddMelees.Push(e);                              //add to update list
        return e;
    }
}
