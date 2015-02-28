using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {
	public int roomID;
	public int oddsToSpawn = 1;
	public GameObject monsterToCreate;

	// Use this for initialization
	void Start () {
		roomID = this.transform.parent.transform.parent.GetComponent<ASRoomBuilder>().roomID;
		Random.seed = roomID;
		int spawnCheck = Random.Range(0,oddsToSpawn);
		if(spawnCheck == 0)
		{
			GameObject monster = (GameObject) Instantiate (monsterToCreate, this.transform.position, Quaternion.identity);
			monster.transform.parent = this.transform;
			//Debug.Log ("Spawned Monster");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
