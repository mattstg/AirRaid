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

    public override void Initialize(Enemy _enemy)
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

    void Main() // Jsais pas comment la nommer, should refactor
    {
        Debug.Log("AOE Ability");
    }
}
