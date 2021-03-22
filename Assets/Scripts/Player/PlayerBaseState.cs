using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
	protected Player player = null;
	protected float gravity = 0.0f;

	public void SetPlayer(Player player)
	{
		this.player = player;
	}

	public virtual void OnEnterState()
	{
		gravity = player.GetGravity();
	}

	public virtual void OnExitState()
	{

	}

	public virtual void OnUpdate(ref Vector3 movement)
	{

	}
}
