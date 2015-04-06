using UnityEngine;
using System.Collections;

public class OuterDoorOpen : MonoBehaviour {
	
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
	
	// Use this for initialization
	void Start () {
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
			StartCoroutine(openDoor());
			//Debug.Log ("close enough to open");
		}
		else if(docked && distanceToPlayer > 4.5f && !moving && open)// && Input.GetKey(KeyCode.E))
		{
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
		yield return new WaitForSeconds(2f);
		raising = false;
		moving = false;
		open = true;
		
	}
	
	public IEnumerator closeDoor()
	{
		lowering = true;
		moving = true;
		yield return new WaitForSeconds(2f);
		lowering = false;
		moving = false;
		
		open = false;
	}
	
}
