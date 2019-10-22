using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityShape { Box, Sphere }
[CreateAssetMenu (fileName ="EAb_AbilityName" , menuName = "EnemyAbility/Area/General")]
public class AOE_Ability : EnemyAbility
{
    //All of them are hidden because they are dynamic in the inspector / Set by AOE_Editor
    [HideInInspector] public float radius;
    [HideInInspector] public Vector2 angle; 
    [HideInInspector] public Vector2 boxSize; 
    [HideInInspector] public AbilityShape shape;

    public override void Initialize(AnimatedEnemy _enemy)
    {
        Debug.Log("init called " + _enemy.Id);
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
        switch (shape)
        {
            case AbilityShape.Box:
                BoxRay();
                break;
            case AbilityShape.Sphere:
                SphereRay();
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
            foreach (Collider collider in colliders)
            {
                IHittable hit = collider.GetComponent<IHittable>();
                hit.HitByProjectile(damage);
                enemy.ModEnergy(damage); // Gain as much energy as it inflict damage - Should be balanced
            }
        }
    }
    void SphereRay()
    {

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
        else if (shape == AbilityShape.Sphere)
        {

        }
        return false;
    }



}