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
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void addSingleItem(Ore singleItem)
	{
		Debug.Log (singleItem.oreName);
		if(ores.Count > 0)
		{
			bool oreFound = false;
			foreach(var ore in ores)
			{
				if(ore.oreName == singleItem.oreName)
				{
					Debug.Log ("found ore, incrementing");
					ore.count++;
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
		if(ores.Count > 0)
		{
			foreach(var ore in ores)
			{
				if(ore.oreName == singleItem.oreName)
				{
					ore.count--;
					foundOre = true;
				}
	
			}
		}
		if(foundOre == false)
		{
			Debug.Log ("cannot remove, not here");
		}

	}

	public void addMultipleItems(GameObject singleItem, int count)
	{

	}
}
