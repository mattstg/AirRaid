using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/TagFilter")]
public class TagFilter : ContextFilter
{
    public string tag;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in original)
        {
            if (tag == item.tag)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
