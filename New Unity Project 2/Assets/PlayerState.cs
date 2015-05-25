using UnityEngine;
using System.Collections;

public abstract class PlayerState : MonoBehaviour 
{
	public float distanceToFighter = 0f;
	public float distanceToSeat = 0f;
	public GameObject ship;
	public GameObject seat;
	public GameObject fighter;
	public bool cameraEnabled = false;
	public Camera[] cameras;
	public float playerSpeed = 0f;
	public Vector3 localGravity;
	public PlayerControl playerInit;
	public Vector3 localMovement;
	public Vector3 verticalSpeed;
	public bool inMenu;
	public bool camerasLocked;
	public bool seated = false;
	public bool rotationLocked = false;
	public bool menuLocked = false;

	public void Start()
	{
		ship = GameObject.FindGameObjectWithTag("Player");
		seat = GameObject.Find("pilotSeat");
		fighter = GameObject.FindGameObjectWithTag("Fighter");
//		Debug.Log ("Player State Initializing");
		cameras = GetComponentsInChildren<Camera>();
		this.transform.parent = ship.transform;
		localGravity = -ship.transform.up;
		playerSpeed = 3f;
		playerInit = GetComponent<PlayerControl>();
		inMenu = false;
		camerasLocked = false;
		unlockCameras();
		unlockRotation();
		StartCoroutine(timedMenuLock());

	}
	public PlayerState()
	{

	}

	public IEnumerator timedMenuLock()
	{
		menuLocked = true;
		yield return new WaitForSeconds(.5f);
		menuLocked = false;
	}

	public float getFighterDistance()
	{
	
		return (this.transform.position - fighter.transform.position).magnitude;
	}

	public float getSeatDistance()
	{
		return (this.transform.position - seat.transform.position).magnitude;
	}

	public bool IsGrounded()
	{
		RaycastHit myHit;
		if(Physics.Raycast (transform.position,-transform.up, out myHit,1f))
		{
			if(myHit.collider != null)//!myHit.collider.isTrigger)
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}
		else
		{
//			Debug.Log ("hitting nothing");
			return false;
		}
		//return Physics.Raycast(transform.position, -transform.up, 1f);
	}

	public void disableCameras()
	{
		foreach(var camera in cameras)
		{
			camera.enabled = false;
		}
		cameraEnabled = false;

	}


	public void enableCameras()
	{

		foreach(var camera in cameras)
		{
			camera.enabled = true;
		}
		cameraEnabled = true;

	}

	public void lockCameras()
	{
		foreach(var camera in cameras)
		{
			camera.GetComponent<MouseLook>().enabled = false;
		}
		camerasLocked = true;
	}

	public void lockRotation()
	{
		this.GetComponent<MouseLook>().enabled = false;
		rotationLocked = true;
	}

	public void unlockRotation()
	{
		this.GetComponent<MouseLook>().enabled = true;
		rotationLocked = false;
	}

	public void unlockCameras()
	{
		foreach(var camera in cameras)
		{
			camera.GetComponent<MouseLook>().enabled = true;
		}
		camerasLocked = false;
	}
	
	public abstract void movement();
	
	public abstract void checkNextState();
	
}



public class PlayerStateFree : PlayerState
{


	public override void movement()
	{
		if(cameraEnabled == false)
		{
			enableCameras();
		}

		if(rotationLocked == true)
		{
			unlockRotation();
		}

		localMovement = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		if(!IsGrounded())
		{

			verticalSpeed += -ship.transform.up/3 * Time.deltaTime;

		}
		else
		{

			verticalSpeed = Vector3.zero;
		}
		if(Input.GetKey(KeyCode.LeftShift))
		{
			playerSpeed = 6f;
		}
		else
		{
			playerSpeed = 3f;
		}
		if(Input.GetKey(KeyCode.W))
		{
			localMovement += transform.forward * playerSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.A))
		{
			localMovement += -transform.right * playerSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D))
		{
			localMovement += transform.right * playerSpeed * Time.deltaTime;
			
		}
		if(Input.GetKey(KeyCode.S))
		{
			localMovement  += -transform.forward * playerSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Space) && IsGrounded())
		{
			verticalSpeed += transform.up/8;
		}

		if(IsGrounded() && localMovement != Vector3.zero)
		{
			transform.FindChild("FootstepAudio").GetComponent<AudioSource>().enabled = true;
			transform.FindChild("FootstepAudio").GetComponent<AudioSource>().audio.pitch = (localMovement.magnitude)*10;
		}
		else
		{
			transform.FindChild("FootstepAudio").GetComponent<AudioSource>().enabled = false;

		}
		transform.Translate(localMovement + verticalSpeed,Space.World);

		distanceToFighter = getFighterDistance();

		distanceToSeat = getSeatDistance();






	}

	public override void checkNextState()
	{

		if(Input.GetKey (KeyCode.E) && distanceToFighter < 3f)
		{
//			Debug.Log ("player switching to piloting fighter state");
			GetComponent<MeshRenderer>().renderer.enabled = false;
			GetComponent<MouseLook>().enabled = false;
			playerInit.fighting = true;
			disableCameras();
			lockRotation();
			//freeInShip = false;
			fighter.GetComponent<FighterController>().inControl = true;
			this.GetComponent<PlayerControl>().state = gameObject.AddComponent<PlayerStatePilotingFighter>();
			lockCameras();
			Destroy(this);
		}

		if(Input.GetKey (KeyCode.E) && (distanceToSeat < 2f))
		{
//			Debug.Log ("player switching to piloting ship state");
			playerInit.piloting = true;
			//freeInShip = false;
			disableCameras ();
			lockRotation();
			ship.GetComponent<ShipControl>().state.inControl = true;
			this.GetComponent<PlayerControl>().state = gameObject.AddComponent<PlayerStatePilotingShip>();
			lockCameras();
			Destroy(this);
		}

		RaycastHit myHit;
		if(Input.GetKey(KeyCode.E)  && menuLocked == false)
		{
			if(Physics.Raycast (transform.position,transform.forward, out myHit,1.5f))
			{
//				Debug.Log (myHit.transform.name);
				if(myHit.collider.gameObject.tag == "3dMenu")// = )//!myHit.collider.isTrigger)
				{
					StartCoroutine(timedMenuLock());
//					Debug.Log ("switching to in menu state");
					this.GetComponent<PlayerControl>().state = gameObject.AddComponent<PlayerStateInMenu>();
					Destroy(this);
				}
			}
		}


	}
}





public class PlayerStatePilotingShip: PlayerState
{

	public override void movement()
	{
		seated = true;
		if(camerasLocked == false)
		{
			lockCameras();
		}
		if(rotationLocked == false)
		{
			lockRotation();
		}
		if(cameraEnabled == true)
		{
			disableCameras();
		}
	
	}

	public override void checkNextState()
	{
		if(playerInit.piloting == false)
		{
//			Debug.Log ("player switching to free state");
			this.GetComponent<PlayerControl>().state = gameObject.AddComponent<PlayerStateFree>();
			Destroy (this);
		}

		if(playerInit.docked == true && Input.GetKey(KeyCode.R))
		{
//			Debug.Log ("player switching to free state");
			this.GetComponent<PlayerControl>().state  = gameObject.AddComponent<PlayerStateFree>();
			playerInit.piloting = false;
			Destroy (this);
		}
	}
}


public class PlayerStatePilotingFighter: PlayerState
{
	
	public override void movement()
	{

	}
	
	public override void checkNextState()
	{

	}
}

public class PlayerStateInMenu : PlayerState
{
	public override void movement()
	{

		if(camerasLocked == false)
		{
			lockCameras();
		}
		if(rotationLocked == false)
		{
			lockRotation();
		}
	}

	public override void checkNextState()
	{
		if(Input.GetKey(KeyCode.E) && menuLocked == false)
		{

//			Debug.Log ("switching to free state");
			this.GetComponent<PlayerControl>().state = gameObject.AddComponent<PlayerStateFree>();
			Destroy(this);
		}
	}
}


public class PlayerStatePilotingMenu : PlayerState
{
	public override void movement()
	{

	}

	public override void checkNextState()
	{

	}
}