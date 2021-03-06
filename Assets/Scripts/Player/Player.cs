﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
	Ground,
	Air,
	Count //KEEP AT END
}

public class Player : MonoBehaviour
{
	[Header("Camera controls")]
	[SerializeField] private MouseLook mouseLook = null;

	[Header("Player states")]
	[SerializeField] private PlayerGroundState groundState = null;
	[SerializeField] private PlayerAirState airState = null;

	[Header("Ground detection")]
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private Transform groundCheck = null;
	[SerializeField] private float groundDistance = 0.4f;
	[SerializeField] private LayerMask groundMask = 0;

	private PlayerBaseState[] states = null;

	private PlayerState previousState = PlayerState.Ground;
	private PlayerState currentState = PlayerState.Ground;

	private CharacterController controller = null;
	private Vector3 movement = Vector3.zero;

	//Inventory
	private Inventory inventory = null;
	private bool showInventory = false;

	private bool menuMode = false;
	private bool isGrounded = false;
	private bool isSprinting = false;

	private bool showCraftBook = false;

	//Input
	private Vector2 moveInput = Vector2.zero;
	private Vector2 lookInput = Vector2.zero;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();

		states = new PlayerBaseState[(int)PlayerState.Count];

		mouseLook.SetPlayer(this);

		groundState.SetPlayer(this);
		states[(int)PlayerState.Ground] = groundState;

		airState.SetPlayer(this);
		states[(int)PlayerState.Air] = airState;

		inventory = new Inventory(10);
	}

	private void OnEnable()
	{
		EventManager.Instance.StartListening(EventType.OnMenuOpened, OnMenuOpen);
		EventManager.Instance.StartListening(EventType.OnMenuClosed, OnMenuClose);
	}

	private void Start()
	{
		states[(int)currentState].OnEnterState();
	}

	private void Update()
	{
		CheckGround();

		//Block movement if in menu.
		if (!menuMode)
		{
			mouseLook.OnUpdate(ref movement);

			states[(int)currentState].OnUpdate(ref movement);

			controller.Move(transform.TransformDirection(movement) * Time.deltaTime);
		}
	}

	private void OnDisable()
	{
		EventManager.Instance.StartListening(EventType.OnMenuOpened, OnMenuOpen);
		EventManager.Instance.StartListening(EventType.OnMenuClosed, OnMenuClose);
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (isGrounded)
		{
			Gizmos.color = Color.red;
		}
		else
		{
			Gizmos.color = Color.blue;
		}

		Gizmos.DrawSphere(groundCheck.position, groundDistance);
	}
#endif

	#region InputListeners

	private void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}

	private void OnLook(InputValue value)
	{
		lookInput = value.Get<Vector2>();
	}

	private void OnJump(InputValue value)
	{
		states[(int)currentState].OnJump();

		EventManager.Instance.TriggerEvent(EventType.OnPlayerJump);
	}

	private void OnInteract(InputValue value)
	{
		if (!menuMode)
		{
			mouseLook.OnInteract();

			EventManager.Instance.TriggerEvent(EventType.OnPlayerInteract);
		}
	}

	private void OnSprint(InputValue value)
	{
		isSprinting = value.isPressed;

		states[(int)currentState].OnSprint(isSprinting);
	}

	private void OnInventory(InputValue value)
	{
		if (!menuMode || menuMode && showInventory)
		{
			showInventory = !showInventory;

			MainCanvas.Instance.DisplayPlayerInventory(showInventory);
		}
	}

	private void OnCraft(InputValue value)
	{
		if (!menuMode || menuMode && showCraftBook)
		{
			showCraftBook = !showCraftBook;

			MainCanvas.Instance.DisplayCraftBook(showCraftBook);
		}
	}

	private void OnClose(InputValue value)
	{
		if (menuMode)
		{
			if (showInventory)
			{
				OnInventory(value);
			}
			else if (showCraftBook)
			{
				OnCraft(value);
			}
			else
			{
				MainCanvas.Instance.HideChestInventory();
			}
		}
	}

	#endregion

	/// <summary>
	/// Cast a sphere and check if player is near ground.
	/// </summary>
	private void CheckGround()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		if (isGrounded && currentState == PlayerState.Air)
		{
			SetState(PlayerState.Ground);
		}
		else if (!isGrounded && currentState == PlayerState.Ground)
		{
			SetState(PlayerState.Air);
		}
	}

	private void OnMenuOpen(EventParameters parameters)
	{
		menuMode = true;
	}

	private void OnMenuClose(EventParameters parameters)
	{
		menuMode = false;
	}

	#region Getter

	/// <summary>
	/// Return true if player is near the ground.
	/// </summary>
	public bool IsGrounded()
	{
		return isGrounded;
	}

	/// <summary>
	/// Return the gravity applied on player.
	/// </summary>
	public float GetGravity()
	{
		return gravity;
	}

	/// <summary>
	/// Return the move input read by PlayerInput.
	/// </summary>
	public Vector2 GetMoveInput()
	{
		return moveInput;
	}

	/// <summary>
	/// Return the look input read by PlayerInput.
	/// </summary>
	public Vector2 GetLookInput()
	{
		return lookInput;
	}

	/// <summary>
	/// Return the sprint input read by PlayerInput.
	/// </summary>
	public bool GetSprintInput()
	{
		return isSprinting;
	}

	public Inventory GetInventory()
	{
		return inventory;
	}

	#endregion

	/// <summary>
	/// Set a new state : call OnStateExit on previous state and OnStateEnter on new state.
	/// </summary>
	public void SetState(PlayerState newState)
	{
		states[(int)currentState].OnExitState();

		previousState = currentState;
		currentState = newState;

		states[(int)currentState].OnEnterState();
	}

	public void SetPreviousState()
	{
		states[(int)currentState].OnExitState();

		PlayerState buffer = previousState;
		previousState = currentState;
		currentState = buffer;

		states[(int)currentState].OnEnterState();
	}

	/// <summary>
	/// Add a new item in player's inventory.
	/// </summary>
	public bool AddItem(Item item, int count)
	{
		if (item != null)
		{
			return inventory.AddItem(item, count);
		}

		return false;
	}
}
