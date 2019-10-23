using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MakeScriptableItem", order = 1)]
public class Item : ScriptableObject {
    public string Name;

    public string Description;

    public float Cost;

    public int Level;

    public Sprite imgSprite;

    public BodyPart Part;

    public bool Purchased = false;
}
