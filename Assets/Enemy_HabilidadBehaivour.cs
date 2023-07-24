using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HabilidadBehaivour : StateMachineBehaviour
{
    [SerializeField] private GameObject habilidad;
    [SerializeField] private float offsetY;
    private EnemyBehaviour skeleton;
    private Transform fireWarriorPrefab;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skeleton = animator.GetComponent<EnemyBehaviour>();
        fireWarriorPrefab = skeleton.fireWarriorPrefab;
        skeleton.MirarJugador();
        Vector2 posicionAparicion = new Vector2(fireWarriorPrefab.position.x, fireWarriorPrefab.position.y + offsetY);
        Instantiate(habilidad, posicionAparicion, Quaternion.identity);


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
