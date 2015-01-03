using UnityEngine;
using System.Collections;

public class InitializeGhostSector : MonoBehaviour {


	public int seed;
	public float sectorX;
	public float sectorY;
	public float sectorZ;
	public Initialize info;
	public bool hasStar = false;
	public GameObject ghostStar;
	public GameObject tempGhostStar;
	public GameObject player;
	public int newIntRandomNumber = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(Vector3 coords)
	{
		player = GameObject.FindGameObjectWithTag("PlayerBody");
		info = player.GetComponent<Initialize>();
		seed = info.seed;
		sectorX = coords.x;
		sectorY = coords.y;
		sectorZ = coords.z;

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
			GameObject tempGhostStar =  (GameObject) Instantiate(ghostStar);
			tempGhostStar.GetComponent<InitializeGhostStar>().Initialize(newIntRandomNumber,(int)sectorX, (int)sectorY, (int)sectorZ);
			tempGhostStar.transform.parent = this.gameObject.transform;
		}

	}
}
