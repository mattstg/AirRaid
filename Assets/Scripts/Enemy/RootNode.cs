using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode  : MonoBehaviour
{
    public static readonly Vector2 timeToMakeNewNode = new Vector2(15f,35f);    
    public static readonly float rootDistance = 6.5f;
    public static readonly float timePerNodeInSystem = 2.5f;          //Time to spawn new root increases by total nodes in system


    RootSystem rootSystemParent;
    float timeIncreaseForNextNode;
    float timeOfNextNode;
    RootNode[] neighborNodes;
    int numOfNodes = 1; 
    
    public void Initialize(RootNode spawningRootNode, RootSystem _rootSystemParent)
    {
        neighborNodes = new RootNode[4] { spawningRootNode, null, null, null };
        timeOfNextNode = Time.time + Random.Range(timeToMakeNewNode.x, timeToMakeNewNode.y) * numOfNodes;
        rootSystemParent = _rootSystemParent;
        rootSystemParent.numberOfRoots++;
    }

    public void DestroyRootSystem()
    {
        for (int i = 1; i < 4; i++) //skip first one, since that is the parent
            if (neighborNodes[i])
                neighborNodes[i].DestroyRootSystem();
        GameObject.Destroy(gameObject);
    }

    public void RefreshRootNode()
    {
        if (numOfNodes < 4 && Time.time >= timeOfNextNode)
        {
            neighborNodes[numOfNodes] = SpawnNewRootNode(this);
            numOfNodes++;
            timeOfNextNode = Time.time + Random.Range(timeToMakeNewNode.x, timeToMakeNewNode.y) * numOfNodes + rootSystemParent.numberOfRoots * timePerNodeInSystem;
        }

        for (int i = 1; i < 4; i++) //skip first one, since that is the parent
            if (neighborNodes[i])
                neighborNodes[i].RefreshRootNode();
    }

   
    public RootNode SpawnNewRootNode(RootNode parentNode)
    {


        Vector2 dir = Random.insideUnitCircle.normalized*rootDistance;
        GameObject newRootNodeObj = GameObject.Instantiate(EnemyManager.rootPrefab);
        newRootNodeObj.transform.position = new Vector3(dir.x + parentNode.transform.position.x, 1, dir.y + parentNode.transform.position.z);
        newRootNodeObj.transform.SetParent(EnemyManager.Instance.rootNodeParent);
        newRootNodeObj.GetComponent<LineRenderer>().SetPosition(0, new Vector3(newRootNodeObj.transform.position.x,1, newRootNodeObj.transform.position.z));
        newRootNodeObj.GetComponent<LineRenderer>().SetPosition(1, new Vector3(parentNode.transform.position.x, 1, parentNode.transform.position.z));
        RootNode newRootNode = newRootNodeObj.GetComponent<RootNode>();
        newRootNode.Initialize(this, this.rootSystemParent);
        return newRootNode;
    }
}
