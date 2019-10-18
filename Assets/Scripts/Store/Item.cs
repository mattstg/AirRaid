using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name { get; set; }

    public string Description { get; set; }

    public float Cost { get; set; }

    public int Level { get; set; }

    public string ImgSrc { get; set; }

    public BodyPart Part;

    public bool Purchased { get; set; } = false;

    Item(string name, string description, float cost, int level, string imgSrc, BodyPart part)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Level = level;
        ImgSrc = imgSrc;
        Part = part;
    }

}
