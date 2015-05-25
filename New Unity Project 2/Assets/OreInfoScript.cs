using UnityEngine;
using System.Collections;
using System.IO;


public class OreInfoScript : MonoBehaviour {

	public string fileText = null;
	public Ore[] ores = null;
	public string[] oreParts;
	public int totalRarity = 0;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		//Debug.Log ("Reading ore details from file");
		if(File.Exists ("OreInfo.txt"))
		{
			var OreFile = File.OpenText ("OreInfo.txt");
			string fileText = OreFile.ReadToEnd();
			//Debug.Log (fileText);
			OreFile.Close ();
			//Debug.Log ("Done reading from file");
			parseString(fileText);
		}



	}

	public void parseString(string oreDetails)
	{
		var oreArray = oreDetails.Split(':');
		ores = new Ore[System.Convert.ToInt32(oreArray[0])];

		for(int x = 0; x< oreArray.Length-1; x++)
		{

			oreParts = oreArray[x+1].Split (',');
		//	Debug.Log (oreParts[0] + " " + oreParts[1] + " " + oreParts[2]);
			ores[x] = new Ore();//this.gameObject.AddComponent<Ore>();
			ores[x].oreName = oreParts[0].Trim();
			ores[x].baseValue = System.Convert.ToInt32(oreParts[1]);
			ores[x].rarity = System.Convert.ToInt32 (oreParts[2]);
			totalRarity += System.Convert.ToInt32 (oreParts[2]);
			ores[x].oreColor = new Color32(System.Convert.ToByte(oreParts[3]),System.Convert.ToByte(oreParts[4]),System.Convert.ToByte(oreParts[5]),255);


		}
		float tempRarity = 0;
		for(int x = 0; x<ores.Length; x++)
		{
			ores[x].rarity = (int)(tempRarity + ((float)ores[x].rarity/(float)totalRarity)*1000f);
			//Debug.Log (ores[x].oreName);

		}
	}
	// Update is called once per frame
	public Ore fetchOre(int passedValue)
	{
		Ore oreToReturn = null;
		float lastRarity = 0f;
		for(int x = 0; x < ores.Length; x++)
		{

			if(passedValue >= lastRarity && passedValue <= ores[x].rarity + lastRarity)
			{
				oreToReturn = ores[x];
			}
			lastRarity = ores[x].rarity + lastRarity;
		}
		//Debug.Log (passedValue + " " + lastRarity + " " + oreToReturn.oreName);

		return oreToReturn;
	}


	public Ore fetchExplicit(string name)
	{
		Ore oreToReturn = new Ore();//this.gameObject.AddComponent<Ore>();//null;
		for (int x = 0; x < ores.Length; x++)
		{
			if(name == ores[x].oreName)
			{

				oreToReturn = oreToReturn.getOre(ores[x]);
				Debug.Log (oreToReturn.count + " in explicit call");
			}
		}
		return oreToReturn;
	}

	void Update () {
	
	}
}
