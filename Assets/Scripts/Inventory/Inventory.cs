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
	public bool HasFreeSpace(int count = 0)
	{
		return GetFreeSlot() >= count;
	}

	/// <summary>
	/// Return true if stackable item can be added.
	/// </summary>
	public bool HasFreeSpace(Item item, int count = 1)
	{
		if (item.isStackable)
		{
			int totalCount = 0;
			int totalStack = 0;

			for (int i = 0; i < slots.Count; i++)
			{
				if (slots[i].item == item)
				{
					totalCount += slots[i].count;
					totalStack += item.stackCount;
				}
			}

			if (totalCount == 0)
			{
				return HasFreeSpace();
			}
			else
			{
				return totalCount + count < totalStack;
			}
		}
		else
		{
			return HasFreeSpace();
		}
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

	public int GetItemCount(Item item)
	{
		int total = 0;

		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].item == item)
			{
				total += slots[i].count;
			}
		}

		return total;
	}

	/// <summary>
	/// Add a new item in the inventory.
	/// </summary>
	public bool AddItem(Item item, int count = 1)
	{
		if (item.isStackable && HasFreeSpace(item, count))
		{
			InventorySlot slot;
			for (int i = 0; i < slots.Count; i++)
			{
				slot = slots[i];

				//Find a slot that has some free space
				if (slot.item == item && slot.count < item.stackCount)
				{
					int canAdd = item.stackCount - slot.count;

					//If everything can go inside, we're done
					if (count <= canAdd)
					{
						slot.count += count;
						return true;
					}
					//If not, go to next slot and add what's left
					else
					{
						slot.count = item.stackCount;
						count -= canAdd;
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

	public void RemoveItem(Item item, int count)
	{
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].item == item)
			{
				if (slots[i].count > count)
				{
					slots[i].count -= count;

					return;
				}
				else
				{
					count -= slots[i].count;
					slots[i].item = null;
				}
			}
		}
	}
}
