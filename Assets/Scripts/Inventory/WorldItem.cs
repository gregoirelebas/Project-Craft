using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable, IHoverable
{
	[SerializeField] private Item item = null;

	public void OnCursorEnter()
	{
		Debug.Log("Enter");
	}

	public void OnCursorExit()
	{
		Debug.Log("Exit");
	}

	public void OnInteraction()
	{
		Debug.Log("Picked up :" + item.label);

		Destroy(gameObject);
	}
}
