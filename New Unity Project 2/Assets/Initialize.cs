using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {
	
	public bool firstStart = true;
	public int seed = 100;
	public Vector3 startingPosition = new Vector3(1000f,1000f,1000f);
	public Vector3 currentSector = new Vector3(5000f, 5000f, 5000f);
	public int sectorX = 5000;
	public int sectorY = 5000;
	public int sectorZ = 5000;
	public GameObject secCenter;
	public SectorGenerator secGen;
	public bool piloting = false;
	public bool fighting = false;
	public GameObject[] missions;
	public int currency = 0;

	// Use this for initialization
	void Start () {
		if(firstStart)
		{
			sectorX = (int)currentSector.x;
			sectorY = (int)currentSector.y;
			sectorZ = (int)currentSector.z;

		}
		secCenter = GameObject.FindGameObjectWithTag("sectorCenter");
		secGen = secCenter.GetComponent<SectorGenerator>();
		missions = new GameObject[10];
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		sectorX = secGen.sectorX;
		sectorY = secGen.sectorY;
		sectorZ = secGen.sectorZ;
	}

	public void updateCurrency(int currencyChange)
	{
		currency += currencyChange;
	}
}
