using UnityEngine;
using System.Collections;

public class InitializeGhostStar : MonoBehaviour {
	public int numOfPlanets = 0;
	public int[] seedArray;
	public GameObject ghostPlanet;
	public GameObject tempGhostPlanet;
	public int planetCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(int seed, float sectorX, float sectorY, float sectorZ)
	{
		Random.seed = seed;
		numOfPlanets = Random.Range(3,8);
		seedArray = new int[numOfPlanets];
		for(int i = 1; i < numOfPlanets+1; i++)
		{
			generatePlanet(seed, i);
			
		}


	}

	public void generatePlanet(int seed, int planetNumber)
	{
		Random.seed = seed;
		seedArray[planetNumber-1] = seed+planetNumber;
		GameObject tempGhostPlanet =  (GameObject) Instantiate(ghostPlanet);
		tempGhostPlanet.GetComponent<InitializeGhostPlanet>().Initialize(seed + planetNumber, planetNumber);
		tempGhostPlanet.transform.parent = this.gameObject.transform;


		planetCount++;

	}
}
