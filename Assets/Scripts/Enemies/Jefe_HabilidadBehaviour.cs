using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe_HabilidadBehaviour : StateMachineBehaviour
{
    [SerializeField] private GameObject habilidad;
    [SerializeField] private float offsetY;
    private Jefe jefe;
    private Transform fireWarriorPrefab;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        fireWarriorPrefab = jefe.fireWarriorPrefab;
        jefe.MirarJugador();
        Vector2 posicionAparicion = new Vector2(fireWarriorPrefab.position.x, fireWarriorPrefab.position.y + offsetY);
        Instantiate(habilidad, posicionAparicion, Quaternion.identity);


    }
    /*
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void onstateupdate(animator animator, animatorstateinfo stateinfo, int layerindex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    */
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
