using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] private Item[] items = new Item[3];

	[SerializeField] private InventoryDisplay inventoryDisplay = null;

	private Inventory inventory = null;
	private bool showInventory = false;

	private void Awake()
	{
		inventory = new Inventory(10);

		inventory.AddItem(items[0]);
		inventory.AddItem(items[1]);
		inventory.AddItem(items[2]);
		inventory.AddItem(items[1]);
		inventory.AddItem(items[1]);
		inventory.AddItem(items[2]);
		inventory.AddItem(items[0]);
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
}
