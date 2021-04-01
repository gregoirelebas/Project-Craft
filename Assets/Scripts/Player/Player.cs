using System.Collections;
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
	[SerializeField] private ItemBank itemBank = null;

	private CharacterController controller = null;
	private Vector3 movement = Vector3.zero;

	//Inventory
	private Inventory inventory = null;
	private bool showInventory = false;

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

	private bool menuMode = false;
	private bool isGrounded = false;

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

			controller.Move(movement * Time.deltaTime);
		}

		if (Keyboard.current.yKey.wasPressedThisFrame)
		{
			MainCanvas.Instance.HideChestInventory();
			EventManager.Instance.TriggerEvent(EventType.OnMenuClosed);
		}

		if (Keyboard.current.uKey.wasPressedThisFrame)
		{
			if (inventory.AddItem(itemBank.GetItemByLabel("Wood"), 7))
			{
				Debug.Log("Added wood!");
			}
			else
			{
				Debug.Log("Failed to add wood!");
			}
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
		if (value.isPressed)
		{
			states[(int)currentState].OnJump();

			//EventManager.Instance.TriggerEvent(EventType.OnPlayerJump);
		}
	}

	private void OnInteract(InputValue value)
	{
		if (value.isPressed)
		{
			mouseLook.OnInteract();

			//EventManager.Instance.TriggerEvent(EventType.OnPlayerInteract);
		}
	}

	private void OnInventory(InputValue value)
	{
		if (value.isPressed)
		{
			showInventory = !showInventory;

			MainCanvas.Instance.DisplayPlayerInventory(showInventory);

			if (showInventory)
			{
				EventManager.Instance.TriggerEvent(EventType.OnMenuOpened);
			}
			else
			{
				EventManager.Instance.TriggerEvent(EventType.OnMenuClosed);
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
	public bool AddItem(Item item)
	{
		if (item != null && inventory.HasFreeSpace())
		{
			//If item can be stack, check all slots to add it.
			if (item.isStackable && inventory.HasFreeSpace(item))
			{
				inventory.AddItem(item);
			}
			//If item take a full slot, check only if inventory has room for it.
			else if (inventory.HasFreeSpace())
			{
				inventory.AddItem(item);
			}

			return true;
		}

		return false;
	}
}
