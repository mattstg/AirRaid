using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="EAb_AbilityName" , menuName = "EnemyAbility/Melee/General")]
public class Melee_Ability : EnemyAbility
{
    public enum AbilityShape { Ray, Box }
    //All of them are hidden because they are dynamic in the inspector / Set by AOE_Editor
    [HideInInspector] public Vector2 boxSize; 
    [HideInInspector] public AbilityShape shape;

    public override void Initialize(AnimatedEnemy _enemy)
    {
        enemy = _enemy;
    }
    public override void UseAbility()
    {
       if(timeBeforeHit > 0)
        {
            enemy.StartCoroutine(MustWaitBeforeHit(timeBeforeHit));
        }
        else
        {
            Main();
        }
    }

    IEnumerator MustWaitBeforeHit(float time)
    {
        yield return new WaitForSeconds(time);
        Main();
    }

    void Main()
    {
        timeLastUsed = Time.time;
        switch (shape)
        {
            case AbilityShape.Box:
                BoxRay();
                break;
            case AbilityShape.Ray:
                Ray();
                break;
            default:
                Debug.Log("Unhandled shape: AOE Ability");
                break;
        }
    }

    
    void BoxRay()
    {
        Collider[] colliders = Physics.OverlapBox(enemy.lastTargetPosition, new Vector3(boxSize.x / 2, 3, boxSize.y / 2), Quaternion.identity, hittableLayer);
        if (colliders.Length != 0)
        {
            
            IHittable hit = colliders[0].GetComponent<IHittable>();
            hit.HitByProjectile(damage);
            enemy.ModEnergy(damage); // Gain as much energy as it inflict damage - Should be balanced
        }
    }
    void Ray()
    {
        RaycastHit[] raysInfo = Physics.RaycastAll(enemy.transform.position + Vector3.forward * enemy.transform.localScale.z, Vector3.forward, enemy.transform.localScale.z + range.y - range.x , hittableLayer);


        if (raysInfo.Length > 0)
        {
            foreach (RaycastHit rayInfo in raysInfo) // this wont care if its the closest
            {
                Debug.Log(rayInfo.collider.gameObject != enemy);
                if (rayInfo.collider.gameObject != enemy)
                {
                    IHittable hit = rayInfo.collider.GetComponent<IHittable>();
                    hit.HitByProjectile(damage);
                    enemy.ModEnergy(damage);
                    return;
                }
            }
        }
        
    }
    public override bool WillHitTarget()
    {
        if(shape == AbilityShape.Box)
        {
            Vector3 dir = (enemy.target.transform.position - enemy.transform.position).normalized;

            RaycastHit rayInfo;

            if (!Physics.Raycast(enemy.transform.position, dir, out rayInfo, range.y + (boxSize.y / 2), hittableLayer))
                return false;
            if (rayInfo.distance < range.x - boxSize.y / 2)
                return false;

            float distance = Vector3.Distance(enemy.target.transform.position, enemy.transform.position);

            //if it does, is it too far
            if (distance > range.y)
                enemy.lastTargetPosition = enemy.transform.position + (enemy.target.transform.position - enemy.transform.position).normalized * range.y;
            //or too close
            else if (distance < range.x)
                enemy.lastTargetPosition = enemy.transform.position + (enemy.target.transform.position - enemy.transform.position).normalized * range.x;
            //or in range
            else
                enemy.lastTargetPosition = enemy.target.transform.position;
            return true;
        }
        else if (shape == AbilityShape.Ray)
        {
            Vector3 dir = (enemy.target.transform.position - enemy.transform.position).normalized;

            
            
            /*if (rayInfo.distance < range.x)
                return false;
                */
            enemy.lastTargetPosition = enemy.target.transform.position;
            return true;
        }
        Debug.Log("Unhandled Shape : MeleeAbility in WillHitTarget()", this);
        return false;
    }



}