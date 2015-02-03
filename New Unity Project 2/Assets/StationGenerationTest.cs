using UnityEngine;
using System.Collections;


public class StationGenerationTest : MonoBehaviour {
	public GameObject room0;
	public GameObject room1;
	public GameObject room2;
	public GameObject room3;
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
	public int uniqueRooms = 4;
	
	// Use this for initialization
	void Start () {
		rows = 4;
		columns = 4;
		floorHeight = 4;
		numOfFloors = 3;
		createFloor();

		
		
		
		
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
		for(int y = 0; y< numOfFloors*floorHeight;)
		{
			for(int x = 0; x<(int)roomWidth*columns;)
			{
				for(int z = 0; z<(int)roomHeight*rows;)
				{
					int i = Random.Range (0,4);
					Quaternion rot = Quaternion.Euler (new Vector3(0,90*i,0));
					if(y == 0)
					{
						i = Random.Range(0,uniqueRooms-1);
					}
					else
					{
						i = Random.Range (0,uniqueRooms);
					}
					if(i == 0)
					{
						GameObject room = (GameObject) Instantiate(room0, new Vector3(x,0,z),rot);
						room.transform.parent = this.transform;
						room.transform.localPosition = new Vector3(x,y,z);
					}
					else if(i == 1)
					{
						GameObject room = (GameObject) Instantiate(room1, new Vector3(x,0,z),rot);
						
						room.transform.parent = this.transform;
						room.transform.localPosition = new Vector3(x,y,z);
					}
					else if(i == 2)
					{
						GameObject room = (GameObject) Instantiate(room2, new Vector3(x,0,z),rot);
						room.transform.parent = this.transform;
						room.transform.localPosition = new Vector3(x,y,z);
					}
					else
					{
						GameObject room = (GameObject) Instantiate(room3, new Vector3(x,0,z),rot);
						room.transform.parent = this.transform;
						room.transform.localPosition = new Vector3(x,y,z);
					}
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

	// Update is called once per frame
	void FixedUpdate () {

		if(doneUpdating && Input.GetKey (KeyCode.R))
		{
			doneUpdating = false;
			refreshFloor ();
			StartCoroutine(updateAndWait());
		}
	}

	public IEnumerator updateAndWait()
	{
		yield return new WaitForSeconds(.05f);
		doneUpdating = true;
	}
}