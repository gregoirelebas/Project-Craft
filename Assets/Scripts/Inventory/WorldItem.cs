using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable, IHoverable
{
	[SerializeField] private Item item = null;

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
		Debug.Log("Picked up :" + item.label);

		EventParameters parameters = new EventParameters();
		parameters.@item = item;

		EventManager.Instance.TriggerEvent(EventManager.EventType.OnItemPickUp, parameters);

		OnCursorExit();

		Destroy(gameObject);
	}
}
