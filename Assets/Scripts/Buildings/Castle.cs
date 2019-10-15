using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Building
{
    public override void BuildingDied()
    {
        BuildingManager.Instance.CastleDied();
    }
}
