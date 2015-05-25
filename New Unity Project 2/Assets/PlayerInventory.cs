using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : InventoryScript
{
	void Start()
	{
		GameObject itemList = GameObject.Find ("ItemList");
		items = new List<Item>();
		foreach(var myVar in GameObject.Find ("Saver").GetComponent<SaveGame>().items)
		{
		//	Debug.Log ("adding " + myVar.name + " " + myVar.count);
			int tempInt = myVar.count;
		//	Debug.Log (tempInt);
			for(int x = 0; x < tempInt; x++)
			{
//				Debug.Log ("adding");
				addSingleItem(myVar);
			}
		}
//		for(int i = 0; i< 100; i++)
//		{
//			int fetchInt = Random.Range(0,1000);
//			addSingleItem (itemList.GetComponent<ItemListInitializer>().fetchItem(fetchInt));
//		}

	}


	public void removeSingle(Item removeMe)
	{
		removeSingleItem(removeMe);
	}

}
