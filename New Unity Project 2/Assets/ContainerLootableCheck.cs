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
	public bool waiting = false;
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
//			if(hit.collider.transform != null)
//			{
				if(hit.collider.transform.parent.transform.parent.name == this.name)
				{
//					Debug.Log ("looking at " + this.name);
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
					if(Input.GetKey (KeyCode.E) && inMenu == false && waiting == false)
					{
					player.GetComponent<PlayerState>().lockCameras();
					player.GetComponent<PlayerState>().lockRotation();
						StartCoroutine(menuWait());
						realGUI.SetActive(true);
					transform.FindChild("OpenContainerAudio").GetComponent<AudioSource>().Play();


						StartCoroutine(realGUI.GetComponent<ContainerGUIBehavior>().waitAndUpdate());
						//realGUI.GetComponent<ContainerGUIBehavior>().UpdateContainerButtons();
					//realGUI.GetComponent<ContainerGUIBehavior>().UpdateContainerButtons();
						inMenu = true;
					}
					else if(Input.GetKey(KeyCode.E) && inMenu == true && waiting == false)
					{
					player.GetComponent<PlayerState>().unlockCameras();
					player.GetComponent<PlayerState>().unlockRotation();
						StartCoroutine(menuWait());
					transform.FindChild("CloseContainerAudio").GetComponent<AudioSource>().Play();

							realGUI.GetComponent<ContainerGUIBehavior>().shutDownButtons();
					//StartCoroutine(realGUI.GetComponent<ContainerGUIBehavior>().deletePlayerButtons());
					//realGUI.SetActive(false);
						inMenu = false;


					}
						if(Input.GetKey(KeyCode.E) && inMenu == true)
					{
//						Debug.Log("in menu");
					}
				//}
			}
			else
			{
				if(Input.GetKey(KeyCode.E) && inMenu == true && waiting == false)
				{
					StartCoroutine(menuWait());
					realGUI.GetComponent<ContainerGUIBehavior>().shutDownButtons();
					inMenu = false;
				}
				realPrompt.SetActive(false);
			}
		}
		else if(inMenu == true)
		{
			realGUI.SetActive (false);
			player.GetComponent<PlayerState>().unlockCameras();
			player.GetComponent<PlayerState>().unlockRotation();
			inMenu = false;
		}
		else
		{
			realPrompt.SetActive(false);
		}
	}

	public IEnumerator menuWait()
	{
		waiting = true;
		yield return new WaitForSeconds(.2f);
		waiting = false;
	}


}
