using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminando : StateMachineBehaviour
{
    private Enemy_0 enemy_0;
    [SerializeField] Transform positionFloor;
    [SerializeField] float distance;
    [SerializeField] LayerMask layerFloor;
    [SerializeField] Rigidbody2D rgb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy_0 = animator.gameObject.GetComponent<Enemy_0>();
        positionFloor = enemy_0.positionDetectorFloor.transform;
        rgb = enemy_0.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy_0.transform.localEulerAngles.y == 0)
        {
            rgb.velocity = new Vector2(2, rgb.velocity.y);
        }
        if (enemy_0.transform.localEulerAngles.y == 180)
        {
            rgb.velocity = new Vector2(-2, rgb.velocity.y);
        }
        if (!DetectFloor())
        {
            animator.SetBool("walk", false);
            animator.SetBool("idel", true);
            animator.SetBool("outFloor", true);
            rgb.velocity = new Vector2(0, rgb.velocity.y);
        }
        else
        {
            animator.SetBool("walk", true);
            animator.SetBool("idel", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
