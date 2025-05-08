using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;

    public float attackingDistance = 1f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>();
        agent = animator.transform.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Should unit transition to Idle State?
        if (attackController.target == null)
        {
            animator.SetBool("isFollowing", false);
        }
        else
        {
            // If there is no other direct command to move
            if (animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
            {
                // Moving unit towards enemy?
                agent.SetDestination(attackController.target.position);
                animator.transform.LookAt(attackController.target.position);

                // Should unit transition to Attack State?
                //float distanceFromTarget = Vector3.Distance(attackController.target.position, animator.transform.position);
                //if (distanceFromTarget < attackingDistance)
                //{
                //    animator.SetBool("isAttacking", true);  // Move to Attacking State

                //}
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);

    }
}
