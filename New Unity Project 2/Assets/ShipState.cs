using UnityEngine;
using System.Collections;

public abstract class ShipState : MonoBehaviour {
	public bool inControl = false;
	public Camera[] cameras;
	public bool camerasActive = false;
	public MouseLook mLook;
	public bool mLookActive = false;
	public bool pilotSeatLook = false;
	public bool canSwitchViews = true;
	public bool inEnemyRange = false;
	public GameObject star;
	public GameObject enemyArea;
	public bool beingTracked;
	public float rotAccel;


	public GameObject player;
	// Use this for initialization
	void Start () {
	
		cameras = GetComponentsInChildren<Camera>();
		mLook = GetComponent<MouseLook>();
		GameObject.Find ("MainShip").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
		rotAccel = .99f;
		player = GameObject.FindGameObjectWithTag("PlayerBody");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract void movement();

	public abstract void checkNextState();



	public IEnumerator switchCameraPause()
	{
		canSwitchViews = false;
		yield return new WaitForSeconds(2f);
		canSwitchViews = true;
	}

	public void enableCameras()
	{
		foreach(var camera in cameras)
		{
			if(camera.transform.parent.name == "ShipInterior")
			{
				camera.enabled = true;
			}
		}
		camerasActive = true;
	}

	public void enableMouseLook()
	{
		mLook.enabled = true;
		mLookActive = true;
	}

	public void disableMouseLook()
	{
		mLook.enabled = false;
		mLookActive = false;
	}

	public void disableCameras()
	{
		foreach(var camera in cameras)
		{
			if(camera.transform.parent.name == "ShipInterior")
			{
				camera.enabled = false;
			}
		}
		camerasActive = false;
	}

	public void recenterCamera()
	{

	}
}

public class ShipStateInControl : ShipState
{
	public int count = 0;
	public override void movement()
	{
		if(mLookActive == false)
		{
			enableMouseLook();
		}
		if(camerasActive == false)
		{
			enableCameras();
		}
		foreach(Camera camera in cameras)
		{
		if(camera.transform.localEulerAngles != new Vector3(0,0,0))
			{
	 			camera.transform.localEulerAngles = new Vector3(0,0,0);
			}
		}
		if(count < 30)
		{
			count++;
		}
//		Debug.Log ("InControl");
		inControl = true;
	}

	public override void checkNextState()
	{
		if(this.GetComponent<ShipControl>().docking)
		{
	//		Debug.Log ("ship Switching to docking state");
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateDocking>();
			disableMouseLook();
			Destroy (this);
		}

		if(count >29 && Input.GetKey (KeyCode.LeftShift))
		{
	//		Debug.Log ("Ship switching to look state");
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateInMenu>();
			disableMouseLook();

			Destroy(this);

		}

		if(Input.GetKey(KeyCode.R))
		{
	//		Debug.Log ("ship switching to no control state");
			player.GetComponent<PlayerControl>().piloting = false;
			disableCameras();
			disableMouseLook();
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateNoControl>();
			Destroy (this);
		}
	}
}

public class ShipStateInMenu : ShipState
{
	public int count = 0;
	public override void movement()
	{
		foreach(Camera camera in cameras)
		{
			if(camera.transform.localEulerAngles != new Vector3(0,32,0))
			{
				camera.transform.localEulerAngles = new Vector3(0,32,0);
			}
			//transition camera to point at menu
		}

		if(count < 30)
		{
			count++;
		}

	}


	public override void checkNextState()
	{
		if(count >= 29 && Input.GetKey(KeyCode.LeftShift))
		{
			enableCameras ();
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateInControl>();
			Destroy(this);

		}

	}

}


public class ShipStateNoControl : ShipState
{
	public override void movement()
	{
		inControl = false;
		if(player.GetComponent<PlayerControl>().piloting == false)
		{
			disableCameras();
		}
		else if(player.GetComponent<PlayerControl>().piloting == true)
		{
			enableCameras();
		}

	}

	public override void checkNextState()
	{
		if(player.GetComponent<PlayerControl>().state.seated == true && player.GetComponent<PlayerControl>().docked == false)
		{
//			Debug.Log ("ship switching to in control state");
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateInControl>();
			Destroy (this);
		}

		if(player.GetComponent<PlayerControl>().state.seated == true && player.GetComponent<PlayerControl>().docked == false && player.GetComponent<ShipControl>().docking == false)
		{
//			Debug.Log ("ship switching to in control state");
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateInControl>();
			Destroy(this);
		}

		if(player.GetComponent<PlayerControl>().state.seated == true && player.GetComponent<PlayerControl>().docked == true)
		{
			if(Input.GetKey (KeyCode.G))
			{
				this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateInControl>();
				player.GetComponent<PlayerControl>().docked = false;
				this.GetComponent<ShipControl>().linedUp = false;
				Destroy(this);
			}
		}
	}

}

public class ShipStateDocking : ShipState
{

	public override void movement()
	{
//		Debug.Log ("Docking");
		this.GetComponent<ShipControl>().docking = true;
		Vector3 quickV = this.transform.localEulerAngles;
		//this.GetComponent<MouseLook>().enabled = false;
		//			Debug.Log ("making it here");
		if((quickV.y < 270.5 && quickV.y > 269.5) && (quickV.x < .5 || quickV.x > 359.5) && (quickV.z < .5 || quickV.z > 359.5))
		{
			//this.transform.localRotation = dockingObject.transform.localRotation;
			this.transform.localEulerAngles = new Vector3(0f,270f,0f);
//			Debug.Log ("lined up");
			this.GetComponent<ShipControl>().linedUp = true;
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
			this.GetComponent<ShipControl>().linedUp = false;
		}

	}

	public override void checkNextState()
	{
		if(this.GetComponent<ShipControl>().docked == true)
		{
	//		Debug.Log ("ship switching to no control state");
			this.GetComponent<ShipControl>().state = gameObject.AddComponent<ShipStateNoControl>();
			Destroy (this);
		}

	}
}
