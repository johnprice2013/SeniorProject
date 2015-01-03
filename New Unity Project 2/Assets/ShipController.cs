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
	// Use this for initialization
	void Start () {
		cameras = GetComponentsInChildren<Camera>();
		mLook = GetComponent<MouseLook>();
		GameObject.Find ("MainShip").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;

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

				GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Initialize>().piloting = false;
				GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<PlayerMovementInShip>().freeInShip = true;
			}
		}
		else
		{
	
		}
	}



	public IEnumerator switchCameraPause()
	{
		canSwitchViews = false;
		yield return new WaitForSeconds(2f);
		canSwitchViews = true;
	}
}
