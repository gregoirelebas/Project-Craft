using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable, IHoverable
{
	[SerializeField] private Item item = null;
	[SerializeField] private int resistance = 1;

	private int tapCount = 0;

	public void OnCursorEnter()
	{
		EventParameters parameters = new EventParameters();
		parameters.@string = "Pick up";

		EventManager.Instance.TriggerEvent(EventManager.EventType.OnCursorEnter, parameters);
	}

	public void OnCursorExit()
	{
		EventParameters parameters = new EventParameters();
		parameters.@string = "";

		EventManager.Instance.TriggerEvent(EventManager.EventType.OnCursorExit, parameters);
	}

	public void OnInteraction()
	{
		tapCount++;

		if (tapCount >= resistance)
		{
			if (GameManager.Instance.GetPlayer().AddItem(item))
			{
				//EventManager.Instance.TriggerEvent(EventManager.EventType.OnItemPickUp, null);

				OnCursorExit();

				Destroy(gameObject);
			}
		}
	}
}
