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

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerBody = GameObject.FindGameObjectWithTag("PlayerBody");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
			if(Input.GetKey(KeyCode.G))
			{
				dockingInProgress = true;
				player.GetComponent<ShipController>().docking = true;
				player.GetComponent<ShipController>().dockingObject = this.transform.FindChild("DockingPoint").gameObject;
			}
		}
		if(player.GetComponent<ShipController>().docked == true)
		{
			this.transform.parent.FindChild("StationInterior").gameObject.SetActive(true);
			
		}
			if(player.GetComponent<ShipController>().linedUp == true && player.GetComponent<ShipController>().docked == true && interiorCreated == false && this.distanceToPlayer < 1000)
			{
				//Thread newThread = new Thread(new ThreadStart(this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().createFloor));
				//newThread.Start ();
				StartCoroutine (this.transform.parent.FindChild ("StationInterior").GetComponent<StationGenerationTest>().createFloor2());
				//this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().createFloor();
				interiorCreated = true;
			}

			if(player.GetComponent<ShipController>().docked == false && interiorCreated == true)
			{
				this.transform.parent.FindChild("StationInterior").GetComponent<StationGenerationTest>().clearFloor();
				interiorCreated = false;
				
			}


		}
	}
}
