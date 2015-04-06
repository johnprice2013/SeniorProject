using UnityEngine;
using System.Collections;

public class StartingSceneScript : MonoBehaviour {
	public GameObject planet;
	// Use this for initialization
	void Start () {
		planet = GameObject.Find("Planet");
		planet.GetComponent<GravityFromPlanet>().enabled = false;
		planet.GetComponent<PlanetMovement>().enabled = false;
		planet.AddComponent<TempPlanetInit>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
