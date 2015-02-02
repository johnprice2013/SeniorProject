using UnityEngine;
using System.Collections;

public class MiningDroneScript : MonoBehaviour {

	public bool droneActive = false;
	public GameObject targetedObject;
	public GameObject parent;
	public float distanceToAsteroid = 0f;
	public bool movingToShip = false;
	// Use this for initialization
	void Start () {
		parent = GameObject.Find ("ShipInterior");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(droneActive && targetedObject != null)
		{
			distanceToAsteroid = (transform.position - targetedObject.transform.position).magnitude;
			if(distanceToAsteroid> 50)
			{
				transform.Translate(transform.forward * Time.deltaTime * (distanceToAsteroid/10),Space.World);
			}
			this.transform.LookAt(targetedObject.transform.position);
			if(distanceToAsteroid <= 50)
			{
				Debug.Log ("mining");
			}



		}
		else if(parent.transform.name != "ShipInterior")
		{
			parent = GameObject.Find ("ShipInterior");
			movingToShip = true;
			StartCoroutine (moveToShipTimer());
			Debug.Log ("starting move to ship");
		}
		else if(movingToShip)
		{
			transform.Translate (((parent.transform.position+new Vector3(0f,-1.5f,14f)) - transform.position) * Time.deltaTime,Space.World);// * distanceToAsteroid);
		}
		else
		{
			this.transform.position = (parent.transform.position+new Vector3(0f,-1.5f,14f));
		}


	}

	public IEnumerator moveToShipTimer()
	{
		yield return new WaitForSeconds(5f);
		movingToShip = false;
	}

	public void activateDrone(GameObject asteroid)
	{
		droneActive = true;
		targetedObject = asteroid;
		transform.parent = asteroid.transform;
		parent = asteroid;
	}

	public void deactivateDrone()
	{
		transform.parent = GameObject.Find ("ShipInterior").transform;
		droneActive = false;
	}

}
