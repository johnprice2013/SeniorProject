using UnityEngine;
using System.Collections;

public class DockingScript : MonoBehaviour {

	public GameObject player;
	public float distanceToPlayer;
	public bool dockingInProgress = false; 



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
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

	}
}
