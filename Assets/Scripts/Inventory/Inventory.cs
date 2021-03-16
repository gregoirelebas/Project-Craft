using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	private List<Item> items = null;

	public Inventory()
	{
		items = new List<Item>();
	}

	public void AddItem(Item newItem)
	{
		items.Add(newItem);
	}
}
