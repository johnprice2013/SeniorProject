using UnityEngine;
using System.Collections;

public class OrbitalGenerator : MonoBehaviour {

	//public int seed = 0;
	public int stationChoice = 0;
	public float statVelocity;
	public GameObject station;
	public int stationSeed = 0;
	public GameObject planetStation;
	// Use this for initialization
	void Start () {
	
	}

	public void initialize(int seed)
	{
		stationSeed = seed;
		Random.seed = seed;
		stationChoice = Random.Range (0,5);
		if(stationChoice == 0)
		{
			planetStation = (GameObject)Instantiate (station, new Vector3(20000+GetComponent<PlanetMovement>().planetRadius,0,0) + transform.position, Quaternion.identity);
			planetStation.GetComponent<StationData>().stationSeed = stationSeed;
			planetStation.GetComponent<StationData>().stationPlanet = (GameObject)this.gameObject;
			planetStation.GetComponent<StationMovement>().parent = (GameObject)this.gameObject;
			GetComponent<PlanetInfo>().hasAbandonedStation = true;
		}
		else if(stationChoice < 4)
		{
			planetStation = (GameObject)Instantiate (station, new Vector3(20000+GetComponent<PlanetMovement>().planetRadius,0,0) + transform.position, Quaternion.identity);
			planetStation.GetComponent<StationData>().stationSeed = stationSeed;
			planetStation.GetComponent<StationData>().stationPlanet = (GameObject)this.gameObject;
			planetStation.GetComponent<StationMovement>().parent = (GameObject)this.gameObject;
			GetComponent<PlanetInfo>().hasStation = true;
		}



	}

	// Update is called once per frame
	void Update () {
	
	}
}
