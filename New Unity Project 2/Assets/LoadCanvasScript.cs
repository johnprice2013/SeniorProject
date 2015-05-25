using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;

public class LoadCanvasScript : MonoBehaviour {
	public GameObject button;
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
		DirectoryInfo dir = new DirectoryInfo(".");
		FileInfo[] info = dir.GetFiles("*.*");
		int localPos = 0;
		foreach (FileInfo f in info) 
		{

			if(f.FullName.Contains("_save"))
			{
				GameObject quickButton = (GameObject)Instantiate(button);
				quickButton.transform.parent = this.transform.FindChild("Panel");
				quickButton.transform.localPosition = new Vector2(0, localPos * -35 + 50);
				localPos++;
				Debug.Log (quickButton.transform.localPosition);
				quickButton.GetComponentInChildren<Text>().text = f.Name.Remove(f.Name.Length-5);
				string quickString = f.Name;
				quickButton.GetComponent<Button>().onClick.AddListener(() => {this.loadGame(quickString);});
				//quickButton.GetComponent<Button>().GetComponent<Text>().text = f.FullName;
			}
		}
		
	}
	public void loadGame(string fileName)
	{
		StartCoroutine(GameObject.Find ("Saver").GetComponent<SaveGame>().FetchAndLoadData(fileName));
	}
	public void returnToMain()
	{
		Instantiate(startCanvas);
		Destroy(this.gameObject);
	}
}
