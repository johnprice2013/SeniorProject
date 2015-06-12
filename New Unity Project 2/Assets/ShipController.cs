using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public bool inControl = false;
	public Camera[] cameras;
	public bool camerasActive = false;
	public MouseLook mLook;
	public bool pilotSeatLook = false;
	public bool canSwitchViews = true;
	public bool inEnemyRange = false;
	public GameObject star;
	public GameObject enemyArea;
	public bool beingTracked;
	public bool docking = false;
	public GameObject dockingObject;
	public Vector3 distanceToDock;
	public GameObject shipDockPoint;
	public bool docked = false;
	public bool linedUp = false;
	public float rotAccel = 0f;
	// Use this for initialization
	void Start () {
		cameras = GetComponentsInChildren<Camera>();
		mLook = GetComponent<MouseLook>();
		GameObject.Find ("MainShip").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
		shipDockPoint = this.transform.FindChild("ShipDockPort").FindChild("ShipDockPoint").gameObject;
		rotAccel = 20f;
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		star = GetComponent<SwitchSector>().star;
	if(inEnemyRange)
		{

				//When in enemy range, make enemy area a child of the ship, that way
			//the enemies will keep pace with player.  This data can be sent to servers as well.
		}
		distanceToDock = getDistanceToDock();

		if(docking)
		{
			Vector3 quickV = this.transform.localEulerAngles;
			//this.GetComponent<MouseLook>().enabled = false;
//			Debug.Log ("making it here");
			if((quickV.y < 270.5 && quickV.y > 269.5) && (quickV.x < .5 || quickV.x > 359.5) && (quickV.z < .5 || quickV.z > 359.5))
			{
				//this.transform.localRotation = dockingObject.transform.localRotation;
				this.transform.localEulerAngles = new Vector3(0f,270f,0f);
				Debug.Log ("lined up");
				linedUp = true;
				//Debug.Log (this.transform.localRotation);
				//Debug.Log (dockingObject.transform.localRotation);
				//Debug.Log ("lined up");
			}
		
			else
			{
				Vector3 myV = this.transform.localEulerAngles;
				if(myV.x >= 180)
				{
					myV.x = ((360f - myV.x) * rotAccel);
					if(myV.x > 359.5f || myV.x < .5f)
					{
						myV.x = 0f;
					}
				}
				else
				{
					myV.x = myV.x * rotAccel;
				}
				if(myV.y > 270f)
				{
					if(myV.y < 270.5f && myV.y > 269.5f)
					{
						myV.y = 270f;
					}
					else
					{
					myV.y = ((myV.y * rotAccel) + 270 * .01f);
					}
				}
				else
				{
					if(myV.y < 270.5f && myV.y > 269.5f)
					{
						myV.y = 270f;
					}
					else
					{
					myV.y = (myV.y * rotAccel) - (90 * .01f);
					}
				}
				if(myV.z < 180f)
				{
					if(myV.z < .05f)
					{
						myV.z = 0f;
					}
					else
					{
						myV.z = myV.z * rotAccel;
					}
				}
				else
				{
					if(myV.z >359.5f)
					{
						myV.z = 0f;
					}
					else
					{
						myV.z = ((360f - myV.z) * rotAccel);;
					}
				}
				this.transform.localEulerAngles = myV;
				linedUp = false;
			}
			
		}

	if(inControl)
		{
			if(camerasActive == false)
			{
				camerasActive = true;
				foreach(var camera in cameras)
				{
					if(camera.transform.parent.name == "ShipInterior")
					{
					camera.enabled = true;
					}
				}
				mLook.enabled = true;
			}

			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				pilotSeatLook = true;
			}
			else if(Input.GetKeyUp(KeyCode.LeftShift))
			{
				pilotSeatLook = false;
			}
			if(pilotSeatLook == true)
			{
				GetComponent<MouseLook>().enabled = false;
				foreach(var camera in cameras)
				{
					if(camera.transform.parent.name == "ShipInterior")
					{
					camera.GetComponent<MouseLook>().enabled = true;
						camera.GetComponent<MouseLook>().lockZ = true;
					}
				}
			}
			else
			{
				GetComponent<MouseLook>().enabled = true;
				foreach(var camera in cameras)
				{
					if(camera.transform.parent.name == "ShipInterior")
					{
						camera.GetComponent<MouseLook>().enabled = false;
						camera.GetComponent<MouseLook>().lockZ = false;
					}
				}
			}
			if(Input.GetKey(KeyCode.R))
			{
				getUp ();
			}
			if(docking || docked)
			{
				mLook.enabled = false;
			}
			else
			{
				GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<PlayerControl>().piloting = true;
				mLook.enabled = true;
			}
		}
		else
		{
	
		}
	}

	public void getUp()
	{
		inControl = false;
		camerasActive = false;
		foreach(var camera in cameras)
		{
			if(camera.transform.parent.name == "ShipInterior")
			{
				camera.enabled = false;
			}
		}
		GetComponent<MouseLook>().enabled = false;
		
		disableControl();
		GameObject player = GameObject.FindGameObjectWithTag("PlayerBody");
		player.GetComponent<PlayerMovementInShip>().freeInShip = true;

		player.GetComponent<PlayerControl>().state = player.gameObject.AddComponent<PlayerStateFree>();  
		//player.GetComponent<PlayerControl>().state.enableCameras();
		Destroy(GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<PlayerStatePilotingShip>());
	}

	public void disableControl()
	{
		GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<PlayerControl>().piloting = false;
	}


	public Vector3 getDistanceToDock()
	{
		if(dockingObject != null)
		{
			return shipDockPoint.transform.position - dockingObject.transform.position;// - new Vector3(-.35f,0f,0f);
		}
		else
			return Vector3.zero;
	}


	public IEnumerator switchCameraPause()
	{
		canSwitchViews = false;
		yield return new WaitForSeconds(2f);
		canSwitchViews = true;
	}
}
