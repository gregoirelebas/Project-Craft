using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 12.0f;
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float jumpHeight = 3.0f;

	[SerializeField] private Transform groundCheck = null;
	[SerializeField] private float groundDistance = 0.4f;
	[SerializeField] private LayerMask groundMask = 0;

	private CharacterController controller = null;
	private Vector3 velocity = Vector3.zero;
	private bool isGrounded = false;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0.0f)
		{
			velocity.y = -2.0f;
		}

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;

		controller.Move(move * speed * Time.deltaTime);

		if (isGrounded && Input.GetButtonDown("Jump"))
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
	}
}
