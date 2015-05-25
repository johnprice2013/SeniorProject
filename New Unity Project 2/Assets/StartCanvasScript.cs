using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class StartCanvasScript : MonoBehaviour {

	//public GameObject startCanvas;
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

		GameObject button = GameObject.Find ("NewButton");
		button.GetComponent<Button>().onClick.AddListener(() => {this.newGame();});
		GameObject button2 = GameObject.Find ("LoadButton");
		button2.GetComponent<Button>().onClick.AddListener(() => {this.loadGame();});
		GameObject button3 = GameObject.Find("QuitButton");
		button3.GetComponent<Button>().onClick.AddListener(() => {this.quitGame();});


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void newGame()
	{
		Instantiate(newCanvas);
		Destroy(this.gameObject);
		//Destroy (
	}

	public void loadGame()
	{
		Instantiate(loadCanvas);
		Destroy(this.gameObject);
	}

	public void quitGame()
	{
		Application.Quit();
	}
}

