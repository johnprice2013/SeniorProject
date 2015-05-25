using UnityEngine;
using System.Collections;


public class StationGenerationTest : MonoBehaviour {
	public GameObject[] walls;
	public GameObject[] hallways;
	public GameObject room;
	public GameObject floor;
	public GameObject stationShell;
	public GameObject corner;
	public GameObject wallpiece;
	public GameObject outerHallway;
	public bool doneUpdating = true;
	public int roomWidth = 25;
	public int roomHeight = 25;
	public int rows = 0;
	public int columns = 5;
	public int floorHeight = 4;
	public int numOfFloors = 3;
	public int roomID = 0;
	public string[] roomTypes;

	
	// Use this for initialization
	void Start () {
		rows = 4;
		columns = 4;
		floorHeight = 4;
		numOfFloors = 3;
		roomTypes = new string[4];
		roomTypes[0] = "Infirmary";
		roomTypes[1] = "Dorm";
		roomTypes[2] = "MessHall";
		roomTypes[3] = "Office";
		//StartCoroutine(createFloor2());

		
		
		
		
	}

	public void refreshFloor()
	{
		clearFloor ();
		createFloor ();
	}


	public void clearFloor()
	{
		int deletedRooms = 0;
		foreach(Transform room in this.transform)
		{
			Destroy(room.gameObject);
			deletedRooms++;
		}
		//Debug.Log (deletedRooms);

	}




	public void createFloor()
	{
		GameObject shell = (GameObject) Instantiate(stationShell, new Vector3(0,0,0),Quaternion.Euler (new Vector3(0,0,0)));
		shell.transform.parent = this.transform;
		shell.transform.localPosition = new Vector3(0,0,0);
		roomID = this.transform.parent.GetComponent<StationData>().stationSeed;
		for(int y = 0; y< numOfFloors*floorHeight;)
		{
			for(int x = 0; x<(int)roomWidth*columns;)
			{
				for(int z = 0; z<(int)roomHeight*rows;)
				{
					Random.seed = roomID;
					int i = Random.Range (0,4);
					Quaternion rot = Quaternion.Euler (new Vector3(0,90*i,0));

					int wallNum = Random.Range (0,walls.Length);
					int hallwayNum = Random.Range(0,hallways.Length);

					Debug.Log ("Made it");

						GameObject currentRoom = (GameObject) Instantiate(room, new Vector3(x,0,z),rot);
					Debug.Log ("stillHere");
						currentRoom.transform.parent = this.transform;
						currentRoom.transform.localPosition = new Vector3(x,y,z);
						currentRoom.GetComponent<ASRoomBuilder>().wall = walls[wallNum];
					Debug.Log ("stillHere2");
						currentRoom.GetComponent<ASRoomBuilder>().hallway = hallways[hallwayNum];
					Debug.Log ("StillHere3");
						currentRoom.GetComponent<ASRoomBuilder>().roomID = roomID;
					Debug.Log ("RoomId = " + currentRoom.GetComponent<ASRoomBuilder>().roomID);
					Debug.Log (roomID);
						Random.seed = roomID;
						int index = Random.Range(0,4);
					
						//currentRoom.GetComponent<ASRoomBuilder>().roomType = roomTypes[index];
					//Debug.Log (currentRoom.GetComponent<ASRoomBuilder>().roomType);
						currentRoom.GetComponent<ASRoomBuilder>().buildSelf();
						


					roomID++;
					//Debug.Log ("x = " + x + " z = " + z);
					z += (int)roomHeight;
						
				}	
				x += (int)roomWidth;
			}
			y += floorHeight;
		}

		for(float x = -12.5f; x<roomWidth * columns;)
		{
			for(float y = 0f; y<floorHeight * numOfFloors;)
			{
				for(float z = -12.5f; z< roomHeight * rows;)
				{
					GameObject room = (GameObject) Instantiate(corner, new Vector3(x,0,z),Quaternion.identity);
					room.transform.parent = this.transform;
					room.transform.localPosition = new Vector3(x,y,z);
					z += roomHeight;
				}
				y += floorHeight;
			}
			x += roomWidth;
		}

		for(int y = 0; y<=(numOfFloors-1)*floorHeight;)
		{
			GameObject room = (GameObject) Instantiate(outerHallway, new Vector3(0,y,0),Quaternion.identity);
			room.transform.parent = this.transform;
			room.transform.localPosition = new Vector3(0,y,0);
			//Debug.Log (y);
			y += floorHeight;

		}
	//	this.gameObject.SetActive(false);// = false;
		//Debug.Log ("awesome");
		//Debug.Log (this.transform.FindChild("StationShell(Clone)").transform.position);
		//this.transform.FindChild ("StationShell(Clone)").transform.FindChild("ASDockingPort").gameObject.SetActive(true);
	}


	public IEnumerator createFloor2()
	{
		GameObject shell = (GameObject) Instantiate(stationShell, new Vector3(0,0,0),Quaternion.Euler (new Vector3(0,0,0)));
		shell.transform.parent = this.transform;
		shell.transform.localPosition = new Vector3(0,0,0);
		roomID = this.transform.parent.GetComponent<StationData>().stationSeed;
		for(int y = 0; y< numOfFloors*floorHeight;)
		{
			for(int x = 0; x<(int)roomWidth*columns;)
			{
				for(int z = 0; z<(int)roomHeight*rows;)
				{
					Random.seed = roomID;
					int addedNum = Random.Range(0,200);
					Random.seed = roomID + addedNum;
					int i = Random.Range (0,4);
					Quaternion rot = Quaternion.Euler (new Vector3(0,90*i,0));
					Random.seed = roomID;
					int wallNum = Random.Range (0,walls.Length);
					int hallwayNum = Random.Range(0,hallways.Length);
					
					//Debug.Log ("Made it");
					
					GameObject currentRoom = (GameObject) Instantiate(room, new Vector3(x,0,z),rot);
					currentRoom.transform.parent = this.transform;
					currentRoom.transform.localRotation = rot;
					currentRoom.transform.localPosition = new Vector3(x,y,z);
					if(x == 0 && z == 0 && y != 0)
					{
						currentRoom.GetComponent<ASRoomBuilder>().wall = walls[0];
						currentRoom.GetComponent<ASRoomBuilder>().hallway = hallways[1];
						//Debug.Log ("stairs");

					}
					else
					{
						currentRoom.GetComponent<ASRoomBuilder>().wall = walls[wallNum];
						currentRoom.GetComponent<ASRoomBuilder>().hallway = hallways[0];
						//Debug.Log ("floor");
					}
					currentRoom.GetComponent<ASRoomBuilder>().wall = walls[wallNum];
					//currentRoom.GetComponent<ASRoomBuilder>().hallway = hallways[hallwayNum];
					currentRoom.GetComponent<ASRoomBuilder>().roomID = roomID;
					Random.seed = roomID;
					int index = Random.Range(0,roomTypes.Length);
					currentRoom.GetComponent<ASRoomBuilder>().roomType = roomTypes[index];
					yield return new WaitForSeconds(0);
					currentRoom.GetComponent<ASRoomBuilder>().buildSelf();

					//Debug.Log ("Room Built");
					
					roomID++;
					//Debug.Log ("x = " + x + " z = " + z);
					z += (int)roomHeight;
					yield return new WaitForSeconds(0);
					
				}	
				x += (int)roomWidth;
			}
			y += floorHeight;
		}
		
		for(float x = -12.5f; x<roomWidth * columns;)
		{
			for(float y = 0f; y<floorHeight * numOfFloors;)
			{
				for(float z = -12.5f; z< roomHeight * rows;)
				{
					GameObject room = (GameObject) Instantiate(corner, new Vector3(x,0,z),Quaternion.identity);
					room.transform.parent = this.transform;
					room.transform.localPosition = new Vector3(x,y,z);
					z += roomHeight;
					yield return new WaitForSeconds(0);
				}
				y += floorHeight;
			}
			x += roomWidth;
		}
		
		for(int y = 0; y<=(numOfFloors-1)*floorHeight;)
		{
			GameObject room = (GameObject) Instantiate(outerHallway, new Vector3(0,y,0),Quaternion.identity);
			room.transform.parent = this.transform;
			room.transform.localPosition = new Vector3(0,y,0);
			//Debug.Log (y);
			y += floorHeight;
			yield return new WaitForSeconds(0);
		}
		//	this.gameObject.SetActive(false);// = false;
		//Debug.Log ("awesome");
		//Debug.Log (this.transform.FindChild("StationShell(Clone)").transform.position);
		//this.transform.FindChild ("StationShell(Clone)").transform.FindChild("ASDockingPort").gameObject.SetActive(true);
		yield return new WaitForSeconds(0);
	}





	// Update is called once per frame
	void FixedUpdate () {

		//if(doneUpdating && Input.GetKey (KeyCode.U))
		//{
		//	doneUpdating = false;
		//	refreshFloor ();
		//	StartCoroutine(updateAndWait());
	//	}
	}


	public void generateRoom(int roomID, GameObject room)
	{
		Random.seed = roomID;
		int wallSelection = Random.Range (0,walls.Length);
		int hallwaySelection = Random.Range(0,hallways.Length);

		
	}

	public IEnumerator updateAndWait()
	{
		yield return new WaitForSeconds(.05f);
		doneUpdating = true;
	}
}