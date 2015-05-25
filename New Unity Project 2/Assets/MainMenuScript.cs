using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MainMenuScript : MonoBehaviour {
	public GameObject startCanvas;
	public GameObject loadCanvas;
	public GameObject newCanvas;
	public GameObject tempCanvas;
	// Use this for initialization
	void Start () {
		loadStartUp();
	//add new game button
		//add load button
		//add credits button
		//add settings button


	
		//in new game menu, add name field, generate seed based on this.



	}

	public void loadStartUp()
	{
		tempCanvas = (GameObject) Instantiate(startCanvas);
		tempCanvas.transform.parent = this.transform;
		GameObject button = GameObject.Find ("NewButton");
		button.GetComponent<Button>().onClick.AddListener(() => {this.newGame();});

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void newGame()
	{
		Destroy(tempCanvas);
		tempCanvas = (GameObject) Instantiate(newCanvas);
		//Destroy (
	}

	public void quitGame()
	{
		Application.Quit();
	}
}
