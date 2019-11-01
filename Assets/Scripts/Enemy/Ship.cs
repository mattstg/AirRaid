using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Enemy
{
    public override void Initialize(float startingEnergy)
    {
        isAlive = true;
        ModEnergy(startingEnergy);
    }
    public override void Refresh()
    {
        base.Refresh();
    }


    protected override void Resize()
    {
        
    }






}

