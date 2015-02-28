using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContainerLootableCheck : MonoBehaviour {

	public Transform playerCamera;
	GameObject player;
	public Vector3 containerPosition;
	public Vector3 cameraPosition;
	public GameObject containerGUI;
	public GameObject realGUI;
	public bool inMenu = false;
	public GameObject prompt;
	public GameObject realPrompt;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Capsule");
		playerCamera = player.transform.FindChild("Camera");
		realGUI = (GameObject) Instantiate(containerGUI);
		realGUI.SetActive(false);
		realGUI.transform.parent = this.transform;
		realPrompt = (GameObject) Instantiate (prompt);
		realPrompt.SetActive(false);
		realPrompt.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		containerPosition = transform.position;
		cameraPosition = playerCamera.transform.position;
		float distanceToPlayer = (transform.position - playerCamera.transform.position).magnitude;
		if(distanceToPlayer < 3f)
		{
			//Debug.Log ("in range");
			RaycastHit hit;
			Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hit);
			//Debug.Log(hit.transform.name);
			if(hit.collider.transform.parent.transform.parent.name == this.name)
			{
//				Debug.Log ("looking at " + this.name);
				if(inMenu == false)
				{
				realPrompt.SetActive(true);
					realPrompt.GetComponent<PromptCanvasScript>().setToE();
				}
				else
				{
					realPrompt.SetActive(false);
				}

				//prompt.GetComponent<PromptCanvasScript>().setToE();
				//prompt.transform.FindChild("Panel").gameObject.renderer.enabled = false;
				if(Input.GetKey (KeyCode.E) && inMenu == false)
				{
					realGUI.SetActive(true);
					realGUI.GetComponent<ContainerGUIBehavior>().UpdateContainerButtons();
					realGUI.GetComponent<ContainerGUIBehavior>().UpdatePlayerInvButtons();
					inMenu = true;
				}
				else if(Input.GetKey(KeyCode.E) && inMenu == true)
				{
					realGUI.SetActive(false);
					inMenu = false;
				}
				if(Input.GetKey(KeyCode.E) && inMenu == true)
				{
					Debug.Log("in menu");
				}
			}
			else
			{
				realPrompt.SetActive(false);
			}
		}
	}
}
