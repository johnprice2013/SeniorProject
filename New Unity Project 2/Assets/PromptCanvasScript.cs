using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PromptCanvasScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		setToClear ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setToE()
	{
		transform.FindChild("Panel").transform.FindChild("Text").GetComponent<Text>().text = "Press E to interact";
	}

	public void setToT()
	{
		transform.FindChild("Panel").transform.FindChild("Text").GetComponent<Text>().text = "Press T to interact";
	}

	public void setToR()
	{
		transform.FindChild("Panel").transform.FindChild("Text").GetComponent<Text>().text = "Press R to interact";
	}

	public void setToClear()
	{
		transform.FindChild("Panel").transform.FindChild("Text").GetComponent<Text>().text = "";
	}
}
