using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

	public int numOfPlanets = 0;
	public int distanceFromSun = 0;
	public int radiusOfPlanet = 0;
	public Transform planet;
	public int planetCount = 0;
	public int planetSeed = 5;
	public int[] seedArray;
	public Transform asteroids;
	public Transform asteroid;
	// Use this for initialization
	void Start () {
	
	}

 
	public void createPlanets(int seed)
	{
		Random.seed = seed;
		numOfPlanets = Random.Range(3,8);
		seedArray = new int[numOfPlanets];
		for(int i = 1; i < numOfPlanets+1; i++)
		{
			Random.seed = seed+i;
			if(Random.Range (0,6) < 4)
			{
			generatePlanet(seed, i);
			}
			else
			{
			generateAsteroids(seed,i);
			}
		}


	}

	public void generateAsteroids(int seed, int planetNumber)
	{
		Random.seed = seed;
		seedArray[planetNumber-1] = seed+planetNumber;
		
		distanceFromSun = Random.Range (10000000,10500000)*planetNumber;
		
 		asteroid =  (Transform)Instantiate(asteroids ,new Vector3((float)distanceFromSun, 0f , 0f)+transform.position,Quaternion.identity);
		asteroid.GetComponent<Asteroids>().asteroidSeed = seed+planetNumber;
		planetCount++;

	}

	public void generatePlanet(int seed, int planetNumber)
	{

		Random.seed = seed;
		seedArray[planetNumber-1] = seed+planetNumber;

		distanceFromSun = Random.Range (10000000,10500000)*planetNumber;

		Instantiate(planet,new Vector3((float)distanceFromSun, 0f , 0f)+transform.position,Quaternion.identity);
		planetCount++;
	}
	// Update is called once per frame

	public void updatePlanets()
	{
	}

	void Update () {
	}
}
