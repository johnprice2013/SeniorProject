using UnityEngine;
using System.Collections;

public class PlayerInventory : InventoryScript
{
	void Start()
	{
		GameObject itemList = GameObject.Find ("ItemList");

		for(int i = 0; i< 10; i++)
		{
			int fetchInt = Random.Range(0,1000);
			items.Add (itemList.GetComponent<ItemListInitializer>().fetchItem(fetchInt));
		}

	}

}
