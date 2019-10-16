using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour{

    public DefenderPck defenderInfos;

    public virtual void Initialise(DefenderPck defenderPck) { }
    public virtual void Refresh() { }
    public virtual void PhysicRefresh() { }
    public virtual void FindTarget() { }
    public virtual void DoAbility() { }
    public virtual void Die() { }
}
