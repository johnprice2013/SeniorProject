using UnityEngine;
using System.Collections;

public class InitializeGhostPlanet : MonoBehaviour {

	public GameObject ghostStation;
	public GameObject tempGhostStation;
	public int seed;
	public int planetNumber;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(int importSeed, int planetNum)
	{
		seed = importSeed;
		planetNumber = planetNum;

		tempGhostStation = (GameObject)Instantiate(ghostStation);
		tempGhostStation.GetComponent<InitializeGhostStation>().Initialize (seed);
		tempGhostStation.transform.parent = this.transform;
	}
}
