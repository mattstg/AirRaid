using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleBehavior : StateMachineBehaviour
{
    AnimatedEnemy ae;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ae == null)
            ae = animator.GetComponent<AnimatedEnemy>();
        //if (ae.target != null)
        //    animator.SetBool("isTarget", true);
       // else
        //    animator.SetBool("isTarget", false);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider targetColider = null;
        int compteur = 1; // Use the detectionRadius variable instead
        if (!animator.GetBool("isTarget"))
        {
            do
            {
                Collider[] collid = Physics.OverlapSphere(ae.transform.position, 20 * compteur, LayerMask.GetMask("Wall", "Building", "Defender"));
                if (collid.Length != 0)
                {
                    targetColider = collid[Random.Range(0, collid.Length)];
                    ae.target = targetColider.gameObject;
                    ae.SetAgent(targetColider.ClosestPoint(ae.transform.position));
                        
                    animator.SetBool("isTarget", true);
                }
                compteur++;
            } while (targetColider == null && compteur < 85);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

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
