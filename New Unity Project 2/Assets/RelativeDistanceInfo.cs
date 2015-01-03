using UnityEngine;
using System.Collections;

public class RelativeDistanceInfo : MonoBehaviour {

	public GameObject parent;
	public float distanceToShip;
	public GameObject playerShip;
	public bool inRange = false;
	public bool tracking = false;
	public GameObject tempEnemyShip;
	public GameObject enemyShip;
	public GameObject[] shipsList;
	public bool active = true;
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		playerShip = GameObject.FindGameObjectWithTag("Player");
		transform.localScale = new Vector3(.5f,.5f,.5f);
		transform.position =  parent.transform.position+new Vector3(500f,500f,500f);
		shipsList = new GameObject[5];
		for(int x = 0; x<5; x++)
		{
			shipsList[x] = (GameObject)Instantiate (enemyShip, transform.position + new Vector3((float)x*2, 0f, 0f), Quaternion.identity);
			shipsList[x].transform.parent = this.gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		distanceToShip = Mathf.Abs((transform.position - playerShip.transform.position).magnitude);
		if(distanceToShip < 1000f && inRange == false)
		{
			inRange = true;

			playerShip.GetComponent<ShipController>().inEnemyRange = true;
			playerShip.GetComponent<ShipController>().enemyArea = this.gameObject;

		}
		else if(inRange == true && distanceToShip > 1000f)
		{
			inRange = false;

			playerShip.GetComponent<ShipController>().inEnemyRange = false;

		}
		else{
		}

		if(inRange)
		{
			transform.position = transform.position * .99f;
		}

		if(transform.FindChild("EnemyFighter(Clone)") == null)
		{
			active = false;

			Destroy (this.gameObject);
		}

	}


}
