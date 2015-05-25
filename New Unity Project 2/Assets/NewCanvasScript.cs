using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class NewCanvasScript : MonoBehaviour {
	public GameObject startCanvas;
	// Use this for initialization
	void Start () {

		initializeMenu();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initializeMenu()
	{
		GameObject.Find ("BackButton").GetComponent<Button>().onClick.AddListener(() => {this.returnToMain();});
		GameObject.Find ("StartGameButton").GetComponent<Button>().onClick.AddListener(() => {this.startNewGame();});

	}

	public void startNewGame()
	{
		string text = GameObject.Find ("InputField").GetComponent<InputField>().text.text;
		int seed = text.GetHashCode();
		Debug.Log (text);
		Debug.Log (seed);
		Random.seed = seed;
		SaveDataStorage myStorage = GameObject.Find ("SaveData").GetComponent<SaveDataStorage>();
		myStorage.fileName = text + "_save";
		SaveGame saveGame = GameObject.Find ("Saver").GetComponent<SaveGame>();
		saveGame.fileName = text+"_save";
		myStorage.worldSeed = seed;
		myStorage.sectorX = Random.Range(-1000,1000);
		myStorage.sectorY = Random.Range(-1000,1000);
		myStorage.sectorZ = Random.Range(-1000,1000);

		Application.LoadLevel("asdf");
	}

	public void returnToMain()
	{
		Instantiate(startCanvas);
		Destroy(this.gameObject);
	}
}
