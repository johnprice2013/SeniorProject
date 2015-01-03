using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {

	public Transform Star;
	public bool working = false;
	public PlanetGenerator planetGen;
	public bool full = false;
	// Use this for initialization
	void Start () {
	
	}

	public void Generate(int seed, int x, int y, int z)
	{
		full = true;
		Instantiate(Star,new Vector3(0f, 0f,0f), Quaternion.identity);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject star = GameObject.FindGameObjectWithTag("star");

		movement starMov = star.GetComponent<movement>();
		SwitchSector secSwitch = player.GetComponent<SwitchSector>();
		starMov.localXAccel = secSwitch.savedX;
		starMov.localYAccel = secSwitch.savedY;
		starMov.localZAccel = secSwitch.savedZ;

		working = true;
	

	}

	public void GenerateBlank(int seed, int x, int y, int z)
	{
		full = false;
		Instantiate(Star, Vector3.zero, Quaternion.identity);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject star = GameObject.FindGameObjectWithTag("star");
		movement starMov = star.GetComponent<movement>();
		SwitchSector secSwitch = player.GetComponent<SwitchSector>();
	
		starMov.localXAccel = secSwitch.savedX;
		starMov.localYAccel = secSwitch.savedY;
		starMov.localZAccel = secSwitch.savedZ;
		working = true;
		star.transform.localScale = Vector3.zero;
	
	}

	void Update () {
	
	}

}
