using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour {
	
	public float weightLimit = 40000;
	public float sizeLimit = 40000;
	public float inventoryWeight;
	public List<Item> items;
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void addSingleItem(Item singleItem)
	{
		Debug.Log (singleItem.name);
		items.Add(singleItem);
		recalculateWeightOnce(singleItem.weight);
	}



	public void addMultipleItems(Item singleItem, int count)
	{
		for(int i = 0; i<count; i++)
		{
			Debug.Log (singleItem.name);
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

}