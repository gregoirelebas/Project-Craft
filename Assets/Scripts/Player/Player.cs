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

		inventory.AddItem(itemBank.GetItemByLabel("Shovel"));
		inventory.AddItem(itemBank.GetItemByLabel("Stone"));
		inventory.AddItem(itemBank.GetItemByLabel("Wood"));
		inventory.AddItem(itemBank.GetItemByLabel("Wood"));
		inventory.AddItem(itemBank.GetItemByLabel("Stone"));
		inventory.AddItem(itemBank.GetItemByLabel("Shovel"));
		inventory.AddItem(itemBank.GetItemByLabel("Wood"));
		inventory.AddItem(itemBank.GetItemByLabel("Stone"));
	}

	private void OnEnable()
	{
		EventManager.Instance.StartListening(EventManager.EventType.OnItemPickUp, AddItemOnPickUp);
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
			}
		}
	}

	private void AddItemOnPickUp(EventParameters parameters)
	{
		Item item = parameters.item;
		if (item != null)
		{
			inventory.AddItem(item);
		}
	}
}
