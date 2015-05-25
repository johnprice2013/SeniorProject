using UnityEngine;
using System.Collections;

public class MissionInfo : MonoBehaviour {
	
	public string missionGiver;
	public string missionDetails;
	public int missionTypeNumber = 0;
	public string missionType;
	public float missionTimer;
	public float missionStartTime;
	public float missionTimeLimit;
	public GameObject missionStation;
	public int missionNumber = 0;
	public int missionTimeFrame = 0;
	public string itemToFetch;
	public int numberOfKills;
	public bool accepted = false;
	public bool finished = false;
	public bool cancelled = false;
	public bool initiated = false;
	public int missionID = 0;
	public bool moved = false;
	public string missionText = null;
	public GameObject missionLocation;
	public bool isInLocalArea = false;
	public bool flaggedAsLocal = false;
	public int numToPick;
	public int xNum;
	public int yNum;
	public int zNum;
	public int xNeg;
	public int yNeg;
	public int zNeg;
	public int tempMissionStationNumber = 0;
	public bool tempNumberAssigned = false;
	public GameObject ghostMissionLocation;
	public int payOut = 0;
	
	
	//Turn me into a factory for fucks sake!!!
	//seriously
	//turn
	//me
	//into
	//a
	//factory!!!!
	
	// Use this for initialization
	void Start () {

		generateMissionDetails();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void FixedUpdate()
	{
		if(accepted == true && moved == false)
		{
			//set parent of mission to the player, or could do this in the acceptance phase.
			moved = true;

		}
		if(missionType == "Kill" && accepted == true && initiated == false)
		{
			//launch function to initiate strike.
			findStationInSector(xNum,yNum,zNum);


			//missionStation.GetComponent<StationData>().createEnemyStrike();


			missionLocation = missionStation;
			initiated = true;

			
		}

		if(missionType == "Kill" && accepted == true && initiated == true && isInLocalArea == true)
		{
		
			if(missionStation.GetComponent<StationData>().enemyAreaInstance == null)
			{
				Debug.Log ("why is this null???");
				finished = true;
			}
		}
		if(finished == true)
		{
			Debug.Log ("destroying mission and paying out");
			GameObject player = GameObject.FindGameObjectWithTag("PlayerBody");
			player.GetComponent<Initialize>().updateCurrency(payOut);
			Destroy(this.gameObject);
		}



	

	}


	public void findStationInSector(int xCoord, int yCoord, int zCoord)
	{
		xCoord += (int)missionStation.GetComponent<StationData>().stationSector.x;
		yCoord += (int)missionStation.GetComponent<StationData>().stationSector.y;
		zCoord += (int)missionStation.GetComponent<StationData>().stationSector.z;

		while(tempNumberAssigned == false)
		{
			Debug.Log ("finding station at coords " + xCoord + "," + yCoord + "," + zCoord);
			foreach(Transform sector in GameObject.Find("SectorData").transform)
			{

				if(sector.GetComponent<InitializeGhostSector>().sectorX == xCoord)
				{
					if(sector.GetComponent<InitializeGhostSector>().sectorY == yCoord)
					{
						if(sector.GetComponent<InitializeGhostSector>().sectorZ == zCoord)
						{
							Debug.Log ("found the right station at " + xCoord + "," + yCoord + "," + zCoord);

								if(sector.childCount != 0)
								{
								GameObject ghostStar = sector.GetChild (0).gameObject;
								GameObject planet = ghostStar.transform.GetChild (Random.Range(0,ghostStar.transform.childCount-1)).gameObject;
								ghostMissionLocation = planet.transform.GetChild(0).gameObject;
								tempMissionStationNumber = planet.transform.GetChild(0).gameObject.GetComponent<InitializeGhostStation>().stationSeed;
								tempNumberAssigned = true;
								checkIfLocal();
								}
						}
					}
				}
			}
			if(xCoord < (int)missionStation.GetComponent<StationData>().stationSector.x)
			{
				xCoord++;
			}
			else
			{
				xCoord--;
			}
			if(yCoord < (int)missionStation.GetComponent<StationData>().stationSector.y)
			{
				yCoord++;
			}
			else
			{
				yCoord--;
			}
			if(zCoord < (int)missionStation.GetComponent<StationData>().stationSector.z)
			{
				zCoord++;
			}
			else
			{
				zCoord--;
			}
		}
	}

	public void generateMissionDetails()
	{


		missionTimeFrame = (((int)Time.time/1000) * 1000);
		missionID = missionStation.GetComponent<StationData>().stationSeed * 10 + missionNumber + missionTimeFrame * 100;
		Random.seed = missionID;
		missionStation = this.gameObject.transform.parent.gameObject;
		missionTypeNumber = Random.Range (0,3);
		if(missionTypeNumber == 0)
		{
			missionType = "Fetch";
			missionText = "I am a Fetch mission.  When implemented, I will tell you to find me some Items in exchange for some cash.";
			payOut = 100;
			//set up details of fetch missions such as which item and how many and where to get them.
		}
		else if(missionTypeNumber == 1)
		{
			missionType = "Visit";
			missionText = "I am a Visit mission.  When implemented, I will tell you to go scout an area and return for some cash.";//find me some Items in exchange for some cash";
			payOut = 100;
			//set up details of visit missions such as where to visit and how close to get/requirements.
		}
		else if(missionTypeNumber == 2)
		{
			missionType = "Kill";
			missionText = "I am a Kill mission.  When implemented, I will tell you to go to a location and wipe out a group of enemies and return for some cash.";//find me some Items in exchange for some cash";
			payOut = 100;
			//set up details of kill missions such as where the targets are and how many to kill.
		}

	


	}

	public void checkIfLocal()
	{
		Debug.Log ("searching locally for station with ID of " + tempMissionStationNumber);
		foreach(GameObject station in GameObject.FindGameObjectsWithTag("station"))
		{
			if(station.GetComponent<StationData>().stationSeed == tempMissionStationNumber)
			{
				isInLocalArea = true;
				flaggedAsLocal = true;
				missionStation = station;
				//this.transform.parent = station.transform;
				Debug.Log ("creating enemy strike");
				missionStation.GetComponent<StationData>().createEnemyStrike();

			}

		}
		if(flaggedAsLocal == false)
		{
			isInLocalArea = false;
			//this.transform.parent = ghostMissionLocation.transform;
			Debug.Log ("changed mission parent to " + ghostMissionLocation.GetComponent<InitializeGhostStation>().stationSeed);
		}
		if(flaggedAsLocal == false)
		{
		StartCoroutine (doubleCheckIfLocal());
		}
		flaggedAsLocal = false;
	}

	public IEnumerator doubleCheckIfLocal()
	{
		yield return new WaitForSeconds(.5f);
		Debug.Log ("searching locally for station with ID of " + tempMissionStationNumber);
		foreach(GameObject station in GameObject.FindGameObjectsWithTag("station"))
		{
			if(station.GetComponent<StationData>().stationSeed == tempMissionStationNumber)
			{
				isInLocalArea = true;
				flaggedAsLocal = true;
				missionStation = station;
				//this.transform.parent = station.transform;
				Debug.Log ("creating enemy strike");
				missionStation.GetComponent<StationData>().createEnemyStrike();
				
			}
			
		}
		if(flaggedAsLocal == false)
		{
			isInLocalArea = false;
			//this.transform.parent = ghostMissionLocation.transform;
			Debug.Log ("changed mission parent to " + ghostMissionLocation.GetComponent<InitializeGhostStation>().stationSeed);
		}
		flaggedAsLocal = false;

	}
}
