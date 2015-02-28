using UnityEngine;
using System.Collections;

public class ASRoomBuilder : MonoBehaviour {


	public GameObject wall;
	public GameObject hallway;
	public GameObject floor;
	public GameObject realWall;
	public GameObject realHallway;
	public GameObject realFloor;
	public int roomID = 0;
	public FurnishingsList furnishList;

	public string roomType = null;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buildSelf()
	{
		if(roomType == "Office")
			furnishList = GameObject.Find("OfficeFurnishings").GetComponent<FurnishingsList>();
		else if(roomType == "Dorm")
			furnishList = GameObject.Find("DormFurnishings").GetComponent<FurnishingsList>();
		else if(roomType == "Infirmary")
			furnishList = GameObject.Find("InfirmaryFurnishings").GetComponent<FurnishingsList>();
		else if(roomType == "MessHall")
			furnishList = GameObject.Find("MessHallFurnishings").GetComponent<FurnishingsList>();

		//Debug.Log ("Building room");
		realFloor = (GameObject)Instantiate(floor,Vector3.zero, Quaternion.identity);
		realWall = (GameObject)Instantiate(wall,Vector3.zero, Quaternion.identity);
		realHallway = (GameObject)Instantiate(hallway,Vector3.zero, Quaternion.identity);

		realFloor.transform.parent = this.transform;
		realWall.transform.parent = this.transform;
		realHallway.transform.parent = this.transform;
		realFloor.transform.localRotation = Quaternion.identity;
		realWall.transform.localRotation = Quaternion.identity;
		realHallway.transform.localRotation = Quaternion.identity;
		realFloor.transform.position = this.transform.position;
		realWall.transform.position = this.transform.position;
		realHallway.transform.position = this.transform.position;



	
		//set

		populateRoom();

	}

	public void populateRoom()
	{


	
	}

}
