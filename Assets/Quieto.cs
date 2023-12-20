using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Quieto : StateMachineBehaviour
{
    [SerializeField] public Transform positionFloor;
    [SerializeField] float distance;
    [SerializeField] LayerMask layerFloor;
    private Enemy_0 enemy_0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy_0 = animator.gameObject.GetComponent<Enemy_0>();

        positionFloor = enemy_0.positionDetectorFloor;
        animator.SetBool("outFloor", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!DetectFloor())
        {
            if(enemy_0.transform.localEulerAngles.y == 0){
                enemy_0.transform.localEulerAngles = new Vector3(0, 180, 0);
            }else{
                enemy_0.transform.localEulerAngles = new Vector3(0, 0, 0);    
            }
        }
        else
        {
            animator.SetBool("walk", true);
            animator.SetBool("idel", false);
            animator.SetBool("outFloor", false);
        }
    }
    private bool DetectFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(positionFloor.position, Vector2.down, distance, layerFloor);

        return hit.collider != null;
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
