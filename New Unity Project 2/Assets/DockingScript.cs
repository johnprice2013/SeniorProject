using UnityEngine;
using System.Collections;
using System.Threading;

public class DockingScript : MonoBehaviour {

	public GameObject player;
	public float distanceToPlayer;
	public bool dockingInProgress = false; 
	public bool interiorCreated = false;
	public float distanceToPlayerBody;
	public GameObject playerBody;
	public bool keyPressed = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerBody = GameObject.FindGameObjectWithTag("PlayerBody");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*	flow for docking
	 * remove control of ship,
	 * align ship and move to position
	 * spawn interior of station
	 * 
	 * flow for undocking
	 * remove interior of station
	 * move ship away
	 * allow control of ship
	 * 
	 * 
	 * 
	 * */

	void FixedUpdate()
	{
		if(playerBody != null)
		{
			distanceToPlayerBody = (this.transform.FindChild("DockingPoint").position - playerBody.transform.position).magnitude;
			if(distanceToPlayerBody < 10)
			{
				this.transform.parent.FindChild("space_station_4").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
			}
			else
			{
				this.transform.parent.FindChild("space_station_4").renderer.enabled = true;
			}
		}
		if(player != null)
		{
			distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

			if(distanceToPlayer < 200)
			{
				//Debug.Log ("close enough to dock");
				if(Input.GetKey(KeyCode.G) && keyPressed == false && player.GetComponent<ShipControl>().docked == false)
				{
					StartCoroutine(dontPress());
					dockingInProgress = true;
					player.GetComponent<ShipControl>().docking = true;
					//player.GetComponent<ShipContro>().docking = true;
					player.GetComponent<ShipControl>().dockingObject = this.transform.FindChild("DockingPoint").gameObject;
				}
			}
			if(player.GetComponent<ShipControl>().docked == true)
			{
				this.transform.parent.FindChild("StationInterior").gameObject.SetActive(true);
				
			}
			if(player.GetComponent<ShipControl>().linedUp == true && player.GetComponent<ShipControl>().docked == true && interiorCreated == false && this.distanceToPlayer < 1000)
			{
				//Thread newThread = new Thread(new ThreadStart(this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().createFloor));
				//newThread.Start ();
				Debug.Log ("docked, creating floor");
				player.GetComponent<ShipControl>().docking = false;
				dockingInProgress = false;
				StartCoroutine (this.transform.parent.FindChild ("StationInterior").GetComponent<StationGenerationTest>().createFloor2());
				//this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().createFloor();
				interiorCreated = true;
			}

			float quickDistance = (playerBody.transform.position - player.transform.position).magnitude;

			if(player.GetComponent<ShipControl>().docked == true && interiorCreated ==  true && quickDistance < 20f && Input.GetKey(KeyCode.G))
			{
				StartCoroutine(dontPress());
				Debug.Log ("undocking");
				player.GetComponent<ShipControl>().docked = false;
			}
			if(player.GetComponent<ShipControl>().docked == false && interiorCreated == true )
			{
				Debug.Log ("undocked, deleting floor");
				this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().clearFloor();
				interiorCreated = false;
					
			}
		}
	}


	public IEnumerator dontPress()
	{
		keyPressed = true;
		yield return new WaitForSeconds(.5f);
		keyPressed = false;
	}
}
