using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWarriorMovement : MonoBehaviour {
	private PlayerInputActions playerControls;
	public FireWarriorController2D controller;
	public Animator animator;

    [SerializeField] private float runSpeed = 40f;

	float horizontalMove = 0f;
	bool isJumping = false;
	bool isDashing = false;


    private void Awake() {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable() {
        playerControls.Player.Move.Enable();
		playerControls.Player.Jump.Enable();
		playerControls.Player.Dash.Enable();
    }

    private void OnDisable() {
        playerControls.Player.Move.Disable();
        playerControls.Player.Jump.Disable();
        playerControls.Player.Dash.Disable();
    }

    // Update is called once per frame
    void Update () {

	}

	public void OnFall() {
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding() {
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()	{
		isJumping = playerControls.Player.Jump.triggered;
		horizontalMove = playerControls.Player.Move.ReadValue<Vector2>().x * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		isDashing = playerControls.Player.Dash.triggered;
		controller.Move(horizontalMove * Time.fixedDeltaTime, isJumping, isDashing);
		isJumping = false;
		isDashing = false;
	}
}
