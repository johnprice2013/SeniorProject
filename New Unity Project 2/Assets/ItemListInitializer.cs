using UnityEngine;
using System.Collections;
using System.IO;


public class ItemListInitializer : MonoBehaviour {
	
	public string fileText = null;
	public Item[] itemsList = null;
	public string[] itemParts;
	public int totalRarity = 0;
	public GameObject genericItem;
	//for debug purposes

	public InventoryScript myInventory;



	// Use this for initialization
	void Start () {

		//for debug purposes

	//	myInventory = GameObject.Find("Inventory").GetComponent<InventoryScript>();

		//Debug.Log ("Reading ore details from file");
		Debug.Log ("running");
		if(File.Exists ("ItemInfo.txt"))
		{
			var itemFile = File.OpenText ("ItemInfo.txt");
			string fileText = itemFile.ReadToEnd();
			Debug.Log ("file exists");
			//Debug.Log (fileText);
			itemFile.Close ();
			//Debug.Log ("Done reading from file");
			parseString(fileText);
		}
		
		
		
	}
	
	public void parseString(string itemDetails)
	{
		var itemArray = itemDetails.Split(':');
		itemsList = new Item[System.Convert.ToInt32(itemArray[0])];
		
		for(int x = 0; x< itemArray.Length-1; x++)
		{
			
			itemParts = itemArray[x+1].Split (',');
			//	Debug.Log (oreParts[0] + " " + oreParts[1] + " " + oreParts[2]);
			itemsList[x] = new Item();
			itemsList[x].name = itemParts[0].Trim();
			itemsList[x].baseValue = System.Convert.ToInt32(itemParts[1]);
			itemsList[x].rarity = System.Convert.ToInt32 (itemParts[2]);
			totalRarity += System.Convert.ToInt32 (itemParts[2]);
			
	//		myInventory.addSingleItem(items[x]);
	//		myInventory.addMultipleItems(items[x],2);
		}
		float tempRarity = 0;
		for(int x = 0; x<itemsList.Length; x++)
		{
			itemsList[x].rarity = (int)(tempRarity + ((float)itemsList[x].rarity/(float)totalRarity)*1000f);
			//Debug.Log (ores[x].oreName);
			
		}
	}
	// Update is called once per frame
	public Item fetchItem(int passedValue)
	{
		Item itemToReturn = new Item();
		float lastRarity = 0f;
		for(int x = 0; x < itemsList.Length; x++)
		{
			
			if(passedValue >= lastRarity && passedValue <= itemsList[x].rarity + lastRarity)
			{
				itemToReturn = itemsList[x];
			}
			lastRarity = itemsList[x].rarity + lastRarity;
		}
		//Debug.Log (passedValue + " " + lastRarity + " " + oreToReturn.oreName);
		
		return itemToReturn;
	}
	
	
	void Update () {
		
	}
}