﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable, IHoverable
{
	[SerializeField] private Item item = null;
	[SerializeField] private int count = 1;
	[SerializeField] private int resistance = 1;

	private int tapCount = 0;

	/// <summary>
	/// Show a message and trigger event.
	/// </summary>
	public void OnCursorEnter()
	{
		EventParameters parameters = new EventParameters();
		parameters.@string = "Pick up";

		EventManager.Instance.TriggerEvent(EventType.OnCursorEnter, parameters);
	}

	/// <summary>
	/// Clear the displayed message and trigget event.
	/// </summary>
	public void OnCursorExit()
	{
		EventManager.Instance.TriggerEvent(EventType.OnCursorExit);
	}

	/// <summary>
	/// Try to pickup the item. Destroy gameobject afterward.
	/// </summary>
	public void OnInteraction()
	{
		tapCount++;

		if (tapCount >= resistance)
		{
			if (GameManager.Instance.GetPlayer().AddItem(item, count))
			{
				EventManager.Instance.TriggerEvent(EventType.OnItemPickUp, null);

				OnCursorExit();

				Destroy(gameObject);
			}
		}
	}
}
