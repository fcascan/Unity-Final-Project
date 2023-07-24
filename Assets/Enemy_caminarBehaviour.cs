using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_caminarBehaviour : StateMachineBehaviour
{
    
    private EnemyBehaviour skeleton;
    private Rigidbody2D m_Rigidbody2D;
    public Transform fireWarriorPrefab;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireWarriorPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        skeleton = animator.GetComponent<EnemyBehaviour>();
        m_Rigidbody2D = skeleton.m_Rigidbody2D;
        skeleton.MirarJugador();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
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
