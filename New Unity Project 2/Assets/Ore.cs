using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("OreClass")]
public class Ore{// : MonoBehaviour {


	public Color oreColor;
	public int rarity = 0;
	public int baseValue = 0;
	public string oreName;
	public int count;


	//public Ore myOre = new Ore();

	// Use this for initialization
	public Ore()
	{

	}



	public Ore(string passedName, int val, int rare)
	{
		rarity = rare;
		baseValue = val;
		oreName = passedName;
		count = 1;
	
	}

	public Ore getSingleOre(Ore passedOre)
	{	
		Ore oreToReturn = new Ore();//this.gameObject.AddComponent<Ore>();
		oreToReturn.rarity = passedOre.rarity;
		oreToReturn.baseValue = passedOre.baseValue;
		oreToReturn.oreName = passedOre.oreName;
		oreToReturn.count = 1;
		return oreToReturn;

	}
	public Ore getOre(Ore passedOre)
	{
		Ore oreToReturn = new Ore();//this.gameObject.AddComponent<Ore>();
		oreToReturn.rarity = passedOre.rarity;
		oreToReturn.baseValue = passedOre.baseValue;
		oreToReturn.oreName = passedOre.oreName;
		oreToReturn.count = passedOre.count;
		return oreToReturn;
	}

	public void setCount(int passedCount)
	{
		count = passedCount;
	}
	//void Start () {
	
	//}
	
	// Update is called once per frame
	void Update () {
	
	}
}
