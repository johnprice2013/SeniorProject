using UnityEngine;
using System.Collections;

public class GravityFromPlanet : MonoBehaviour {
	public GameObject player;
	public float distanceToPlayer = 0f;
	public Vector3 playerPosition = Vector3.zero;
	public Vector3 currentPosition = Vector3.zero;
	public Vector3 directionToPlanet = Vector3.zero;
	public Vector3 forceDueToGravity = Vector3.zero;
	public float planetMass = 0f;
	public movement gravMov;
	public float G = 2f;
	public float forceNumber = 0f;
	public double gravitationalForce = 0d;
	public double gravPerSecond = 0d;
	public bool planetSet = false;
	// Use this for initialization
	void Start () {
		G = 50f;
		player = GameObject.FindGameObjectWithTag("Player");
		gravMov = GameObject.FindGameObjectWithTag("star").GetComponent<movement>();
		planetMass = GetComponent<PlanetMovement>().planetMass;

	}
	
	// Update is called once per frame
	void Update () {



	}
	void FixedUpdate()
	{
		playerPosition = player.transform.position;
		currentPosition = transform.position;
		directionToPlanet = currentPosition - playerPosition;
		distanceToPlayer = directionToPlanet.magnitude;
		directionToPlanet = directionToPlanet.normalized;

		gravitationalForce = (((double)G*(double)planetMass)/ System.Math.Pow ((double)distanceToPlayer, 2d));
		gravPerSecond = gravitationalForce;
		gravitationalForce *= Time.deltaTime;
		forceDueToGravity = (float)gravitationalForce * -directionToPlanet;
		if(distanceToPlayer < 500000)
		{
			forceNumber = forceDueToGravity.magnitude;
			gravMov.forceFromPlanets = forceDueToGravity;

		}
		else if(distanceToPlayer  < 750000)
		{
			gravMov.forceFromPlanets = Vector3.zero;
		}


	}
}
