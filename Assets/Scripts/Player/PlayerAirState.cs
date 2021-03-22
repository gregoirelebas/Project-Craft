using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAirState : PlayerBaseState
{
	[SerializeField] private float moveSpeed = 6.0f;

	public override void OnUpdate(ref Vector3 movement)
	{
		if (!player.IsGrounded())
		{
			movement.y += gravity * Time.deltaTime;

			movement.x = 0.0f;
			movement.z = 0.0f;

			Vector2 moveInput = player.GetMoveInput();
			movement += player.transform.right * moveInput.x * moveSpeed;
			movement += player.transform.forward * moveInput.y * moveSpeed;
		}
		else
		{
			player.SetState(PlayerState.Ground);
		}
	}
}
