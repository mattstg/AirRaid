using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_HeavyAttack : EnemyAbility
{
    readonly float MAX_RANGE = 1f;
    readonly float ANIMATION_TIME_HIT = 0.2f;
    readonly float HIT_DAMAGE = 1f;

    public Ab_HeavyAttack(Enemy _enemy) : base(_enemy)
    {
        stats = new AbilityStats(this, AbilityType.Melee, UpdateType.Update, 5f, ANIMATION_TIME_HIT); //Changer le TimeBeforeHit pour matcher l'animation
    }

    public override bool UseAbility()
    {
        if (base.UseAbility())
        {
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, MAX_RANGE, LayerMask.GetMask("Player, Wall, Building")))
            {
                IHittable ihittable = hit.transform.GetComponent<IHittable>();
                if (ihittable != null)
                    ihittable.HitByProjectile(HIT_DAMAGE);
            }
        }
        return false;
    }

}
