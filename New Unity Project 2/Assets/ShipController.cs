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
	// Use this for initialization
	void Start () {
		cameras = GetComponentsInChildren<Camera>();
		mLook = GetComponent<MouseLook>();
		GameObject.Find ("MainShip").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
		shipDockPoint = this.transform.FindChild("ShipDockPort").FindChild("ShipDockPoint").gameObject;
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

		if(dockingObject != null)
		{
			distanceToDock = dockingObject.transform.position-shipDockPoint.transform.position - new Vector3(-.35f,0f,0f);
		}
		if(docking)
		{
			//this.GetComponent<MouseLook>().enabled = false;
//			Debug.Log ("making it here");
			if(((dockingObject.transform.rotation.eulerAngles - (this.transform.rotation.eulerAngles - new Vector3(0f,90f,0f))).magnitude) < 1f)
			{
				this.transform.localRotation = dockingObject.transform.localRotation;
//				Debug.Log ("lined up");
			}
			else
			{
				Vector3 myV = this.transform.localEulerAngles;
				if(myV.x >= 180)
				{
					myV.x = ((360f - myV.x) * .99f);
					if(myV.x > 359.5f || myV.x < .5f)
					{
						myV.x = 0f;
					}
				}
				else
				{
					myV.x = myV.x * .99f;
				}
				if(myV.y > 270f)
				{
					if(myV.y < 270.5f && myV.y > 269.5f)
					{
						myV.y = 270f;
					}
					else
					{
					myV.y = ((myV.y * .99f) + 270 * .01f);
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
					myV.y = (myV.y * .99f) - (90 * .01f);
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
						myV.z = myV.z * .99f;
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
						myV.z = ((360f - myV.z) * .99f);;
					}
				}
				this.transform.localEulerAngles = myV;
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
		GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<PlayerMovementInShip>().freeInShip = true;

	}

	public void disableControl()
	{
		GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Initialize>().piloting = false;
	}



	public IEnumerator switchCameraPause()
	{
		canSwitchViews = false;
		yield return new WaitForSeconds(2f);
		canSwitchViews = true;
	}
}
