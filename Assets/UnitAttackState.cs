using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;

    public float stopAttackingDistance = 1.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<AttackController>();

        attackController.setAttackMaterial();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackController.target != null && animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
        {
            lookAtTarget();

            // Keep moving towards enemy
            agent.SetDestination(attackController.target.position);

            var damageToInflict = attackController.unitDamage;
            attackController.target.GetComponent<Enemy>().ReceiveDamage(damageToInflict);

            // Should unit still attack
            float distanceFromTarget = Vector3.Distance(attackController.target.position, animator.transform.position);
            if (distanceFromTarget > stopAttackingDistance || attackController.target == null)
            {
                animator.SetBool("isAttacking", false);  // Move to Follow State
            }
        }
    }

    private void lookAtTarget()
    {
        Vector3 direction = attackController.target.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
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
