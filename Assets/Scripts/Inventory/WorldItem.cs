using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable, IHoverable
{
	public enum ItemType
	{
		Stone,
		Wood,
		Weed
	}

	[SerializeField] private ItemType type = ItemType.Stone;

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
		Debug.Log("Picked up :" + type.ToString());

		Destroy(gameObject);
	}
}
