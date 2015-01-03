using UnityEngine;
using System.Collections;

public class LaserMovement : MonoBehaviour {

	public float laserSpeed;
	//public bool active;
	// Use this for initialization
	void Start () {
		laserSpeed = 30f;
		StartCoroutine(killCountdown());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{

		transform.Translate(transform.forward * laserSpeed * Time.deltaTime, Space.World);

	}

	public IEnumerator killCountdown()
	{
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}



}
