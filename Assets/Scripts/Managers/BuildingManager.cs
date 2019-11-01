using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager 
{
    #region Singleton
    private static BuildingManager instance;
    private BuildingManager() { }
    public static BuildingManager Instance { get { return instance ?? (instance = new BuildingManager()); } }
    #endregion

    [HideInInspector]public List<Building> allBuildings;
    //This class may be mostly useless, there is at the moment no reason to track buildings, we are not updating them or anything
    //So the framework for now is nice, but if we do not need to track buildings, it would be best to delete them after
    public void Initialize()
    {
        int totalNumOfBuildings = GameLinks.gl.buildingParent.transform.childCount + GameLinks.gl.tentParent.transform.childCount + GameLinks.gl.towerParent.transform.childCount + GameLinks.gl.wallParent.transform.childCount + 1; //castle is 1
        allBuildings = new List<Building>(totalNumOfBuildings);
        ExtractAllBuildingsFromParent(GameLinks.gl.buildingParent.transform, 10);
        ExtractAllBuildingsFromParent(GameLinks.gl.tentParent.transform, 5);
        ExtractAllBuildingsFromParent(GameLinks.gl.towerParent.transform, 30);
        ExtractAllBuildingsFromParent(GameLinks.gl.wallParent.transform, 30);

        Castle c = GameLinks.gl.castle.gameObject.AddComponent<Castle>();
        c.Initialize(100);
        allBuildings.Add(c);
    }

    private void ExtractAllBuildingsFromParent(Transform parent, float hp)
    {
        foreach (Transform t in parent) //foreach child
        {
            Building b = t.gameObject.AddComponent<Building>();
            b.Initialize(hp);
            allBuildings.Add(b); //Add a building to it, and add it to the list we are tracking
        }
    }

    public Building GetRandomBuilding()
    {
        return allBuildings[UnityEngine.Random.Range(0, allBuildings.Count)];
    }

    public void PhysicsRefresh()
    {

    }

    public void PostInitialize()
    {

    }

    public void Refresh()
    {
        //Buildings have no need to update
        //for (int i = allBuildings.Count; i >= 0; i--)
        //    allBuildings[i].Refresh();
    }

    public void BuildingDied(Building b)
    {
        allBuildings.Remove(b);
        GameObject.Destroy(b.gameObject);
    }

    public void CastleDied()
    {
        Debug.Log("You lose");
    }
}
