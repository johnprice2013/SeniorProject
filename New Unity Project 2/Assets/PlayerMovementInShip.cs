using UnityEngine;
using System.Collections;

public class PlayerMovementInShip : MonoBehaviour {

	public GameObject playerShip;
	public Vector3 localGravity = Vector3.zero;
	public Initialize playerInit;
	public Vector3 deltaRotation = Vector3.zero;
	public Vector3 inShipPosition = Vector3.zero;
	public bool childOfShip = false;
	public Vector3 localMovement = Vector3.zero;
	public Vector3 verticalSpeed = Vector3.zero;
	public float playerSpeed = 0f;
	public GameObject pilotSeat;
	public float distanceToSeat = 0f;
	public bool cameraEnabled = true;
	public Camera[] cameras;
	public float distanceToFighter = 0f;
	public GameObject fighter;
	public bool freeInShip = true;
	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindGameObjectWithTag("Player");
		localGravity = -playerShip.transform.up;
		playerInit = GetComponent<Initialize>();
		playerSpeed = 3f;
		pilotSeat = GameObject.Find ("pilotSeat");
		cameras = GetComponentsInChildren<Camera>();
		fighter = GameObject.Find ("Fighter");

		//inShipPosition = playerShip.transform;
		//transform.position = inShipPosition + new Vector3(0,3,0);
		//inShipPosition = transform.position;
		//transform.position = inShipPosition;
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		if(freeInShip == true)
		{
			if(cameraEnabled == false)
			{
				cameraEnabled = true;
				foreach(var camera in cameras)
				{
					camera.enabled = true;
				}
			}
			if(childOfShip == false)
			{
				transform.parent = playerShip.transform;
				childOfShip = true;
			}
			localMovement = Vector3.zero;
			localGravity = -playerShip.transform.up;
			rigidbody.velocity = Vector3.zero;
			if(!IsGrounded())
			{
				verticalSpeed += -playerShip.transform.up/3 * Time.deltaTime;
			}
			else
			{
				verticalSpeed = Vector3.zero;
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

			transform.Translate(localMovement + verticalSpeed,Space.World);
		
			distanceToSeat = (transform.position - pilotSeat.transform.position).magnitude;
			if(Input.GetKey (KeyCode.E) && (distanceToSeat < 2f))
			{
				playerInit.piloting = true;
				freeInShip = false;
				foreach(var camera in cameras)
				{
					camera.enabled = false;
				}
				cameraEnabled = false;
				playerShip.GetComponent<ShipController>().inControl = true;
			}
			distanceToFighter = (transform.position - fighter.transform.position).magnitude;
			if(Input.GetKey (KeyCode.E) && distanceToFighter < 3f)
			{
				GetComponent<MeshRenderer>().renderer.enabled = false;
				GetComponent<MouseLook>().enabled = false;
				playerInit.fighting = true;
				foreach(var camera in cameras)
				{
					camera.enabled = false;
				}
				cameraEnabled = false;
				freeInShip = false;
				fighter.GetComponent<FighterController>().inControl = true;
			}

		}
		else
		{
			//set position to pilots seat and move with ship.
		}
	}

	public bool IsGrounded()
	{
		RaycastHit myHit;
		if(Physics.Raycast (transform.position,-transform.up, out myHit,1f))
		   {
			if(!myHit.collider.isTrigger)
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
			return false;
		}
		//return Physics.Raycast(transform.position, -transform.up, 1f);
	}


}
