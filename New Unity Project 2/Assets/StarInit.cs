using UnityEngine;
using System.Collections;

public class StarInit : MonoBehaviour {

	public GameObject secCenter;
	public int starSeed = 0;
	public PlanetGenerator planetGen;
	public StarGenerator starGen;
	// Use this for initialization
	void Start () {

		secCenter = GameObject.FindGameObjectWithTag("sectorCenter");
		starSeed = secCenter.GetComponent<SectorGenerator>().newIntRandomNumber;
		starGen = secCenter.GetComponent<StarGenerator>();
		planetGen = GetComponent<PlanetGenerator>();
		if(starGen.full)
		{
		planetGen.createPlanets(starSeed);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
