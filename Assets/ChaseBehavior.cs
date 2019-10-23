﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : StateMachineBehaviour
{
    AnimatedEnemy ae;
    Time timeBetweenLastDecision;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ae == null)
            ae = animator.GetComponent<AnimatedEnemy>();
        ae.navmeshAgent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ae.CheckTargetDestroy() || !ae.CheckIfPathPossible())
        {
            animator.SetBool("isTarget", false);
            return;
        }
        ae.lastTargetPosition = ae.target.transform.position;
        Physics.OverlapSphere(ae.transform.position, 1);
        if (CheckIfInRange()) // May create bugs
        {

            if (ae.CanMakeDecision)
            {
                ae.navmeshAgent.isStopped = true;
                ae.timeSinceLastDecision = Time.time;
                animator.SetTrigger("attack");
            }
        
        }
        animator.SetFloat("Velocity", ae.navmeshAgent.velocity.magnitude / ae.navmeshAgent.speed);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ae.transform.LookAt(ae.lastTargetPosition);
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
    public bool CheckIfInRange()
    {
        Collider[] collider = Physics.OverlapSphere(ae.transform.position, (ae.transform.localScale.z / 2) + 2);
        foreach(Collider colid in collider)
        {
            if(colid.gameObject == ae.target)
            {
                return true;
            }
        }
        return false;
    }
}
