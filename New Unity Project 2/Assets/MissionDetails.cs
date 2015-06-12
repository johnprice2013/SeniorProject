using UnityEngine;
using System.Collections;

public class MissionDetails : MonoBehaviour {
	public MissionTemplate mission;
	public int missionNumber = 0;
	public bool started = false;
	// Use this for initialization
	void Start () {
		int index = Random.Range(0,3); /// this is for testing;
		//index = 1;
//		Debug.Log(started);

		if(started == false)
		{
//			Debug.Log ("mission not started");
		if(index == 0)
		{
			mission = gameObject.AddComponent<FetchOreMission>();
		}
		else if(index == 1)
		{
			mission = gameObject.AddComponent<FetchItemMission>();

		}
		else if(index == 2)
		{
			mission = gameObject.AddComponent<KillMission>();
		}
		else if(index == 3)
		{
			mission = gameObject.AddComponent<VisitMission>();
		}
		}
		started = true;
//		Debug.Log (mission.missionNumber);
		mission.missionNumber = missionNumber;
		//mission = gameObject.AddComponent<FetchOreMission>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
