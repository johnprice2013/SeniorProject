using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : InventoryScript
{
	void Start()
	{
		GameObject itemList = GameObject.Find ("ItemList");
		items = new List<Item>();
		for(int i = 0; i< 10; i++)
		{
			int fetchInt = Random.Range(0,1000);
			addSingleItem (itemList.GetComponent<ItemListInitializer>().fetchItem(fetchInt));
		}

	}


	public void removeSingle(Item removeMe)
	{
		removeSingleItem(removeMe);
	}

}
