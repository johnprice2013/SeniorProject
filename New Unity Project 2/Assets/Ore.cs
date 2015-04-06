using UnityEngine;
using System.Collections;

public class Ore : MonoBehaviour {


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



	//void Start () {
	
	//}
	
	// Update is called once per frame
	void Update () {
	
	}
}
