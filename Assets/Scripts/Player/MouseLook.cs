using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseLook : PlayerBaseState
{
	[SerializeField] private Transform playerCamera = null;
	[SerializeField] private float mouseSensivity = 100.0f;
	[SerializeField] private float minXRotation = -90.0f;
	[SerializeField] private float maxXRotation = 90.0f;
	[SerializeField] private float interactionDistance = 2.0f;

	private Vector2 lookMovement = Vector2.zero;
	private float xRotation = 0.0f;

	private GameObject cursorHover = null;

	public override void OnUpdate(ref Vector3 movement)
	{
		lookMovement = player.GetLookInput() * mouseSensivity * Time.deltaTime;

		xRotation -= lookMovement.y;
		xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

		player.transform.Rotate(Vector3.up * lookMovement.x);
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
		if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, interactionDistance))
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

	public void OnInteraction()
	{
		if (cursorHover != null)
		{
			IInteractable toInteract = cursorHover.GetComponent<IInteractable>();
			if (toInteract != null)
			{
				toInteract.OnInteraction();
			}
		}
	}
}
