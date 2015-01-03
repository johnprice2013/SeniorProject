using UnityEngine;
using System.Collections;

public class InitializeGhostStation : MonoBehaviour {


	public int stationSeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(int seed)
	{
		stationSeed = seed;

	}
}
