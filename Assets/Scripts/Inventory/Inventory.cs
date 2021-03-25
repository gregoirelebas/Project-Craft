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

	private List<InventorySlot> slots = null;
	private int capacity = 0;

	public Inventory(int capacity)
	{
		slots = new List<InventorySlot>();
		this.capacity = capacity;

		for (int i = 0; i < capacity; i++)
		{
			slots.Add(new InventorySlot());
		}
	}

	private int GetFreeSlot()
	{
		for (int i = 0; i < capacity; i++)
		{
			if (slots[i].item == null)
			{
				return i;
			}
		}

		return -1;
	}

	private bool AddItemFreeSlot(Item item, int count)
	{
		int index = GetFreeSlot();
		if (index >= 0)
		{
			slots[index].item = item;
			slots[index].count = count;

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
		return slots.Count;
	}

	/// <summary>
	/// Return true if at least one slot is free.
	/// </summary>
	public bool HasFreeSpace()
	{
		return GetFreeSlot() >= 0;
	}

	/// <summary>
	/// Return true if stackable item can be added.
	/// </summary>
	public bool HasFreeSpace(Item item)
	{
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].item == item && slots[i].count < item.stackCount)
			{
				return true;
			}
		}

		return GetFreeSlot() >= 0;
	}

	/// <summary>
	/// Return the item corresponding to the index.
	/// </summary>
	public InventorySlot GetInventorySlot(int index)
	{
		if (index >= 0 && index < slots.Count)
		{
			return slots[index];
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
			for (int i = 0; i < slots.Count; i++)
			{
				if (slots[i].item == item && slots[i].count < item.stackCount)
				{
					InventorySlot slot = slots[i];
					int totalCount = slot.count + count;

					//If can be added to current stack
					if (totalCount <= item.stackCount)
					{
						slot.count = totalCount;
						return true;
					}
					//If need to create a new slot for leftover
					else
					{
						int leftOver = totalCount - item.stackCount;
						if (AddItemFreeSlot(item, leftOver))
						{
							slot.count = item.stackCount;
							return true;
						}
					}
				}
			}
		}

		return AddItemFreeSlot(item, count);
	}

	public void UpdateInventory(int index, Item item, int count)
	{
		slots[index].item = item;
		slots[index].count = count;
	}
}
