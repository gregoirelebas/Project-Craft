using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] private ItemBank itemBank = null;

	[SerializeField] private InventoryDisplay inventoryDisplay = null;

	private Inventory inventory = null;
	private bool showInventory = false;

	private void Awake()
	{
		inventory = new Inventory(10);

		//DEBUG
		List<Item> randomItems = itemBank.GetRandomItems(5);
		for (int i = 0; i < randomItems.Count; i++)
		{
			inventory.AddItem(randomItems[i]);
		}
	}

	private void Update()
	{
		if (Keyboard.current.iKey.wasPressedThisFrame)
		{
			showInventory = !showInventory;

			inventoryDisplay.gameObject.SetActive(showInventory);

			if (showInventory)
			{
				inventoryDisplay.SetInventory(inventory);

				EventManager.Instance.TriggerEvent(EventManager.EventType.OnMenuOpened);
			}
			else
			{
				EventManager.Instance.TriggerEvent(EventManager.EventType.OnMenuClosed);
			}
		}
	}

	public bool AddItem(Item item)
	{
		if (item != null && inventory.HasFreeSpace())
		{
			inventory.AddItem(item);

			return true;
		}

		return false;
	}
}
