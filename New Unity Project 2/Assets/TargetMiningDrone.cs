using UnityEngine;
using System.Collections;

public class TargetMiningDrone : MonoBehaviour {
	//public int hitCount = 0;
	public bool currentlySendingDrone = false;
	public bool pointingAtAsteroid = false;
	RaycastHit hitInfo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		detectIfPointingAtAsteroid();
		if(pointingAtAsteroid && !currentlySendingDrone && Input.GetKey (KeyCode.B))
		{
			GameObject.Find ("MiningDrone").GetComponent<MiningDroneScript>().activateDrone((GameObject)hitInfo.transform.gameObject);
			StartCoroutine(waitToSendAnother());
		}
		if(Input.GetKey(KeyCode.N))
		{
			GameObject.Find ("MiningDrone").GetComponent<MiningDroneScript>().deactivateDrone();
		}
	}

	public void detectIfPointingAtAsteroid()
	{
		//RaycastHit hitInfo;
	 	if(Physics.Raycast(transform.position+ this.transform.forward*16,this.transform.forward,out hitInfo,150f))
		{

			//Debug.Log ("something in the way " + hitInfo.distance);
			//Debug.Log(hitInfo.transform.name);
			pointingAtAsteroid = true;

//			hitCount++;
		}
		else
		{
			pointingAtAsteroid = false;
		}

	}

	public IEnumerator waitToSendAnother()
	{
		yield return new WaitForSeconds(2f);
		currentlySendingDrone = false;
	}
}
