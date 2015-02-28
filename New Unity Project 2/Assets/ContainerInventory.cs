using UnityEngine;
using System.Collections;

public class ContainerInventory : InventoryScript {


	public int invID;
	public int numOfItems;
	public GameObject itemList;
	public Item itemToAdd;
	// Use this for initialization
	void Start () {
		invID = this.transform.GetComponent<ASRoomObject>().selfID;
		Random.seed = invID;
		numOfItems = Random.Range (0,5);
		itemList = GameObject.Find ("ItemList");
		populate();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void populate()
	{
		int totalCash = 0;
		for(int i = 0; i < numOfItems; i++)
		{
			itemToAdd = getRandomItem();
			items.Add (itemToAdd);
			//Debug.Log (itemToAdd.name + " "  +  invID);
			if(itemToAdd.name == "Old Earth Cash")
			{
				totalCash++;
				i--;
			}
		}
		Debug.Log ("Found " + totalCash + " old earth dollars");
	}

	public Item getRandomItem()
	{
		//int sizeOfList = itemList.GetComponent<ItemListInitializer>().items.Length;
		int odds = Random.Range (0,1000);

		return itemList.GetComponent<ItemListInitializer>().fetchItem(odds);

	}


}
