using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {

	public bool inControl = false;
	public Camera[] cameras;
	public bool camerasEnabled = false;
	public bool piloting = false;
	public bool platformUp = true;
	public bool platformDown = false;
	public Vector3 dockPosition = Vector3.zero;
	public float distanceToDock = 0f;
	public GameObject player;
	// Use this for initialization
	void Start () {
		cameras = GetComponentsInChildren<Camera>();
		dockPosition = GetComponent<lowerFighter>().endPosition;
		player = GameObject.FindGameObjectWithTag("PlayerBody");
	}
	
	// Update is called once per frame
	void Update () {
		if(inControl == true)
		{
			dockPosition = GetComponent<lowerFighter>().endPosition;
			distanceToDock = (transform.localPosition - dockPosition).magnitude;
			if(camerasEnabled == false)
			{
				camerasEnabled = true;
				foreach(var camera in cameras)
				{
					camera.enabled = true;
				}
			}
			if(piloting == false && platformUp == true && Input.GetKey(KeyCode.T))
			{
				transform.parent.GetComponent<DockBayController>().undockProcedure();
				platformUp = false;
				platformDown = true;
			}
			else if(piloting == false && platformUp == true && Input.GetKey(KeyCode.R))
			{
				Debug.Log ("registering eject");
			    StartCoroutine( switchToPlayer());
			}

			if(piloting == true)
			{
				GetComponent<MouseLook>().enabled = true;
			}
			else
			{
				GetComponent<MouseLook>().enabled = false;
			}

			if(piloting == true && distanceToDock < 3f && Input.GetKey(KeyCode.T))
			{
				transform.parent.GetComponent<DockBayController>().dockProcedure();
				StartCoroutine (waitFourteenThenSwitch());
			}

		}
	}

	public IEnumerator waitFourteenThenSwitch()
	{
		yield return new WaitForSeconds(14f);
		StartCoroutine(switchToPlayer());
	}

	public IEnumerator switchToPlayer()
	{
		Debug.Log ("Switching to player");
		yield return new WaitForSeconds(0f);
		platformUp = true;
		platformDown = false;
		foreach(var camera in cameras)
		{
			camera.enabled = false;
		}
		player.GetComponent<PlayerMovementInShip>().freeInShip = true;
		player.GetComponent<MeshRenderer>().renderer.enabled = true;
		player.GetComponent<MouseLook>().enabled = true;
		camerasEnabled = false;
		inControl = false;
	}

}
