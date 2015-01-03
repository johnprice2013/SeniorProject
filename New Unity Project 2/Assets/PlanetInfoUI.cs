using UnityEngine;
using System.Collections;

public class PlanetInfoUI : MonoBehaviour {
	public bool fifthUpdate = false;
	public int numberOfUpdates = 0;
	public GameObject planetButton;
	int localButtonHeight = 3;
	public int numberOfBodies = 0;
	public GameObject[] orbitals;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

			
	
	}

	void FixedUpdate()
	{
		if(numberOfUpdates < 5)
		{
			numberOfUpdates++;
		}
		if(numberOfUpdates == 5)
		{
			int tempNum = 0;
			foreach(var planet in GameObject.FindGameObjectsWithTag("planet"))
			{
				GameObject currentButton = (GameObject)Instantiate(planetButton);
				currentButton.transform.parent = this.gameObject.transform;
				currentButton.transform.localScale =  new Vector3(1,1,1);
				currentButton.transform.localPosition = new Vector3(100,localButtonHeight*30,0);
				localButtonHeight--;
				currentButton.transform.localEulerAngles = Vector3.zero;
				currentButton.GetComponent<PlanetButtonInfo>().planet = planet;
				numberOfBodies++;
			}
			foreach(var planet in GameObject.FindGameObjectsWithTag("Asteroid"))
			{
				GameObject currentButton = (GameObject)Instantiate(planetButton);
				currentButton.transform.parent = this.gameObject.transform;
				currentButton.transform.localScale =  new Vector3(1,1,1);
				currentButton.transform.localPosition = new Vector3(100,localButtonHeight*30,0);
				localButtonHeight--;
				currentButton.transform.localEulerAngles = Vector3.zero;
				currentButton.GetComponent<PlanetButtonInfo>().planet = planet;
				numberOfBodies++;
			}
			orbitals = new GameObject[numberOfBodies];
			foreach(var planet in GameObject.FindGameObjectsWithTag("planet"))
			{
				orbitals[tempNum] = planet;
				tempNum++;
			}
			foreach(var planet in GameObject.FindGameObjectsWithTag("Asteroid"))
			{
				orbitals[tempNum] = planet;
				tempNum++;
			}

			numberOfUpdates++;
		}
	}

	public void resetButtons()
	{
		foreach(Transform button in transform)
		{
			if(button.name == "PlanetButton(Clone)")
			{
				Destroy(button.gameObject);
			}
		}
		numberOfBodies = 0;
		numberOfUpdates = 0;
		localButtonHeight = 3;
	}

}
