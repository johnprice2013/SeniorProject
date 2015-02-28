using UnityEngine;
using System.Collections;

public class PlanetMovement : MonoBehaviour {

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
		//setVariables();
		initializePlanet();
//	//	G = 50f;
//		star = GameObject.FindGameObjectWithTag("star");
//		Vector3 temp = transform.position - star.transform.position;
//		distanceFromSun = temp.magnitude;
//		center = GameObject.FindGameObjectWithTag("sectorCenter");
//		planetGen = star.GetComponent<PlanetGenerator>();
//		distanceFromStar = (transform.position - star.transform.position).magnitude;
//		planetNum = (int)distanceFromStar/10000000;
//		seed = planetGen.seedArray[planetNum-1];
//		Random.seed = seed;
//
//		timeOffset = Random.Range (0f,10f);
//		rotationSpeed = Random.Range (100,1000);
//		planetRadius = Random.Range (50000f,200000f);
//		planetMass = Mathf.Pow((planetRadius/100f),2f);
//		starMass = star.GetComponent<movement>().starMass;
//		transform.localScale = new Vector3(planetRadius,planetRadius,planetRadius);
//		neededVelocity = Mathf.Sqrt(G*starMass/distanceFromSun);
//		neededRotation = neededVelocity/distanceFromSun;
//		tempMovTime = Time.time;
//
//
//
//
//
//
//
//		starMov = star.GetComponent<movement>();
//		tempMovTime += Time.deltaTime;
//		
//		tempRotTime = tempMovTime;
//		totalMovTime = neededRotation * tempMovTime;
//		starPosition = star.transform.position;
//		tempX = (double)starPosition.x + (double)distanceFromSun * System.Math.Sin ((double)timeOffset);
//		tempY = (double)starPosition.y + (double)distanceFromSun * System.Math.Cos ((double)timeOffset);
//		tempZ = (double)starPosition.z;
//		newXPosition = (float)tempX;
//		newYPosition = (float)tempY;
//		newZPosition = (float)tempZ;
//		newFullPosition = new Vector3(newXPosition,newYPosition,newZPosition);
//
//		deltaPosition = newFullPosition-transform.position;
//
//		transform.Translate(deltaPosition,Space.World);


	}

	public void setSeed(int settingSeed)
	{
		seed = settingSeed;
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void setVariables()
	{
		PlanetInfo planetInfo = GetComponent<PlanetInfo>();
	//	tempMoveTime = planetInfo.tempMoveTime;
		distanceFromSun = planetInfo.distanceFromSun;
		tempX = planetInfo.tempX;
		tempY = planetInfo.tempY;
		tempZ = planetInfo.tempZ;
		timeOffset = planetInfo.timeOffset;
		rotationSpeed = planetInfo.rotationSpeed;
		starMov = planetInfo.starMov;
		Debug.Log (starMov.forceNumber);

	}

	public void initializePlanet()
	{
		
		G = 50f;
		star = GameObject.FindGameObjectWithTag("star");
		Vector3 temp = transform.position - star.transform.position;
		distanceFromSun = temp.magnitude;
		center = GameObject.FindGameObjectWithTag("sectorCenter");
		planetGen = star.GetComponent<PlanetGenerator>();
		//distanceFromStar = (transform.position - star.transform.position).magnitude;
		planetNum = (int)distanceFromSun/10000000;
		seed = planetGen.seedArray[planetNum-1];
		Random.seed = seed;
		
		timeOffset = Random.Range (0f,10f);
		rotationSpeed = Random.Range (100,1000);
		planetRadius = Random.Range (50000f,200000f);
		planetMass = Mathf.Pow((planetRadius/100f),2f);
		starMass = star.GetComponent<movement>().starMass;
		transform.localScale = new Vector3(planetRadius,planetRadius,planetRadius);
		neededVelocity = Mathf.Sqrt(G*starMass/distanceFromSun);
		neededRotation = neededVelocity/distanceFromSun;
		//		tempMovTime = Time.time;
		
		starMov = star.GetComponent<movement>();
		//tempMovTime += Time.deltaTime;
		
		//tempRotTime = tempMovTime;
		//totalMovTime = neededRotation * tempMovTime;
		starPosition = star.transform.position;
		tempX = (double)starPosition.x + (double)distanceFromSun * System.Math.Sin ((double)timeOffset);
		tempY = (double)starPosition.y + (double)distanceFromSun * System.Math.Cos ((double)timeOffset);
		tempZ = (double)starPosition.z;
		newXPosition = (float)tempX;
		newYPosition = (float)tempY;
		newZPosition = (float)tempZ;
		newFullPosition = new Vector3(newXPosition,newYPosition,newZPosition);

		deltaPosition = newFullPosition-transform.position;
		transform.Translate(deltaPosition,Space.World);
		
	}



	void FixedUpdate()
	{

		tempMovTime += Time.deltaTime;
		
		tempRotTime = tempMovTime;
	
		tempRotTime = tempRotTime%rotationSpeed;
		tempRotTime = tempRotTime/rotationSpeed;
		xRot = tempRotTime * 360f;
		yRot = tempRotTime * 360f;
		
		

		tempX = starMov.trueX + (double)distanceFromSun * System.Math.Sin ((double)timeOffset);
		tempY = starMov.trueY + (double)distanceFromSun * System.Math.Cos ((double)timeOffset);
		tempZ = starMov.trueZ;
		newXPosition = (float)tempX;
		newYPosition = (float)tempY;
		newZPosition = (float)tempZ;

		newFullRotation = new Vector3(xRot,0f,0f);

		transform.eulerAngles = newFullRotation;

		newFullPosition = new Vector3(newXPosition,newYPosition,newZPosition);

		deltaPosition = newFullPosition-transform.position;

		//Debug.Log (deltaPosition);
		transform.Translate(deltaPosition,Space.World);
		newDelta += Time.deltaTime;
		tempVelocity += deltaPosition.magnitude;
		//Debug.Log ("planet" + deltaPosition);
		if(newDelta > 1f)
		{
			displayVelocity = tempVelocity;
			newDelta = 0f;
			tempVelocity = 0f;
		}

	}
}
