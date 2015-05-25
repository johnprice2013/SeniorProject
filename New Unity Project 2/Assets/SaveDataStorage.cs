using UnityEngine;
using System.Collections;

using System.IO;


public class SaveDataStorage : MonoBehaviour {
	public int worldSeed;
	public int sectorX;
	public int sectorY;
	public int sectorZ;
	public double starX;
	public double starY;
	public double starZ;
	public string fileName;
	public GameObject[] missions;
	public Ore[] ores;
	public Item[] items;
	public Vector3 shipRot;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void gatherData()
	{
		SectorGenerator sectorGen = GameObject.Find ("Sector Center").GetComponent<SectorGenerator>();     
		worldSeed = sectorGen.seed;
		sectorX = sectorGen.sectorX;
		sectorY = sectorGen.sectorY;
		sectorZ = sectorGen.sectorZ;
		movement starMov = GameObject.Find ("Star(Clone)").GetComponent<movement>();
		starX = starMov.trueX;
		starY = starMov.trueY;
		starZ = starMov.trueZ;



	}

	public void writeData()
	{
	}

}
