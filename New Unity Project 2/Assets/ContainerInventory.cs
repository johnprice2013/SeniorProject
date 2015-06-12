using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerInventory : InventoryScript {


	public int invID;
	public int numOfItems;
	public GameObject itemList;
	public Item itemToAdd;
	// Use this for initialization
	void Start () {
		//items = new List<Item>();
		startUp();
		invID = this.transform.GetComponent<ASRoomObject>().selfID;
		Random.seed = invID;
		numOfItems = Random.Range (5,7);
		itemList = GameObject.Find ("ItemList");
		populate();

	}



	// Update is called once per frame
	void Update () {
	
	}

	public void populate()
	{
//		Debug.Log ("adding items to container");
		int totalCash = 0;
		for(int i = 0; i < numOfItems; i++)
		{
			itemToAdd = getRandomItem();
			addSingleItem (itemToAdd);
//			Debug.Log (itemToAdd.name + " "  +  invID);
			if(itemToAdd.name == "Old Earth Cash")
			{
				totalCash++;
				i--;
			}
//			Debug.Log ("added");
		}
		//Debug.Log ("Found " + totalCash + " old earth dollars");
	}

	public void removeSingle(Item removeMe)
	{
		removeSingleItem(removeMe);
	}

	public Item getRandomItem()
	{
		//int sizeOfList = itemList.GetComponent<ItemListInitializer>().items.Length;
		int odds = Random.Range (0,1000);
		//Item itemToReturn = new Item();//gameObject.AddComponent<Item>();
		//itemToReturn = itemList.GetComponent<ItemListInitializer>().fetchItem(odds);
		Item itemToReturn = new Item(itemList.GetComponent<ItemListInitializer>().fetchItem(odds));
		return itemToReturn;

	}

	public void printContents()
	{
	

	}


}
