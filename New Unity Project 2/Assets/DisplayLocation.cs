using UnityEngine;
using System.Collections;


public class DisplayLocation : MonoBehaviour {

	public GUIText myGUI;
	public GameObject sectorCenter;
	public SectorGenerator mySecGen;
	// Use this for initialization
	void Start () {
		myGUI = GetComponent<GUIText>();
		sectorCenter = GameObject.FindGameObjectWithTag("sectorCenter");
		mySecGen = sectorCenter.GetComponent<SectorGenerator>();
		myGUI.transform.position = new Vector3(.05f,.9f,0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myGUI.text = mySecGen.sectorX + " " + mySecGen.sectorY + " " + mySecGen.sectorZ;
	}
}
