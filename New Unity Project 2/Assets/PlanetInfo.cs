using UnityEngine;
using System.Collections;

public class PlanetInfo : MonoBehaviour {
	public Vector3 planetScale = Vector3.zero;
	public int planetSeed = 0;
	public bool hasStation = false;
	public string planetName;
	public Vector3 planetSector = Vector3.zero;
	public Color randomColor = Color.green;
	public GameObject water;


	//vars from planetMovement
	public float G = 0f;
	public GameObject star;
	public float distanceFromSun;
	public GameObject center;
	public PlanetGenerator planetGen;
	//public float distanceFromStar;
	public int planetNum = 0;
	public int seed = 0;
	public float timeOffset = 0f;
	public float neededRotation = 0f;
	public float neededVelocity = 0f;
	public float tempMoveTime = 0f;
	public int rotationSpeed = 0;
	public float planetRadius = 0f;
	public float planetMass = 0f;
	public float starMass = 0f;
	public movement starMov;
	public Vector3 starPosition = Vector3.zero;
	public double tempX = 0d;// = (double)starPosition.x + (double)distanceFromSun * System.Math.Sin ((double)timeOffset);
	public double tempY = 0d;//(double)starPosition.y + (double)distanceFromSun * System.Math.Cos ((double)timeOffset);
	public double tempZ = 0d;//(double)starPosition.z;
	public float newXPosition = 0f;//(float)tempX;
	public float newYPosition = 0f;//(float)tempY;
	public float newZPosition = 0f;//(float)tvectrempZ;
	public Vector3 newFullPosition = Vector3.zero;//new Vector3(newXPosition,newYPosition,newZPosition);
	
	public Vector3 deltaPosition = Vector3.zero;//newFullPosition-transform.position;

	public OrbitalGenerator orbGen;

	public bool hasAbandonedStation = false;


	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName == "StartingScene")
		{
			initializePlanet2();
		}
		else
		{
		initializePlanet();
		}
//		Debug.Log ("made it");
		setDetails();
//		Debug.Log("made it 2");
		setColors();
//		Debug.Log("made it 3");
		if(Application.loadedLevelName != "StartingScene")
		{
			orbGen = GetComponent<OrbitalGenerator>();
			createOrbitingBodies(seed);
		}
	}


	public void createOrbitingBodies(int passedSeed)
	{
		orbGen.initialize(passedSeed);

		if(orbGen.planetStation != null)
		{
//			Debug.Log ("errorHere");
			orbGen.planetStation.GetComponent<StationData>().stationSector = planetSector;
		//	Debug.Log ("Yeah, here");
		}

	}

	public void initializePlanet2()
	{
		
//		G = 50f;
//		star = GameObject.FindGameObjectWithTag("star");
//		Vector3 temp = transform.position - star.transform.position;
//		distanceFromSun = temp.magnitude;
//		center = GameObject.FindGameObjectWithTag("sectorCenter");
//		planetGen = star.GetComponent<PlanetGenerator>();
		//distanceFromStar = (transform.position - star.transform.position).magnitude;
//		planetNum = (int)distanceFromSun/10000000;
		seed = Random.Range (0,int.MaxValue);
		Random.seed = seed;
		this.GetComponent<NewMeshEditor>().seed = seed;
		timeOffset = Random.Range (0f,10f);
		rotationSpeed = Random.Range (100,1000);
		planetRadius = 100000;
		planetMass = Mathf.Pow((planetRadius/100f),2f);
	//	starMass = star.GetComponent<movement>().starMass;
		transform.localScale = new Vector3(planetRadius,planetRadius,planetRadius);
//		neededVelocity = Mathf.Sqrt(G*starMass/distanceFromSun);
//		neededRotation = neededVelocity/distanceFromSun;
		//		tempMovTime = Time.time;
		
//		starMov = star.GetComponent<movement>();
		//tempMovTime += Time.deltaTime;
		
		//tempRotTime = tempMovTime;
		//totalMovTime = neededRotation * tempMovTime;
//		starPosition = star.transform.position;
//		tempX = (double)starPosition.x + (double)distanceFromSun * System.Math.Sin ((double)timeOffset);
//		tempY = (double)starPosition.y + (double)distanceFromSun * System.Math.Cos ((double)timeOffset);
//		tempZ = (double)starPosition.z;
//		newXPosition = (float)tempX;
//		newYPosition = (float)tempY;
//		newZPosition = (float)tempZ;
//		newFullPosition = new Vector3(newXPosition,newYPosition,newZPosition);
		
//		deltaPosition = newFullPosition-transform.position;
//		transform.Translate(deltaPosition,Space.World);
		
	}


	public void initializePlanet()
	{
//		Debug.Log ("initializeing planet");
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
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		this.transform.Rotate(.005f,.01f,.03f);
	}

	public void setColors()
	{
		Random.seed = planetSeed;
		water.renderer.material.color = getRandomColor();
//		Debug.Log ("setting water to " + water.renderer.material.color);
		transform.renderer.material.color = getRandomColor();
//		Debug.Log ("setting surface to " + transform.renderer.material.color);
	}

	public void setDetails()
	{
		if(Application.loadedLevelName != "StartingScene")
		{
		planetSeed = GetComponent<PlanetMovement>().seed;
		planetScale = transform.localScale;
		planetSector.x = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorX;
		planetSector.y = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorY;
		planetSector.z = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorZ;
		}
		else
		{
			planetSeed = seed;
			planetScale = transform.localScale;
		}
		water = this.transform.FindChild("Sphere").gameObject;

	}

	public Color getRandomColor()
	{
		float r = Random.Range (0f, .5f);
		float g = Random.Range (0f, .5f);
		float b = Random.Range (0f, .5f);

		randomColor.r = r;
		randomColor.g = g;
		randomColor.b = b;
		randomColor.a = Random.Range (0f,1f);
		return randomColor;
	}
}
