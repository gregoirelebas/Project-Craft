using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
	[SerializeField] private ItemSlot itemSlotPrefab = null;

	[SerializeField] private Transform itemGridContainer = null;

	private Inventory inventory = null;
	private List<ItemSlot> slots = new List<ItemSlot>();

	private void RefreshDisplay()
	{
		if (inventory != null)
		{
			for (int i = 0; i < slots.Count; i++)
			{
				slots[i].SetItem(null);
			}

			for (int i = 0; i < inventory.GetItemCount(); i++)
			{
				slots[i].SetItem(inventory.GetItem(i));
			}
		}
	}

	/// <summary>
	/// Set the inventory to display.
	/// </summary>
	public void SetInventory(Inventory inventory)
	{
		this.inventory = inventory;

		int inventoryCapacity = inventory.GetCapacity();
		int oldCount = slots.Count;

		//If our current inventory is too small, add slots.
		if (slots.Count < inventoryCapacity)
		{
			for (int i = oldCount; i < inventoryCapacity; i++)
			{
				ItemSlot newSlot = Instantiate(itemSlotPrefab, itemGridContainer);
				slots.Add(newSlot);
			}
		}
		//Else if our current inventory is too big, remove slots.
		else if (slots.Count > inventoryCapacity)
		{
			for (int i = inventoryCapacity; i < oldCount; i++)
			{
				Destroy(slots[0].gameObject);
				slots.RemoveAt(0);				
			}
		}

		RefreshDisplay();
	}
}
