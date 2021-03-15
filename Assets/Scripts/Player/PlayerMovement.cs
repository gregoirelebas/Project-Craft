using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 12.0f;
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float jumpHeight = 3.0f;

	[SerializeField] private Transform groundCheck = null;
	[SerializeField] private float groundDistance = 0.4f;
	[SerializeField] private LayerMask groundMask = 0;

	private Vector2 moveInput = Vector2.zero;

	private CharacterController controller = null;
	private Vector3 velocity = Vector3.zero;
	private bool isGrounded = false;
	private bool jumpInput = false;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();

		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		velocity.y += gravity * Time.deltaTime;

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		Vector3 move;

		//Ground control
		if (isGrounded)
		{
			move = transform.right * moveInput.x + transform.forward * moveInput.y;			

			if (velocity.y < 0.0f)
			{
				velocity.y = -2.0f;
			}

			if (jumpInput)
			{
				jumpInput = false;
				velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
			}
		}
		//Air control
		else
		{
			move = transform.right * moveInput.x + transform.forward * moveInput.y;
		}

		controller.Move(move * speed * Time.deltaTime);
		controller.Move(velocity * Time.deltaTime);
	}

	/// <summary>
	/// Read the movement value.
	/// </summary>
	private void OnMove(InputValue inputValue)
	{
		moveInput = inputValue.Get<Vector2>();
	}

	/// <summary>
	/// Read the jump input.
	/// </summary>
	private void OnJump(InputValue inputValue)
	{
		jumpInput = inputValue.isPressed;
	}
}
