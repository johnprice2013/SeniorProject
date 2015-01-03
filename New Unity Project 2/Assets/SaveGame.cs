using UnityEngine;
using System.Collections;
using System.IO;

public class SaveGame : MonoBehaviour {

	public GameObject playerShip;
	public Vector3 playerShipLocation;
	public GameObject star;
	public Vector3 shipRotation;
	public GameObject player;
	public Vector3 starMovement;
	public bool saveWait = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKey(KeyCode.U) && saveWait == false)
		{
			StartCoroutine(FetchAndSaveData());
		}
	if(Input.GetKey (KeyCode.Y) && saveWait == false)
		{
			StartCoroutine(FetchAndReadData());

		}
	}

	public IEnumerator FetchAndSaveData()
	{
		saveWait = true;
		Debug.Log ("Writing to file");
		if(File.Exists ("SaveGame"))
		   {
			File.Delete ("SaveGame");
		}
		var SaveFile = File.CreateText ("SaveGame");
		SaveFile.WriteLine("test, please ignore");
		SaveFile.Close();
		yield return new WaitForSeconds(2f);
		Debug.Log ("Wrote to file");
	}

	public IEnumerator FetchAndReadData()
	{
		saveWait = true;
		Debug.Log ("Reading from file");
		if(File.Exists ("SaveGame"))
		{
			var SaveFile = File.OpenText ("SaveGame");
			string fileText = SaveFile.ReadToEnd();
			Debug.Log (fileText);
			SaveFile.Close ();
			Debug.Log ("Read from file");
		}
		yield return new WaitForSeconds(2f);

	}
}
