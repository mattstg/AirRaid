using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootedEnemy : Enemy
{
    static readonly float ENERGY_GAIN_PER_ROOT = .15f;


    //Rooted enemy stats
    protected RootSystem rootNodeSystem;
    protected bool isRooted;

    public override void Refresh()
    {
        base.Refresh();
        if (isRooted)
        {
            ModEnergy(rootNodeSystem.numberOfRoots * Time.deltaTime * ENERGY_GAIN_PER_ROOT); //gain energy and hp
            rootNodeSystem.Refresh();
        }        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isRooted && !collision.transform.CompareTag("Player"))
        {
            Destroy(GetComponent<Rigidbody>()); //To root it in place, we remove it from the dynamic physics system, since it shouldnt move anymore once rooted
            isRooted = true;
            if(rootNodeSystem == null)          //The root system can already exist if we inherited one from an existing egg
                rootNodeSystem = new RootSystem(transform.position);
        }
    }

    //We can link to an existing root node system, that must mean that we are rooted then
    public void LinkToRootSystem(RootSystem existingRootNodeSys)
    {
        rootNodeSystem = existingRootNodeSys;
        Destroy(GetComponent<Rigidbody>()); //If it exists, destroy it, otherwise itll just destroy null
        isRooted = true;
    }

    public override void Die()
    {
        rootNodeSystem.RootSystemDied();
        base.Die();
    }
}
