using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : AnimatedEnemy
{
    public bool die;
    readonly float MAX_HP = 100f;

    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        hp = MAX_HP;
    }
    public override void Refresh()
    {
        base.Refresh();
        if (die)
        {
            Die();
        }

    }
    public void FixedRefresh()
    {

    }
    public override void Die()
    {
        base.Die();
        anim.SetTrigger("isDead");
        base.Die();
    }
}
