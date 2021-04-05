using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerBaseState
{
	[SerializeField] private float moveSpeed = 12.0f;
	[SerializeField] private float sprintMultiplier = 2.0f;
	[SerializeField] private float jumpHeight = 3.0f;

	private bool jumpInput = false;

	public override void OnEnterState()
	{
		base.OnEnterState();
	}

	public override void OnUpdate(ref Vector3 movement)
	{
		movement.y += gravity * Time.deltaTime;

		movement.x = 0.0f;
		movement.z = 0.0f;

		Vector2 moveInput = player.GetMoveInput();

		if (player.GetSprintInput())
		{
			moveInput.x *= sprintMultiplier;
			moveInput.y *= sprintMultiplier;
		}

		movement.x += moveInput.x * moveSpeed;
		movement.z += moveInput.y * moveSpeed;

		if (jumpInput)
		{
			jumpInput = false;

			movement.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
			return;
		}
	}

	public override void OnJump()
	{
		jumpInput = true;
	}
}
