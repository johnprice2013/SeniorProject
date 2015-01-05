using UnityEngine;
using System.Collections;

public class StationData : MonoBehaviour {
	public int stationSeed = 0;
	public GameObject stationPlanet;
	public Vector3 stationSector = Vector3.zero;
	public GameObject[] currentMissions;
	public MissionGenerator missionGen;
	public GameObject mission;
	public GameObject enemyArea;
	public bool initiateEnemyStrike = true;
	public GameObject enemyAreaInstance;
	public bool notAbandoned = true;
	// Use this for initialization
	void Start () {
		if(notAbandoned)
		{
		for(int x = 0; x < 4; x++)
		{
			GameObject newObject = (GameObject) Instantiate (mission);
			newObject.GetComponent<MissionInfo>().missionStation = (GameObject)this.gameObject;
			newObject.GetComponent<MissionInfo>().missionNumber = x;
			newObject.transform.parent = this.gameObject.transform;


		}
		}
	}

	public void createEnemyStrike()
	{		
			
			enemyAreaInstance = (GameObject)Instantiate(enemyArea);
			enemyAreaInstance.transform.parent = this.gameObject.transform;
			




	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnDestroy()
	{

	}
}
