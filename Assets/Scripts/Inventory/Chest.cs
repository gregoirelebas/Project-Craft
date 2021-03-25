using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable, IHoverable
{
	private Inventory inventory = null;

	private void Awake()
	{
		inventory = new Inventory(20);
	}

	public void OnInteraction()
	{
		if (!GameManager.Instance.IsMenuMode() && GameManager.Instance.GetPlayer().IsGrounded())
		{
			MainCanvas.Instance.DisplayChestInventory(inventory);

			EventManager.Instance.TriggerEvent(EventType.OnMenuOpened);
		}
	}

	public void OnCursorEnter()
	{
		EventParameters parameters = new EventParameters();
		parameters.@string = "Open chest";

		EventManager.Instance.TriggerEvent(EventType.OnCursorEnter, parameters);
	}

	public void OnCursorExit()
	{
		EventManager.Instance.TriggerEvent(EventType.OnCursorExit, null);
	}
}
