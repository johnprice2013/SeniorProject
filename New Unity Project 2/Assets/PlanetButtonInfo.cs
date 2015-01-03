using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlanetButtonInfo : MonoBehaviour {
	public GameObject planet;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void GettinClicked()
	{

		GameObject.Find ("PlanetTarget").GetComponent<PlanetTargetScript>().setPlanet(planet);
	}
}
