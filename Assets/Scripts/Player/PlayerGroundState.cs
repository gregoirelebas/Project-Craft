using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerBaseState
{
	[SerializeField] private float moveSpeed = 12.0f;
	[SerializeField] private float sprintSpeed = 20.0f;
	[SerializeField] private float jumpHeight = 3.0f;

	private bool jumpInput = false;
	private bool sprintInput = false;

	public override void OnUpdate(ref Vector3 movement)
	{
		if (player.IsGrounded())
		{
			movement.y += gravity * Time.deltaTime;
			if (movement.y < 0.0f)
			{
				movement.y = -2.0f;
			}

			movement.x = 0.0f;
			movement.z = 0.0f;

			Vector2 moveInput = player.GetMoveInput();
			movement += player.transform.right * moveInput.x * (sprintInput ? sprintSpeed : moveSpeed);
			movement += player.transform.forward * moveInput.y * (sprintInput ? sprintSpeed : moveSpeed);

			if (jumpInput)
			{
				jumpInput = false;

				movement.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
				return;
			}
		}
		else
		{
			player.SetState(PlayerState.Air);
			return;
		}
	}

	public override void OnJump()
	{
		jumpInput = true;
	}

	public override void OnSprint(bool sprint)
	{
		sprintInput = sprint;
		Debug.Log(sprint);
	}
}
