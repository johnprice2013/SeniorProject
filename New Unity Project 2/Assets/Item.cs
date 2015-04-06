using UnityEngine;
using System.Collections;

public class Item{


	public float weight = 0;
	public float size = 0;
	public string name = null;
	public Sprite image = null;
	public float baseValue = 0;
	public float rarity = 0;
	public int count = 1;



	public Item()
	{

	}

	public Item ItemExplicit(Item passed)
	{
		Item returnMe = new Item();
		returnMe.weight = passed.weight;
		returnMe.size = passed.size;
		returnMe.name = passed.name;
		returnMe.image = passed.image;
		returnMe.baseValue = passed.baseValue;
		returnMe.rarity = passed.baseValue;
		returnMe.count = passed.count;

		return returnMe;
	}

	public Item(Item passed)
	{
		weight = passed.weight;
		size = passed.size;
		name = passed.name;
		image = passed.image;
		baseValue = passed.baseValue;
		rarity = passed.baseValue;
		count = passed.count;
	}
}
