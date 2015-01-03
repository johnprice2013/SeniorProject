using UnityEngine;
using System.Collections;

public class AsteroidInfo : MonoBehaviour {

	public Color astColor;
	public int astID = 0;
	public Ore astOre;
	public int passMe = 0;


	// Use this for initialization
	void Start () {
		setAsteroidOre();
		setAsteroidColor();

	
	}

	public void setAsteroidOre()
	{
		Random.seed = astID;
		passMe = Random.Range (0,1000);
		astOre = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>().fetchOre(passMe);

	}

	public void setAsteroidColor()
	{
		transform.renderer.material.color = astOre.oreColor;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
