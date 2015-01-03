using UnityEngine;
using System.Collections;

public class SwitchSector : MonoBehaviour {

	public GameObject star;
	public Vector3 starLocation;
	public GameObject sectorCenter;
	public GameObject[] planets;
	public GameObject newSector;
	public float savedX = 0f;
	public float savedY = 0f;
	public float savedZ = 0f;
	public Vector3 savedPosition = new Vector3(10000000f,0f,0f);
	public bool firstStart = true;
	public GameObject[] stations;
	public GameObject[] asteroids;

	// Use this for initialization
	void Start () {
		savedPosition = new Vector3(10000000f, 0f, 0f);
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		star = GameObject.FindGameObjectWithTag("star");
		starLocation = star.transform.position;
		sectorCenter = GameObject.FindGameObjectWithTag("sectorCenter");
			planets = GameObject.FindGameObjectsWithTag("planet");
		if(starLocation.x < -100000000)
		{
			GameObject.Find ("SectorData").GetComponent<SectorStorage>().updateSectors (0, 'x');
		}
		if(starLocation.y < -100000000)
		{
			GameObject.Find ("SectorData").GetComponent<SectorStorage>().updateSectors (0, 'y');
		}
		if(starLocation.z < -100000000)
		{
			GameObject.Find ("SectorData").GetComponent<SectorStorage>().updateSectors (0, 'z');
		}
		if(starLocation.x > 100000000)
		{
			GameObject.Find ("SectorData").GetComponent<SectorStorage>().updateSectors (1, 'x');
		}
		if(starLocation.y > 100000000)
		{
			GameObject.Find ("SectorData").GetComponent<SectorStorage>().updateSectors (1, 'y');
		}
		if(starLocation.z > 100000000)
		{
			GameObject.Find ("SectorData").GetComponent<SectorStorage>().updateSectors (1, 'z');
		}
		if( Mathf.Abs(starLocation.x) > 100000000 || Mathf.Abs(starLocation.y) > 100000000 || Mathf.Abs(starLocation.z) > 100000000)
		{
			Debug.Log ("switching sectors");
			GameObject player = GameObject.FindGameObjectWithTag("PlayerBody");
			Initialize init = player.GetComponent<Initialize>();
			init.sectorX += (int)starLocation.x/100000000;
			init.sectorY += (int)starLocation.y/100000000;
			init.sectorZ += (int)starLocation.z/100000000;
			if(Mathf.Abs(starLocation.x) > 100000000)
			{
				starLocation.x = (int)-star.transform.position.x/100000000;
				starLocation.x *= 99999999;
			}
			if(Mathf.Abs(starLocation.y) > 100000000)
			{
				starLocation.y = (int)-star.transform.position.y/100000000;
				starLocation.y *= 99999999;
			}
			if(Mathf.Abs(starLocation.z) > 100000000)
			{
				starLocation.z = (int)-star.transform.position.z/100000000;
				starLocation.z *= 99999999;
			}
			SectorGenerator secGen = sectorCenter.GetComponent<SectorGenerator>();
			planets = GameObject.FindGameObjectsWithTag("planet");
			foreach(GameObject planet in planets)
			{
				Destroy (planet);
			}
			asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
			foreach(GameObject asteroid in asteroids)
			{
				Destroy(asteroid);
			}
			stations = GameObject.FindGameObjectsWithTag("station");
			foreach(GameObject station in stations)
			{
				DestroyImmediate (station);
			}
			movement starMov = star.GetComponent<movement>();
			savedX = starMov.totalAccel.x;
			savedY = starMov.totalAccel.y;
			savedZ = starMov.totalAccel.z;
			savedPosition = starLocation;
			Destroy (star);
			planets = null;
			secGen.createSector ();

			GameObject.FindGameObjectWithTag("FlightNavigation").GetComponent<PlanetInfoUI>().resetButtons();

			checkLocalMissions();


		}
		savedPosition = starLocation;
	}

	public void changeFirstStart()
	{
		firstStart = false;
	}

	public void checkLocalMissions()
	{
		foreach(Transform missions in GameObject.FindGameObjectWithTag("PlayerBody").transform)
		{
			if(missions.name == "Mission(Clone)")
			{
				Debug.Log ("player has missions");
				missions.GetComponent<MissionInfo>().checkIfLocal();
			}
		}
	}
}
