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

	public int GetCapacity()
	{
		return capacity;
	}

	public int GetItemCount()
	{
		return items.Count;
	}

	public bool HasFreeSpace()
	{
		return items.Count < capacity;
	}

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

	public void AddItem(Item newItem)
	{
		items.Add(newItem);
	}
}
