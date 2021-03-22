using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
	protected Player player = null;
	protected float gravity = 0.0f;

	/// <summary>
	/// Set the player script reference.
	/// </summary>
	public void SetPlayer(Player player)
	{
		this.player = player;
	}

	/// <summary>
	/// Call when you enter this state the first time.
	/// </summary>
	public virtual void OnEnterState()
	{
		gravity = player.GetGravity();
	}

	/// <summary>
	/// Call when you change and exit this state.
	/// </summary>
	public virtual void OnExitState()
	{

	}

	/// <summary>
	/// Call on Monobehaviour Update().
	/// </summary>
	public virtual void OnUpdate(ref Vector3 movement)
	{

	}

	/// <summary>
	/// Call when player receive jump input.
	/// </summary>
	public virtual void OnJump()
	{

	}

	/// <summary>
	/// Call when player receive interaction input.
	/// </summary>
	public virtual void OnInteract()
	{

	}
}
