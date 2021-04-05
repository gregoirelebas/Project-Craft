using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAirState : PlayerBaseState
{
	[SerializeField] private float inertia = 0.1f;
	[SerializeField] private float sprintMultiplier = 2.0f;
	[SerializeField] private float maxSpeed = 20.0f;

	public override void OnEnterState()
	{
		base.OnEnterState();

		if (player.GetSprintInput())
		{
			maxSpeed *= sprintMultiplier;
		}
	}

	public override void OnUpdate(ref Vector3 movement)
	{
		movement.y += gravity * Time.deltaTime;

		Vector2 moveInput = player.GetMoveInput();

		if (player.GetSprintInput())
		{
			moveInput.x *= sprintMultiplier;
			moveInput.y *= sprintMultiplier;
		}

		Debug.Log(maxSpeed);

		movement.x += inertia * moveInput.x;
		movement.z += inertia * moveInput.y;

		movement.x = Mathf.Clamp(movement.x, -maxSpeed, maxSpeed);
		movement.z = Mathf.Clamp(movement.z, -maxSpeed, maxSpeed);
	}

	public override void OnExitState()
	{
		if (player.GetSprintInput())
		{
			maxSpeed /= sprintMultiplier;
		}
	}

	public override void OnSprint(bool sprint)
	{
		if (sprint)
		{
			maxSpeed *= sprintMultiplier;
		}
		else
		{
			maxSpeed /= sprintMultiplier;
		}
	}
}
