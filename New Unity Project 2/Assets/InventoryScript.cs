using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class InventoryScript : MonoBehaviour {
	
	public float weightLimit = 40000;
	public float sizeLimit = 40000;
	public float inventoryWeight;
	public List<Item> items;
	
	
	// Use this for initialization
	void Start () {
	//	Debug.Log ("being called");
		items = new List<Item>();
	}

	public void startUp()
	{
		items = new List<Item>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void addSingleItem(Item singleItem)
	{
//		Debug.Log ("adding " + singleItem.name);
		//	Debug.Log ("adding " + singleItem.name);
		int itemIndex = this.findItem(singleItem);
		if(itemIndex != -1)
		{
			items[itemIndex].count++;
			//			Debug.Log (this.transform.GetComponent<ASRoomObject>().name);
			//			Debug.Log (items[itemIndex].name + " " + items[itemIndex].count);
			recalculateWeightOnce(singleItem.weight);
			//		Debug.Log (items[itemIndex].name + items[itemIndex].count);
		}
		else
		{
			Item addMe = new Item(singleItem);
			//addMe.count = 1;
			//Debug.Log (addMe.count);
			items.Add(singleItem);

			itemIndex = this.findItem (addMe);
			//Debug.Log (items[itemIndex].count);
			items[itemIndex].count = 1;
			recalculateWeightOnce(singleItem.weight);
		}
		
		//items.Add(singleItem);
		//recalculateWeightOnce(singleItem.weight);
	}


	public void removeSingleItem(Item singleItem)
	{
//		Debug.Log ("removing " + singleItem.name);
		int itemIndex = findItem(singleItem);
		if(itemIndex != -1)
		{
		//	Debug.Log ("found and removing");
		//	Debug.Log ("count before removal = " + items[itemIndex].count);
			items[itemIndex].count--;
		//	Debug.Log ("count after removal = " + items[itemIndex].count);
			if(items[itemIndex].count < 1)
			{
				items.RemoveAt(itemIndex);
			}
		}
		else
		{
			Debug.Log ("item cannot be removed, doesn't exist");
		}
		//Debug.Log (items[itemIndex].count);
	}

	public void removeMultipleItems(Item singleItem)
	{

	}



	public void addMultipleItems(Item singleItem, int count)
	{
		for(int i = 0; i<count; i++)
		{
		//	Debug.Log (singleItem.name);
			items.Add(singleItem);
		}
		recalculateWeightMulti(singleItem.weight, count);
	}

	public void recalculateWeightOnce(float itemWeight)
	{
		inventoryWeight += itemWeight;

	}

	public void recalculateWeightMulti(float itemWeight, int count)
	{
		inventoryWeight += itemWeight*count;
	}

	public int findItem(Item item)
	{
		//Debug.Log (items.Count);
		for(int i = 0; i < items.Count; i++)
		{
			if(item.name == items[i].name)
			{
			//	Debug.Log ("found item at " + i);
				return i;
			}

		}
	//	Debug.Log ("not found");
		return -1;
	}

	public void printItems()
	{
		for( int i = 0; i < items.Count; i++)
		{
			Debug.Log ("Name = " + items[i].name + " Count = " + items[i].count);
		}
	}

}





