using UnityEngine;
using System.Collections;

public class StationDoorScript : MonoBehaviour {

	public Vector3 closedPosition;
	public Vector3 openPosition;
	public float elapsedTime = 0f;
	public bool opening = false;
	public bool open = false;
	public bool closing = false;
	public int timeToTake = 1;
	public float distanceToPlayer;
	public GameObject doorAudio;
	public GameObject realDoor;
	// Use this for initialization
	void Start () {
		realDoor = (GameObject) Instantiate(doorAudio);
		realDoor.transform.parent = this.transform;
		realDoor.transform.localPosition = Vector3.zero;
		closedPosition = transform.localPosition;
		openPosition = closedPosition + new Vector3(0f,3f,0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if(opening)
		{
			elapsedTime += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (closedPosition, openPosition, elapsedTime/timeToTake);
		}
		else if(closing)
		{
			elapsedTime += Time.deltaTime;
			transform.localPosition = Vector3.Lerp (openPosition, closedPosition, elapsedTime/timeToTake);
		}
		else
		{
			elapsedTime = 0f;
		}
		if(open)
		{
			distanceToPlayer = (this.transform.position - GameObject.Find ("Capsule").transform.position).magnitude;
		}
		else
		{
			distanceToPlayer = 100f;
		}
		if(distanceToPlayer > 5 && open)
		{
			StartCoroutine(doorCloseTimer());
		}

	}

	public void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.name == "Capsule")
		{

			//Debug.Log ("Open the door");
			StartCoroutine(doorOpenTimer());
		}
	}

	public IEnumerator doorOpenTimer()
	{
		realDoor.GetComponent<AudioSource>().Play();
		opening = true;
		yield return new WaitForSeconds(timeToTake);
		open = true;
		opening = false;
	}

	public void OnTriggerExit(Collider collider)
	{
		//if(collider.transform.name == "Capsule")
		//{
		//	StartCoroutine(doorCloseTimer());
		//	Debug.Log ("Close door");
		//}
	}

	public IEnumerator doorCloseTimer()
	{
		realDoor.GetComponent<AudioSource>().Play();

		closing = true;
		open = false;
		yield return new WaitForSeconds(timeToTake);
		open = false;
		closing = false;
	}
}
