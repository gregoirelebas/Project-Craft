using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	private List<Item> items = null;
	private int capacity = 0;

	public Inventory(int capacity)
	{
		items = new List<Item>();
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
		return items.Count;
	}

	/// <summary>
	/// Return true if at least one slot is free.
	/// </summary>
	public bool HasFreeSpace()
	{
		return items.Count < capacity;
	}

	/// <summary>
	/// Return the item corresponding to the index.
	/// </summary>
	public Item GetItem(int index)
	{
		if (index >= 0 && index < items.Count)
		{
			return items[index];
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
	public void AddItem(Item newItem)
	{
		items.Add(newItem);
	}
}
