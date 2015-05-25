using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SaveGame : MonoBehaviour {

	public GameObject playerShip;
	public Vector3 playerShipLocation;
	public GameObject star;
	public Vector3 shipRotation;
	public GameObject player;
	public Vector3 starMovement;
	public bool saveWait = false;
	public string fileName;
	public bool firstStart;
	public List<MissionTemplate> missions;
	public List<Ore> ores;
	public List<Item> items;
	public List<GameObject> realMissions;
	public GameObject tempmission;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		firstStart = false;
		OreInfoScript info = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>();
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKey(KeyCode.U) && saveWait == false)
		{
			StartCoroutine(FetchAndSaveData());
		}
	if(Input.GetKey (KeyCode.Y) && saveWait == false)
		{
			StartCoroutine(FetchAndReadData());

		}
	}

	public IEnumerator FetchAndSaveData(string passedFile = "SaveGame")
	{
		saveWait = true;
		Debug.Log ("Writing to file");
		if(File.Exists ("SaveGame"))
		   {
			File.Delete ("SaveGame");
			}
		var SaveFile = File.CreateText (fileName);

		SaveDataStorage s = GameObject.Find ("SaveData").GetComponent<SaveDataStorage>();
		s.gatherData();




		SaveFile.WriteLine(s.worldSeed);
		SaveFile.WriteLine(s.sectorX);
		SaveFile.WriteLine(s.sectorY);
		SaveFile.WriteLine(s.sectorZ);
		SaveFile.WriteLine(s.starX);
		SaveFile.WriteLine(s.starY);
		SaveFile.WriteLine(s.starZ);
		SaveFile.WriteLine("Items");
		PlayerInventory playInv = GameObject.Find ("Inventory").GetComponent<PlayerInventory>();
		SaveFile.WriteLine(playInv.items.Count);
		foreach(var item in playInv.items)
		{
			SaveFile.WriteLine(item.name);
			SaveFile.WriteLine (item.count);
		}
		//write in item details
		SaveFile.WriteLine ("?Ores");
		ShipInventoryScript shipInv = GameObject.Find("ShipInventory").GetComponent<ShipInventoryScript>();
		SaveFile.WriteLine (shipInv.ores.Count);
		foreach(var ore in shipInv.ores)
		{
			SaveFile.WriteLine (ore.oreName);
			SaveFile.WriteLine (ore.count);
		}
		//write in ore details
		SaveFile.WriteLine("!Missions");
		GameObject player = GameObject.Find ("Capsule");
		foreach(Transform child in player.transform)
		{
			if(child.name == "Mission(Clone)")
			{
				if(child.GetComponent<MissionDetails>().mission.missionType == "Kill")
				{
					SaveFile.WriteLine ("Kill");
					SaveFile.WriteLine (child.GetComponent<KillMission>().numToKill);
					SaveFile.WriteLine (child.GetComponent<KillMission>().numKilled);
					SaveFile.WriteLine (child.GetComponent<KillMission>().missionFlavorText);
					SaveFile.WriteLine (child.GetComponent<KillMission>().missionInfoText);
				}
				else if (child.GetComponent<MissionDetails>().mission.missionType == "Fetch Ore")
				{
					SaveFile.WriteLine ("Fetch Ore");
					Debug.Log (child.GetComponent<FetchOreMission>().oreType);
					SaveFile.WriteLine (child.GetComponent<FetchOreMission>().oreType.oreName);
					SaveFile.WriteLine (child.GetComponent<FetchOreMission>().amountToGet);
					SaveFile.WriteLine (child.GetComponent<FetchOreMission>().missionFlavorText);
					SaveFile.WriteLine (child.GetComponent<FetchOreMission>().missionInfoText);
				}
				else if(child.GetComponent<MissionDetails>().mission.missionType == "Fetch Item")
				{
					SaveFile.WriteLine ("Fetch Item");
					SaveFile.WriteLine (child.GetComponent<FetchItemMission>().itemType.name);
					SaveFile.WriteLine (child.GetComponent<FetchItemMission>().amountToGet);
					SaveFile.WriteLine (child.GetComponent<FetchItemMission>().missionFlavorText);
					SaveFile.WriteLine (child.GetComponent<FetchItemMission>().missionInfoText);
				}

			}
		}
		//write in mission details

		SaveFile.Close();
		yield return new WaitForSeconds(2f);
		saveWait = false;
		Debug.Log ("Wrote to file");
	}

	public IEnumerator FetchAndReadData(string passedFile = "SaveGame")
	{
		saveWait = true;
		Debug.Log ("Reading from file");
		fileName = passedFile;
		if(File.Exists (fileName))
		{
			var SaveFile = File.OpenText ("SaveGame");
			string fileText = SaveFile.ReadToEnd();
			Debug.Log (fileText);
			SaveFile.Close ();
			Debug.Log ("Read from file");
		}
		yield return new WaitForSeconds(2f);
		saveWait = false;
	}

	public IEnumerator FetchAndLoadData(string passedFile = "SaveGame")
	{
		saveWait = true;
		Debug.Log ("Reading from file");
		Debug.Log (passedFile);
		fileName = passedFile;
		if(File.Exists (fileName))
		{
			var SaveFile = File.OpenText (fileName);
			getSeed(SaveFile);
			getSector(SaveFile);
			getPosition(SaveFile);
			getItems(SaveFile);
			getOres(SaveFile);
			getMissions(SaveFile);
			int quickIndex = 0;
		//	realMissions = new GameObject[4];
			foreach(var mis in realMissions)
			{
				Debug.Log (mis.GetComponent<MissionDetails>().mission.missionInfoText);
			//	realMissions[quickIndex] = (GameObject)Instantiate(tempmission);
			//	realMissions[quickIndex].GetComponent<MissionDetails>().mission = mis;
			//	realMissions[quickIndex].transform.parent = this.transform;
//				if(mis.GetComponent<MissionDetails>().mission.missionType == "Fetch Ore")
//				{
//					Debug.Log (mis.GetComponent<FetchOreMission>().oreType.oreName);
//				}
			//	realMissions[quickIndex].GetComponent<MissionInfo>().accepted = true;
			//	Debug.Log (realMissions[quickIndex].GetComponent<MissionDetails>().mission.missionType);
			//	Debug.Log (quickIndex);
			//	quickIndex++;


			}
			//string fileText = SaveFile.ReadToEnd();
			//Debug.Log (fileText);
			SaveFile.Close ();
			Debug.Log ("Read from file");
			saveWait = false;
			Application.LoadLevel("asdf");
		}
		else
		{

			Debug.Log ("filenotfound");
		}
		yield return new WaitForSeconds(2f);
		saveWait = false;
	}

	public void getSeed(StreamReader myFile)
	{
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().worldSeed = System.Convert.ToInt32(myFile.ReadLine());
	}
	public void getSector(StreamReader myFile)
	{
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().sectorX = System.Convert.ToInt32(myFile.ReadLine());
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().sectorY = System.Convert.ToInt32(myFile.ReadLine());
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().sectorZ = System.Convert.ToInt32(myFile.ReadLine());
	}
	public void getPosition(StreamReader myFile)
	{
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().starX = System.Convert.ToDouble(myFile.ReadLine());
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().starY = System.Convert.ToDouble(myFile.ReadLine());
		GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().starZ = System.Convert.ToDouble(myFile.ReadLine());
	}
	public void getItems(StreamReader myFile)
	{

		if(myFile.Peek() == 'I')
		{
			Debug.Log ("reading items");
			myFile.ReadLine();
			int i = 0;
			if(!myFile.EndOfStream)
			{
			int quickCount = System.Convert.ToInt32(myFile.ReadLine());
			items = new List<Item>();
			while(myFile.Peek() != '?' && myFile.EndOfStream == false)
			{
				string itemName = myFile.ReadLine();
				int itemCount = System.Convert.ToInt32(myFile.ReadLine());
		//		Debug.Log (itemName + " " + itemCount);
				items.Insert(i,GameObject.Find ("ItemList").GetComponent<ItemListInitializer>().fetchExplicit(itemName));
				//items[i] = GameObject.Find ("ItemList").GetComponent<ItemListInitializer>().fetchExplicit(itemName);
			//	Debug.Log (items[i].name);

					Debug.Log (items[i].name);
				
				items[i].setCount(itemCount);
//				Debug.Log (items[i].count);
			}
			}
		}
		//GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().worldSeed = myFile.ReadLine();
	}
	public void getOres(StreamReader myFile)
	{
		OreInfoScript info = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>();
		Debug.Log(info.ores[0].count + " " + 0);
		if(myFile.Peek() == '?')
		{
			Debug.Log ("reading Ores");
			myFile.ReadLine();
			int i = 0;
			if(!myFile.EndOfStream)
			{
				int quickCount = System.Convert.ToInt32(myFile.ReadLine());
				//ores = new List<gameObject.AddComponent<Ore>()>();
				ores = new List<Ore>();
				//Debug.Log (ores[0].count);
				while(myFile.Peek() != '!' && myFile.EndOfStream == false)
				{
					string itemName = myFile.ReadLine();
					int itemCount = System.Convert.ToInt32(myFile.ReadLine());
					//		Debug.Log (itemName + " " + itemCount);
				//	OreInfoScript info = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>();
					//Debug.Log(info.ores[0].count + " " + 0);
					Ore oreToAdd = new Ore();// this.gameObject.AddComponent<Ore>();
					//Debug.Log(info.ores[0].count + " " + 1);
					oreToAdd = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>().fetchExplicit(itemName);
					ores.Insert(i,oreToAdd);
					//items[i] = GameObject.Find ("ItemList").GetComponent<ItemListInitializer>().fetchExplicit(itemName);
					//	Debug.Log (items[i].name);
					
					Debug.Log (ores[i].oreName);
					Debug.Log(info.ores[0].count + " " + 5);

					ores[i].setCount(itemCount);
					Debug.Log(info.ores[0].count + " " + 6);
				

				//				Debug.Log (items[i].count);
			}
			}
		}
		//GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().worldSeed = myFile.ReadLine();
	}
	public void getMissions(StreamReader myFile)
	{
		Debug.Log ("reading Missions");
		myFile.ReadLine();
		int i = 0;
		while(!myFile.EndOfStream)
		{
			string missionType = myFile.ReadLine();
			if(missionType == "Fetch Item")
			{
				Debug.Log ("found fetch item mission");
				string itemName = myFile.ReadLine ();
				int itemCount = System.Convert.ToInt32(myFile.ReadLine());
				string flavor = myFile.ReadLine ();
				string missionInfo = myFile.ReadLine ();

				Item itemToAdd = GameObject.Find ("ItemList").GetComponent<ItemListInitializer>().fetchExplicit(itemName);
				GameObject quickMission = (GameObject)Instantiate(tempmission);
				FetchItemMission fetchMission = gameObject.AddComponent<FetchItemMission>();
				fetchMission.itemType = itemToAdd;
				fetchMission.amountToGet = itemCount;
				fetchMission.missionFlavorText = flavor;
				fetchMission.missionInfoText = missionInfo;
				fetchMission.missionType = "Fetch Item";
				fetchMission.started = true;
				quickMission.GetComponent<MissionDetails>().mission = fetchMission;
				quickMission.GetComponent<MissionDetails>().started = true;

				realMissions.Insert(0,quickMission);
				realMissions[0].transform.parent = this.transform;

				Debug.Log (fetchMission.itemType.name);
			}
			else if(missionType == "Fetch Ore")
			{
				Debug.Log ("found fetch ore mission");
				string itemName = myFile.ReadLine ();
				int itemCount = System.Convert.ToInt32(myFile.ReadLine());
				string flavor = myFile.ReadLine ();
				string missionInfo = myFile.ReadLine ();
				GameObject quickMission = (GameObject)Instantiate(tempmission);
				Ore itemToAdd = new Ore();
				itemToAdd = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>().fetchExplicit(itemName);
				FetchOreMission fetchMission = gameObject.AddComponent<FetchOreMission>();
				//fetchMission.oreType = new Ore();
				//fetchMission.oreType = fetchMission.oreType.getOre(itemToAdd);
				fetchMission.oreType = itemToAdd;
				fetchMission.amountToGet = itemCount;
				fetchMission.missionFlavorText = flavor;
				fetchMission.missionInfoText = missionInfo;
				fetchMission.missionType = "Fetch Ore";
				fetchMission.started = true;
				quickMission.GetComponent<MissionDetails>().mission = fetchMission;
				quickMission.GetComponent<MissionDetails>().started = true;
				realMissions.Insert(0,quickMission);

				realMissions[0].transform.parent = this.transform;
				//missions.Insert(0,fetchMission);
				Debug.Log (fetchMission.oreType.oreName);
				Debug.Log (missions.Count);
			}
			else if(missionType == "Kill")
			{
				Debug.Log ("found kill mission");
				int killAmount = System.Convert.ToInt32(myFile.ReadLine());
				int numKilled = System.Convert.ToInt32(myFile.ReadLine());
				string flavor = myFile.ReadLine ();
				string missionInfo = myFile.ReadLine ();
				KillMission killMission = gameObject.AddComponent<KillMission>();
				killMission.missionType = "Kill";
				killMission.missionFlavorText = flavor;
				killMission.missionInfoText = missionInfo;
				killMission.numKilled = numKilled;
				killMission.numToKill = killAmount;
				missions.Insert(0,killMission);

			}

		}
	}
		//GameObject.Find ("SaveData").GetComponent<SaveDataStorage>().worldSeed = myFile.ReadLine();

}