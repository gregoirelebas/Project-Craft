using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
	[SerializeField] private Transform playerCamera = null;
	[SerializeField] private float mouseSensivity = 100.0f;

	[SerializeField] private LayerMask pickUpMask = 0;

	private Vector2 mouseInput = Vector2.zero;
	private float xRotation = 0.0f;

	private GameObject cursorHover = null;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		mouseInput *= Time.deltaTime * mouseSensivity;

		xRotation -= mouseInput.y;
		xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

		transform.Rotate(Vector3.up * mouseInput.x);
		playerCamera.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

		CastRayFromCursor();
	}

	private void OnHoverEnter(GameObject newHover)
	{
		cursorHover = newHover;

		IHoverable hover = cursorHover.GetComponent<IHoverable>();
		if (hover != null)
		{
			hover.OnCursorEnter();
		}
	}

	private void OnHoverExit()
	{
		IHoverable hover = cursorHover.GetComponent<IHoverable>();
		if (hover != null)
		{
			hover.OnCursorExit();
		}

		cursorHover = null;
	}

	private void CastRayFromCursor()
	{
		if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, 10.0f, pickUpMask))
		{
			if (cursorHover != null && !hit.transform.gameObject.Equals(cursorHover))
			{
				OnHoverExit();

				OnHoverEnter(hit.transform.gameObject);
			}
			else if (cursorHover == null)
			{
				OnHoverEnter(hit.transform.gameObject);
			}
		}
		else
		{
			if (cursorHover != null)
			{
				OnHoverExit();
			}
		}
	}

	#region Input

	private void OnLook(InputValue inputValue)
	{
		mouseInput = inputValue.Get<Vector2>();
	}

	private void OnInteract(InputValue inputValue)
	{
		if (inputValue.isPressed && cursorHover != null)
		{
			IInteractable toInteract = cursorHover.GetComponent<IInteractable>();
			if (toInteract != null)
			{
				toInteract.OnInteraction();
			}
		}
	}

	#endregion
}
