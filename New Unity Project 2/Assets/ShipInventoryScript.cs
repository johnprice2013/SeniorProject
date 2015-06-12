using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipInventoryScript : MonoBehaviour {

	public float weightLimit = 40000;
	public float sizeLimit = 40000;
	public List<Ore> ores;


	// Use this for initialization
	void Start () {
		ores = new List<Ore>();
		if(GameObject.Find ("Saver").GetComponent<SaveGame>().ores != null)
		{
		foreach (var myVar in GameObject.Find ("Saver").GetComponent<SaveGame>().ores)
		{
			int tempInt = myVar.count;
			Debug.Log ("tempInt = " + tempInt);
			for(int x = 0; x < tempInt; x++)
			{
				Ore oreToAdd = new Ore();
				oreToAdd = oreToAdd.getSingleOre(myVar);
				addSingleItem(oreToAdd);
			}
		}
		}
		foreach(var myVar in ores)
		{
			Debug.Log (myVar.oreName + " " + myVar.count + " " + "in inventory");
		}
//		for(int i = 0; i < 100; i++)
//		{
//			int fetchInt = Random.Range(0,1000);
//			addSingleItem (GameObject.Find ("OreInfo").GetComponent<OreInfoScript>().fetchOre(fetchInt));
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void addSingleItem(Ore singleItem)
	{
//		Debug.Log (singleItem.oreName);
		if(ores.Count > 0)
		{
			bool oreFound = false;
			foreach(var ore in ores)
			{
				if(ore.oreName == singleItem.oreName)
				{
//					Debug.Log ("found ore, incrementing");
					ore.count++;
//					Debug.Log (ore.count);
					oreFound = true;
				}

			}
			if(oreFound == false)
			{
				ores.Add(singleItem);
			}

		}
		else
		{
			ores.Add (singleItem);
		}
	}


	public void removeSingleItem(Ore singleItem)
	{
		bool foundOre = false;
		int itemIndex = findOre(singleItem);
		if(itemIndex != -1)
		{
			//	Debug.Log ("found and removing");
			//	Debug.Log ("count before removal = " + items[itemIndex].count);
			ores[itemIndex].count--;
			//	Debug.Log ("count after removal = " + items[itemIndex].count);
			if(ores[itemIndex].count < 1)
			{
				ores.RemoveAt(itemIndex);
			}
		}
		else
		{
			Debug.Log ("ore not found in inventory");
		}

	}

	public int findOre(Ore ore)
	{
		//Debug.Log (items.Count);
		for(int i = 0; i < ores.Count; i++)
		{
			if(ore.oreName == ores[i].oreName)
			{
				//	Debug.Log ("found item at " + i);
				return i;
			}
			
		}
		//	Debug.Log ("not found");
		return -1;
	}

	public void addMultipleItems(GameObject singleItem, int count)
	{

	}
}
