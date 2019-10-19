using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Ability : EnemyAbility
{
    public float radius; //Radius of ability
    public Vector2 angle; // Could be a cone, from 

    public override void Initialize(Enemy _enemy)
    {
        base.Initialize(_enemy);
    }

    public override void TriggerAbility()
    {
        throw new System.NotImplementedException();
    }
}
