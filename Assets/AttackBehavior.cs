using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    AnimatedEnemy enemy;
    EnemyAbility ability;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null)
            enemy = animator.GetComponent<AnimatedEnemy>();
        EnemyAbility temp;
        if (enemy.target != null)
       { 
            //Cycle every ability, check of can use it and if it will hit the target
            for (int i = 0; i < enemy.enemyAbilityManager.abilities.Count; i++)
            {
                //Create a shorthand
                temp = enemy.enemyAbilityManager.abilities[i];
                if (temp.canUseAbility && temp.WillHitTarget())
                {
                    ability = temp;
                    break;
                }
            }
            if(ability == null)
            {
                animator.SetTrigger("cantAttack");
            }
            else
            {
                enemy.animatorOverrideController["attack"] = ability.animation;
                ability.UseAbility();
            }
       }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
