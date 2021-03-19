using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemBank", menuName = "Inventory/Bank", order = 1)]
public class ItemBank : ScriptableObject
{
	[SerializeField] private List<Item> items = null;

	public Item GetItemByLabel(string label)
	{
		Item toReturn = items.Find(x => x.label.Equals(label));

		if (toReturn == null)
		{
			Debug.LogWarning("Couldn't find item with label : " + label + ", return null");
		}

		return toReturn;
	}

	public List<Item> GetRandomItems(int count)
	{
		List<Item> randomItems = new List<Item>();

		for (int i = 0; i < count; i++)
		{
			int index = Random.Range(0, items.Count);
			randomItems.Add(items[index]);
		}

		return randomItems;
	}
}
