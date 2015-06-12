using UnityEngine;
using System.Collections;

public class ShowDetails : MonoBehaviour {
	public GameObject player;
	public float playerX = 0;
	public float playerY = 0;
	public float playerZ = 0;
	public GameObject [] planets;
	GUIText myGui;
	public PlanetMovement planMov;
	public float mag = 0f;
	public float preMag = 0f;
	public float dMag = 0f;
	public Vector3 percievedChange = Vector3.zero;
	public Vector3 actualChange = Vector3.zero;
	// Use this for initialization
	void Start () {
		myGui = GetComponent<GUIText>();
		myGui.transform.position = new Vector3(.75f,.95f,0f);
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		player = GameObject.FindGameObjectWithTag("star");
		if(player)
		{
		playerX = player.transform.position.x;
		playerY = player.transform.position.y;
		playerZ = player.transform.position.z;
		}
		planets = GameObject.FindGameObjectsWithTag("planet");
		int planetCount = planets.Length;
		//Debug.Log ("this script is active");
	
		if(planetCount >= 3)
		{
			planMov = planets[0].GetComponent<PlanetMovement>();
			percievedChange = planets[0].GetComponent<GravityFromPlanet>().forceDueToGravity;
			dMag = player.GetComponent<movement>().totalVelocity / Time.deltaTime;
			mag = planets[0].GetComponent<GravityFromPlanet>().distanceToPlayer;
			preMag = mag;

		}
		else
		{
			mag = 0f;
		}
		myGui.text = ("Current velocity = " + dMag);
}
}