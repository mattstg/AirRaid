using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Defender : MonoBehaviour{

    public virtual void Initialise() { }
    public virtual void Refresh() { }
    public virtual void PhysicRefresh() { }
    protected virtual void Move() { }
    protected virtual void Rotate() { }
    protected virtual void FindTarget() { }
    protected virtual void DoAbility() { }
    public virtual void Die() { }
}
