using UnityEngine;
using System.Collections;
using System.Threading;

public class StationDock : MonoBehaviour {
	
	public GameObject player;
	public float distanceToPlayer;
	public bool dockingInProgress = false; 
	public bool interiorCreated = false;
	public float distanceToPlayerBody;
	public GameObject playerBody;
	public bool keyPressed = false;
	public GameObject stationInt;
	public GameObject stationRealInt;

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
				this.transform.parent.FindChild("ASOuterShell").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
			}
			else
			{
				this.transform.parent.FindChild("ASOuterShell").renderer.enabled = true;
			}
		}
		if(player != null)
		{
			distanceToPlayer = (this.transform.position - player.transform.position).magnitude;
			
			if(distanceToPlayer < 200)
			{

				if(Input.GetKey(KeyCode.G) && keyPressed == false && player.GetComponent<ShipControl>().docked == false)
				{
					StartCoroutine(dontPress());
					dockingInProgress = true;
					player.GetComponent<ShipControl>().docking = true;

					player.GetComponent<ShipControl>().dockingObject = this.transform.FindChild("DockingPoint").gameObject;
				}
			}
			if(player.GetComponent<ShipControl>().docked == true)
			{
				//this.transform.parent.FindChild("StationInterior").gameObject.SetActive(true);
				
			}
			if(player.GetComponent<ShipControl>().linedUp == true && player.GetComponent<ShipControl>().docked == true && interiorCreated == false && this.distanceToPlayer < 1000)
			{

				Debug.Log ("docked, creating floor");
				player.GetComponent<ShipControl>().docking = false;
				dockingInProgress = false;
				stationRealInt = (GameObject) Instantiate(stationInt,Vector3.zero,Quaternion.identity);
				stationRealInt.transform.parent = this.transform;
				stationRealInt.transform.localPosition = Vector3.zero;
				//StartCoroutine (this.transform.parent.FindChild ("StationInterior").GetComponent<StationGenerationTest>().createFloor2());

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
				Destroy(stationRealInt.gameObject);
				//this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().clearFloor();
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
