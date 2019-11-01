using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum NPCType { Melee,Ranged,Healer}
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
    float timeToSpawnHealer = 120f;
    Transform npcParent;

    public HashSet<Npc> npcs;
    public Stack<Npc> toRemove;
    public Stack<Npc> toAdd;
   
    Dictionary<NPCType, GameObject> npcPrefabDict = new Dictionary<NPCType, GameObject>(); //all Npc prefabs

    // Start is called before the first frame update
    public void Initialize()
    {

        toRemove = new Stack<Npc>();
        toAdd = new Stack<Npc>();
        npcs= new HashSet<Npc>();       

        npcParent = new GameObject("NPCParent").transform;
        npcParent.position = new Vector3(-2,4,85);//GameObject.FindGameObjectWithTag("npcspawnlocation").transform.position;
        foreach (NPCType npctype in System.Enum.GetValues(typeof(NPCType))) //fill the resource dictionary with all the prefabs
        {
            Debug.Log("Prefabs loaded"+npctype);
            npcPrefabDict.Add(npctype, Resources.Load<GameObject>("Prefabs/Humanoids/" + npctype.ToString())); //Each enum matches the name of the NPC perfectly
        }
        Debug.Log("Initialize done");
       
        NpcSpawnlocation = new GameObject().transform;
        NpcSpawnlocation.position = npcParent.position;
    }
    public void PostInitialize()
    {

    }
    public void PhysicsRefresh()
    {

    }
    public Npc GetRandomNPC()
    {
        return npcs.GetRandomElement<Npc>();
    }
    public void Refresh()
    {
        foreach (Npc e in npcs)
            if (e.isAlive)
                e.Refresh();


        while (toRemove.Count > 0)
        {
            Npc e = toRemove.Pop();
            npcs.Remove(e);
            GameObject.Destroy(e.gameObject);
        }

        while (toAdd.Count > 0)
        {
            npcs.Add(toAdd.Pop());
        }

        timeToSpawnRanged -= Time.deltaTime;
        if (timeToSpawnRanged <= 0)
        {
            timeToSpawnRanged = 10f;
             NPCManager.Instance.SpawnNpc(NPCType.Ranged, NpcSpawnlocation.position, 10f); //Spawn an npc on spawn location
        }

        
        timeToSpawnMelee -= Time.deltaTime;
        if (timeToSpawnMelee <= 0)
        {
            timeToSpawnMelee = 30f;
             NPCManager.Instance.SpawnNpc(NPCType.Melee, NpcSpawnlocation.position, 20f); //Spawn an npc on spawn location
        }

        timeToSpawnHealer -= Time.deltaTime;
        if (timeToSpawnHealer <= 0)
        {
            timeToSpawnHealer = 30f;
             NPCManager.Instance.SpawnNpc(NPCType.Healer, NpcSpawnlocation.position, 40f); //Spawn an npc on spawn location
        }

    }

    public void NpcDied(Npc npcDied)
    {
        toRemove.Push(npcDied);
    }
    
    public Npc SpawnNpc(NPCType npcType, Vector3 spawnLoc, float startingEnergy)
    {
        GameObject newNpc;        
        newNpc = GameObject.Instantiate(npcPrefabDict[npcType], npcParent);       //create from prefab
        newNpc.transform.position = spawnLoc;     //set the position
        Npc e= newNpc.GetComponent<Npc>();          
        e.Initialize(startingEnergy);               //initialize the Npc
        toAdd.Push(e);                              //add to update list
        return e;
    }
}
