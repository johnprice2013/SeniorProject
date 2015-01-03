using UnityEngine;
using System.Collections;

public class StationMovement : MonoBehaviour {
	public GameObject[] planets;
	public GameObject parent;
	public float testDistance = 0f;
	public float newDistance = 0f;
	public float planetMass = 0f;
	public double neededVelocity = 0d;
	public double neededRotation = 0d;
	public double tempMovTime = 0d;
	public double totalMovTime = 0d;
	public float newXPosition = 0f;
	public float newYPosition = 0f;
	public float newZPosition = 0f;
	public float distanceFromPlanet = 0f;
	public Vector3 newFullPosition = Vector3.zero;
	public Vector3 planetPosition = Vector3.zero;
	public Vector3 deltaPosition = Vector3.zero;
	public double G = 1d;
	public float totalMovement = 0f;
	public double trueX = 0d;
	public double trueY = 0d;
	public double trueZ = 0d;
	public PlanetMovement planMov;
	public float realDistance = 0f;
	public float distanceToPlayer = 0f;
	public float newDelta = 0f;
	public float tempVelocity = 0f;
	public float displayVelocity = 0f;
	public Vector3 stationDirection = Vector3.zero;
	public Vector3 usedDirection = Vector3.zero;
	// Use this for initialization
	void Start () {

		newDistance = 1000000000;


		G = 50d;
		

		Vector3 temp = transform.position - parent.transform.position;
		distanceFromPlanet = temp.magnitude;
	
		planMov = parent.GetComponent<PlanetMovement>();
		planetMass = planMov.planetMass;
	

		neededVelocity = System.Math.Sqrt((G * (double) planetMass)/ (double) distanceFromPlanet);
		neededRotation = neededVelocity/(double)distanceFromPlanet;
		tempMovTime = (double)Time.time;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		

		tempMovTime += (double)Time.deltaTime;
		
	
		totalMovTime = neededRotation * tempMovTime;
	
		planetPosition = parent.transform.position;
		trueX = planMov.tempX + (double) distanceFromPlanet * System.Math.Sin (totalMovTime);
		trueY = planMov.tempY + (double) distanceFromPlanet * System.Math.Cos (totalMovTime);
		trueZ = planMov.tempZ;
		newXPosition = (float) trueX;
		newYPosition = (float) trueY;
		newZPosition = (float) trueZ;
	
		
		newFullPosition = new Vector3(newXPosition,newYPosition,newZPosition);
		
		deltaPosition = newFullPosition-transform.position;
		newDelta += Time.deltaTime;
		tempVelocity += deltaPosition.magnitude;
		stationDirection += deltaPosition.normalized;
		if(newDelta >= 5f)
		{
			usedDirection = stationDirection.normalized;
			displayVelocity = tempVelocity/5f;
			usedDirection = usedDirection * (float)neededVelocity;
			newDelta = 0f;
			tempVelocity = 0f;
			stationDirection = Vector3.zero;
		}
		totalMovement = deltaPosition.magnitude / Time.deltaTime;

		transform.Translate(deltaPosition);
		distanceToPlayer = transform.position.magnitude;
		realDistance = (transform.position - parent.transform.position).magnitude;

	}
}
