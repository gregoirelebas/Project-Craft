using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Craft/Recipe", order = 1)]
public class CraftRecipe : ScriptableObject
{
    [System.Serializable]
    public struct RecipeItem
	{
        public Item item;
        public int count;

        public RecipeItem(Item item, int count)
		{
            this.item = item;
            this.count = count;
		}
	}

    public string label = "NewRecipe";
    public Item item = null;
    public Sprite icon = null;
    public int count = 1;
    [Space]
    public List<RecipeItem> parts = null;
}
