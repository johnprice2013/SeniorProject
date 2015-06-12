using UnityEngine;
using System.Collections;

public class ShipDoorOpen : MonoBehaviour {

	public GameObject player;
	public GameObject ship;
	public ShipControl shipCon;
	public bool docking;
	public float distanceToPlayer;
	public bool moving = false;
	public bool open = false;
	public bool lowering = false;
	public bool raising = false;
	public float timeElapsed;
	public Vector3 startPosition;
	public Vector3 endPosition;
	public bool docked = false;
	public GameObject doorAudio;
	public GameObject realDoor;
	// Use this for initialization
	void Start () {
		realDoor = (GameObject) Instantiate(doorAudio);
		realDoor.transform.parent = this.transform;
		realDoor.transform.localPosition = Vector3.zero;
		player = GameObject.FindGameObjectWithTag("PlayerBody");
		ship = GameObject.FindGameObjectWithTag("Player");
		shipCon = ship.GetComponent<ShipControl>();
		startPosition = this.transform.localPosition;
		endPosition = startPosition - new Vector3(0f, -3f, 0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		docking = shipCon.docking;
		docked = shipCon.docked;
		distanceToPlayer = (this.transform.position - player.transform.position).magnitude;
		if(docked && distanceToPlayer < 1.5f && !moving && !open)// && Input.GetKey(KeyCode.E))
		{
//			Debug.Log ("opening door");
			StartCoroutine(openDoor());
			//Debug.Log ("close enough to open");
		}
		else if(docked && distanceToPlayer > 4.5f && !moving && open)// && Input.GetKey(KeyCode.E))
		{
//			Debug.Log ("closing door");
			StartCoroutine(closeDoor());
		}
		if(lowering)
		{
			timeElapsed += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (endPosition, startPosition, timeElapsed);
		}
		else if(raising)
		{
			timeElapsed += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (startPosition, endPosition, timeElapsed);
		}
		else
		{
			timeElapsed = 0f;
		}
	}




	public IEnumerator openDoor()
	{
		raising = true;
		moving = true;
		realDoor.GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(2f);
		raising = false;
		moving = false;
			open = true;
			
	}

	public IEnumerator closeDoor()
	{
		lowering = true;
		moving = true;
		realDoor.GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(2f);
		lowering = false;
		moving = false;
			
			open = false;
	}

}
