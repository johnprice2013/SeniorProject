using UnityEngine;
using System.Collections;

public class PlanetTargetScript : MonoBehaviour {
	public GameObject targetObject;
	public Vector3 reticalPosition = Vector3.zero;

	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(targetObject != null)
		{
			reticalPosition = targetObject.transform.position - this.transform.parent.transform.position;// - targetPlanet.transform.position;
			reticalPosition = this.transform.parent.transform.position + (reticalPosition.normalized * 14f);
				transform.position = reticalPosition;
		}
		else
		{
		}
	}

	public void setPlanet(GameObject planet)
	{
		targetObject = planet;
	}
}
