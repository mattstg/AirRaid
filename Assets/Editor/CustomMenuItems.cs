using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomMenuItems : MonoBehaviour
{
    public static readonly HashSet<string> destructibleThings = new HashSet<string>() { "Bldg", "Tower", "Wall", "Prop" };


    //Tool for setting up all the custom colliders needed for the imported assest
    [MenuItem("Custom Menu/Setup Colliders")]
    public static void SetupSceneColliders()
    {
        Transform selectedItem = Selection.activeTransform; //Get the clicked on transform
        if (!selectedItem)
        {
            Debug.LogError("error, nothing selected");
            return; //quit function
        }
    }


}
