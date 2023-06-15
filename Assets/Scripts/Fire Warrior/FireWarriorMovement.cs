using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireWarriorMovement : MonoBehaviour {
	//Config:
	private PlayerInputActions playerControls;
	public FireWarriorController2D controller;
	public Animator animator;

    [SerializeField] private float runSpeed = 40f;

	//States:
	private float horizontalMove = 0f;
	private bool isJumping = false;
	private bool isDashing = false;
	//bool dashAxis = false;

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
		// horizontalMove = playerControls.Player.Move.ReadValue<Vector2>().x * runSpeed;	//Input.GetAxisRaw("Horizontal") * runSpeed;
		// animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		// isJumping = playerControls.Player.Jump.triggered;   // if (Input.GetKeyDown(KeyCode.Space)) { jump = true; }
        // isDashing = playerControls.Player.Dash.triggered;	// if (Input.GetKeyDown(KeyCode.E)) { dash = true; }

		/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/
	}

	public void OnFall() {
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding() {
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, isJumping, isDashing);
        isJumping = false;
        isDashing = false;
        horizontalMove = playerControls.Player.Move.ReadValue<Vector2>().x * runSpeed;  //Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        isJumping = playerControls.Player.Jump.triggered;   // if (Input.GetKeyDown(KeyCode.Space)) { jump = true; }
        isDashing = playerControls.Player.Dash.triggered;   // if (Input.GetKeyDown(KeyCode.E)) { dash = true; }

        /*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/

    }
}
