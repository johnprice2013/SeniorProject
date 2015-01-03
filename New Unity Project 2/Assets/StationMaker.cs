using UnityEngine;
using System.Collections;

public class StationMaker : MonoBehaviour {

	public float statVelocity;
	public GameObject station;
	public int stationSeed = 0;
	public GameObject planetStation;

	// Use this for initialization
	void Start () {
//		stationSeed = GetComponent<PlanetMovement>().seed;
//		planetStation = (GameObject)Instantiate (station, new Vector3(20000+GetComponent<PlanetMovement>().planetRadius,0,0) + transform.position, Quaternion.identity);
//		planetStation.GetComponent<StationData>().stationSeed = stationSeed;
//		planetStation.GetComponent<StationData>().stationPlanet = (GameObject)this.gameObject;
//		planetStation.GetComponent<StationMovement>().parent = (GameObject)this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
