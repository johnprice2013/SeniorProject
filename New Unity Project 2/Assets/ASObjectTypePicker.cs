using UnityEngine;
using System.Collections;

public class ASObjectTypePicker : MonoBehaviour {
	public GameObject[] availableTypes;
	public int id;
	// Use this for initialization
	void Start () {
		id = this.transform.parent.transform.parent.GetComponent<ASRoomBuilder>().roomID + this.transform.GetSiblingIndex();
		Random.seed = id;

		int randAdd = Random.Range(0,200);
		Random.seed = randAdd;
		GameObject makeMe = availableTypes[Random.Range (0,availableTypes.Length)];
		GameObject selfObject = (GameObject) Instantiate(makeMe,Vector3.zero,Quaternion.identity);
		selfObject.transform.parent = this.transform;
		selfObject.transform.rotation = this.transform.rotation;
		selfObject.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
