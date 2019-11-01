using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Enemy

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






}

