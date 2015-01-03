using UnityEngine;
using System.Collections;

public class CurrencyDisplay : MonoBehaviour {

	public GUIText myGUI;
	public GameObject player;
	public Initialize playerInit;
	// Use this for initialization
	void Start () {
		myGUI = GetComponent<GUIText>();
		player = GameObject.FindGameObjectWithTag("PlayerBody");
		playerInit = player.GetComponent<Initialize>();
		myGUI.transform.position = new Vector3(.5f,.9f,0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		myGUI.text = "Current available funds: " + playerInit.currency;
	}

}
