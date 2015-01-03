using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour {

	public GameObject tempAsteroid;
	public GameObject singleAsteroid;
	public int asteroidSeed = 0;
	public int numOfAsteroids = 0;


	public GameObject star;
	public float distanceFromSun = 0f;
	public float xAngle = 0f;
	public float yAngle = 0f;
	public float newXPosition = 0f;
	public float newYPosition = 0f;
	public float newZPosition = 0f;
	public Vector3 newFullPosition = Vector3.zero;
	public Vector3 deltaPosition = Vector3.zero;
	public Vector3 starPosition = Vector3.zero;
	public float xRot = 0f;
	public float yRot = 0f;
	public float zRot = 0f;
	public int seed = 0;
	public int orbitalSpeed = 0;
	public int planetNum;
	public float distanceFromStar = 0f;
	public GameObject center;
	public PlanetGenerator planetGen;
	public float timeOffset = 0f;
	public float tempMovTime = 0f;
	public float tempRotTime = 0f;
	public int rotationSpeed = 0;
	public Vector3 deltaRotation = Vector3.zero;
	public Vector3 newFullRotation = Vector3.zero;
	public float planetRadius;
	public float starMass;
	public float planetMass;
	public float neededVelocity;
	public float G = 2f;
	public float neededRotation;
	public float totalMovTime;
	public float xChange = 0f;
	public float yChange = 0f;
	public float test1 = 0f;
	public double test2 = 0f;
	public double tempX = 0d;
	public double tempY = 0d;
	public double tempZ = 0d;
	public double tempNewX = 0d;
	public double tempNewY = 0d;
	public double tempNewZ = 0d;
	public GravityFromPlanet planGrav;
	public Vector3 newMove = Vector3.zero;
	public movement starMov;
	public float tempVelocity = 0f;
	public float displayVelocity = 0f;
	public float newDelta = 0f;

	// Use this for initialization
	void Start () {
		setVariables();
		initialize();
		createAsteroidField();

	}

	public void initialize()
	{
		Random.seed = asteroidSeed;
		numOfAsteroids = Random.Range (20,41);

	}

	public void setVariables()
	{
		star = GameObject.Find ("Star(Clone)");
		//PlanetInfo planetInfo = GetComponent<PlanetInfo>();


		//tempX = planetInfo.tempX;
		//tempY = planetInfo.tempY;
		//tempZ = planetInfo.tempZ;
		//timeOffset = planetInfo.timeOffset;
		//rotationSpeed = planetInfo.rotationSpeed;
		starMov = star.GetComponent<movement>();
		Vector3 temp = transform.position - star.transform.position;
		distanceFromSun = temp.magnitude;
	
		//Debug.Log (starMov.forceNumber);
		
	}


	public void createAsteroidField()
	{
		int distanceFromCenter = 10;
		for(int x = 0; x<numOfAsteroids; x++)
		{
			Random.seed = x+asteroidSeed;
			Vector3 tempCoords = Vector3.zero;
			tempCoords.x = Random.Range (-1f,1f);
			tempCoords.y = Random.Range (-1f,1f);
			tempCoords.z = Random.Range (-1f,1f);
			tempCoords.Normalize();
//			Debug.Log (tempCoords);
			singleAsteroid = (GameObject)Instantiate(tempAsteroid, Vector3.zero, Quaternion.identity);
			singleAsteroid.transform.parent = this.transform;
			singleAsteroid.transform.position = this.transform.position + tempCoords*distanceFromCenter;
			distanceFromCenter += 200;
			singleAsteroid.transform.localScale = new Vector3(100,100,100);
			singleAsteroid.GetComponent<AsteroidInfo>().astID = x+asteroidSeed;
		}
	}


	void FixedUpdate()
	{
		tempMovTime += Time.deltaTime;
		
		tempRotTime = tempMovTime;
		
		//tempRotTime = tempRotTime%rotationSpeed;
		//tempRotTime = tempRotTime/rotationSpeed;
		//xRot = tempRotTime * 360f;
		//yRot = tempRotTime * 360f;
		
		
		
		tempX = starMov.trueX + (double)distanceFromSun * System.Math.Sin ((double)timeOffset);
		tempY = starMov.trueY + (double)distanceFromSun * System.Math.Cos ((double)timeOffset);
		tempZ = starMov.trueZ;
		newXPosition = (float)tempX;
		newYPosition = (float)tempY;
		newZPosition = (float)tempZ;
		
		//newFullRotation = new Vector3(xRot,0f,0f);
		
		//transform.eulerAngles = newFullRotation;
		
		newFullPosition = new Vector3(newXPosition,newYPosition,newZPosition);
		
		deltaPosition = newFullPosition-transform.position;
		
		transform.Translate(deltaPosition,Space.World);
		newDelta += Time.deltaTime;
		tempVelocity += deltaPosition.magnitude;
		if(newDelta > 1f)
		{
			displayVelocity = tempVelocity;
			newDelta = 0f;
			tempVelocity = 0f;
		}

	}


	// Update is called once per frame
	void Update () {
	
	}
}
