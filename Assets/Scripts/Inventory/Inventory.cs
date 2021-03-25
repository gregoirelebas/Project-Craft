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
	/// Create a new slot and an item in it. Return false if failed.
	/// </summary>
	private bool CreateSlot(Item item, int count)
	{
		if (itemSlots.Count < capacity)
		{
			InventorySlot slot = new InventorySlot();

			slot.item = item;
			slot.count = count;

			itemSlots.Add(slot);

			return true;
		}

		return false;
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
	/// Return true if stackable item can be added.
	/// </summary>
	public bool HasFreeSpace(Item item)
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].item == item && itemSlots[i].count < item.stackCount)
			{
				return true;
			}
		}

		return HasFreeSpace();
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
		if (item.isStackable)
		{
			for (int i = 0; i < itemSlots.Count; i++)
			{
				if (itemSlots[i].item == item && itemSlots[i].count < item.stackCount)
				{
					InventorySlot slot = itemSlots[i];
					int totalCount = slot.count + count;

					//If can be added to current stack
					if (totalCount <= item.stackCount)
					{
						slot.count = totalCount;
						return true;
					}
					//If need to create a new slot for leftover
					else if (HasFreeSpace())
					{
						int leftOver = totalCount - item.stackCount;
						return CreateSlot(item, leftOver);
					}

					return false;
				}
			}
		}

		return CreateSlot(item, count);
	}
}
