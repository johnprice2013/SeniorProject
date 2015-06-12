using UnityEngine;
using System.Collections;

public class NewMeshEditor : MonoBehaviour {

	float scale = .075f;
	float speed = 3f;
	bool recalculateNormals = true;
	public int seed;
	public GameObject star;
	private Vector3[] baseVertices;
	public PlanetGenerator planetGen;
	public float distanceFromStar = 0f;
	public int planetNum = 0;
	private Perlin noise;
	public GameObject center;
	public PlanetMovement planMov;



	
	void Start ()
	{

		if(Application.loadedLevelName != "StartingScene")
		{
		setVariables();

		planMov.setSeed(seed);
		}
		createTerrain();

	

	}

	public void createTerrain()
	{
		noise = new Perlin (seed);
		
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		
		if (baseVertices == null)
			baseVertices = mesh.vertices;
		
		
		var vertices = new Vector3[baseVertices.Length];
		
		float timex = speed + 1.1365143f;
		float timey = speed + 1.21688f;
		float timez = speed + 1.5564f;
		
		for (int i=0;i<vertices.Length;i++)
		{
			var vertex = baseVertices[i];
			
			vertex.x += noise.Noise(timex + vertex.x, timex + vertex.y, timex + vertex.z) * scale;
			vertex.y += noise.Noise(timey + vertex.x, timey + vertex.y, timey + vertex.z) * scale;
			vertex.z += noise.Noise(timez + vertex.x, timez + vertex.y, timez + vertex.z) * scale;
			
			vertices[i] = vertex;
		}
		
		mesh.vertices = vertices;
		
		if (recalculateNormals)
		{
			mesh.RecalculateNormals();
		}	
		
		mesh.RecalculateBounds();

	}


	public void setVariables()
	{
		star = GameObject.FindGameObjectWithTag("star");
		center = GameObject.FindGameObjectWithTag("sectorCenter");
		planetGen = star.GetComponent<PlanetGenerator>();
		
		distanceFromStar = (transform.position - star.transform.position).magnitude;
		
		planetNum = (int)distanceFromStar/10000000;
		
		seed = planetGen.seedArray[planetNum-1];
		planMov = GetComponent<PlanetMovement>();


	}


	public void updateTerrain()
	{
		noise = new Perlin (seed);

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		if (baseVertices == null)
			baseVertices = mesh.vertices;
		
		var vertices = new Vector3[baseVertices.Length];
		
		float timex = speed + 1.1365143f;
		float timey = speed + 1.21688f;
		float timez = speed + 1.5564f;
		for (int i=0;i<vertices.Length;i++)
		{
			var vertex = baseVertices[i];

			vertex.x += noise.Noise(timex + vertex.x, timex + vertex.y, timex + vertex.z) * scale;
			vertex.y += noise.Noise(timey + vertex.x, timey + vertex.y, timey + vertex.z) * scale;
			vertex.z += noise.Noise(timez + vertex.x, timez + vertex.y, timez + vertex.z) * scale;
			
			vertices[i] = vertex;
		}
		
		mesh.vertices = vertices;
		
		if (recalculateNormals)	
			mesh.RecalculateNormals();
		mesh.RecalculateBounds();

	}

	void setSeed(int givenSeed)
	{
		seed = givenSeed;
		updateTerrain();
	}

	void Update () {

	}
}