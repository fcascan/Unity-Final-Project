using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private PlayerInputActions playerControls;
    public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;

    //bool dashAxis = false;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls.Player.Move.Enable();
        playerControls.Player.Jump.Enable();
        playerControls.Player.Dash.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Move.Disable();
        playerControls.Player.Jump.Disable();
        playerControls.Player.Dash.Disable();
    }

    // Update is called once per frame
    void Update () {

        horizontalMove = playerControls.Player.Move.ReadValue<Vector2>().x * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		/*if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			dash = true;
		}*/

		jump = playerControls.Player.Jump.triggered;
		dash = playerControls.Player.Dash.triggered;

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

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}
}
