using UnityEngine;
using System.Collections;

public class SectorGenerator : MonoBehaviour {
	public GameObject player;
	public Initialize info;
	public int seed = 0;
	public int sectorX;
	public int sectorY;
	public int sectorZ;
	public bool hasStar;
	public StarGenerator starGen;
	public int newIntRandomNumber;


	// Use this for initialization
	void Start () {
		createSector ();
	}
	 public void createSector()
	{
		player = GameObject.FindGameObjectWithTag("PlayerBody");
		info = player.GetComponent<Initialize>();
		seed = info.seed;
		sectorX = info.sectorX;
		sectorY = info.sectorY;
		sectorZ = info.sectorZ;

		float newRandomNumber = seed + sectorX * 100000 + sectorY * 1000 + sectorZ;
		newIntRandomNumber = (int)(newRandomNumber % 2000000000);
		
		
		
		Random.seed = newIntRandomNumber;

		int newSeed = (int)Random.Range (0f,8000f);

		hasStar = false;

		if (newSeed < 5000)
		{
			hasStar = true;
		}


		if(hasStar)
		{
			starGen.Generate(newIntRandomNumber,sectorX,sectorY,sectorZ);
		}
		else
		{
			starGen.GenerateBlank(newIntRandomNumber,sectorX,sectorY,sectorZ);
		}
		
		

	}

	public void OnDestroy()
	{

	}
	// Update is called once per frame
	void Update () {
	
	}
}
