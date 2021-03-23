using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	public class InventorySlot
	{
		public Item item = null;
		public int count = 0;
	}

	private List<InventorySlot> itemSlots = null;
	private int capacity = 0;

	public Inventory(int capacity)
	{
		itemSlots = new List<InventorySlot>();
		this.capacity = capacity;
	}

	/// <summary>
	/// Return the capacity of this inventory.
	/// </summary>
	public int GetCapacity()
	{
		return capacity;
	}

	/// <summary>
	/// Get the current item count.
	/// </summary>
	public int GetItemCount()
	{
		return itemSlots.Count;
	}

	/// <summary>
	/// Return true if at least one slot is free.
	/// </summary>
	public bool HasFreeSpace()
	{
		return itemSlots.Count < capacity;
	}

	/// <summary>
	/// Return the item corresponding to the index.
	/// </summary>
	public InventorySlot GetInventorySlot(int index)
	{
		if (index >= 0 && index < itemSlots.Count)
		{
			return itemSlots[index];
		}
		else
		{
			Debug.LogError("Index out of range!");
			return null;
		}
	}

	/// <summary>
	/// Add a new item in the inventory.
	/// </summary>
	public bool AddItem(Item item, int count = 1)
	{
		if (itemSlots.Count < capacity)
		{
			InventorySlot slot = new InventorySlot();

			slot.item = item;
			slot.count = count;

			itemSlots.Add(slot);
		}

		return false;
	}
}
