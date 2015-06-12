using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {
	
	public bool firstStart = true;
	public int seed = 100;
	public Vector3 startingPosition = new Vector3(1000f,1000f,1000f);
	public Vector3 currentSector = new Vector3(5000f, 5000f, 5000f);
	public int sectorX;
	public int sectorY;
	public int sectorZ;
	public GameObject secCenter;
	public SectorGenerator secGen;

	public GameObject[] missions;
	public int currency = 0;

	// Use this for initialization
	void Start () {
		sectorX = 1;
		sectorY = 1;
		sectorZ = 1;
		missions = new GameObject[10];
		if(GameObject.Find ("SaveData") != null)
		{
			SaveDataStorage s = GameObject.Find ("SaveData").GetComponent<SaveDataStorage>();
			currency = GameObject.Find ("Saver").GetComponent<SaveGame>().currency;
			currentSector.x = s.sectorX;
			currentSector.y = s.sectorY;
			currentSector.z = s.sectorZ;
			seed = s.worldSeed;
			int i = 0;
			foreach( var mission in GameObject.Find ("Saver").GetComponent<SaveGame>().realMissions)
			{
//				Debug.Log (mission.GetComponent<MissionDetails>().mission.missionInfoText);
				mission.transform.parent = this.transform;
				mission.GetComponent<MissionDetails>().mission.setPayout();
				missions[i] = mission.gameObject;
				i++;
				//Debug.Log (mission.gameObject.GetComponent<MissionTemplate>().missionType);
			}

		}
		if(firstStart)
		{
			//current sector is where the game is getting sector data.  use it to go where you need to go.

			sectorX = (int)currentSector.x;
			sectorY = (int)currentSector.y;
			sectorZ = (int)currentSector.z;

		}
		secCenter = GameObject.FindGameObjectWithTag("sectorCenter");
		secGen = secCenter.GetComponent<SectorGenerator>();
		missions = new GameObject[10];
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		sectorX = secGen.sectorX;
		sectorY = secGen.sectorY;
		sectorZ = secGen.sectorZ;
	}

	public void updateCurrency(int currencyChange)
	{
		currency += currencyChange;
	}
}
