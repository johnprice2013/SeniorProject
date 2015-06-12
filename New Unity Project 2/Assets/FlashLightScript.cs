using UnityEngine;
using System.Collections;

public class FlashLightScript : MonoBehaviour {
	public Vector3 hitPoint;
	public bool on = true;
	public Light theLight;
	public bool paused = false;
	// Use this for initialization
	void Start () {
		theLight = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void FixedUpdate()
	{
		if(on)
		{
			RaycastHit hit;
			Physics.Raycast(transform.position, transform.forward, out hit, 30);
			hitPoint = hit.point;
				//Debug.Log (hit.point);

		}
		else
		{
			hitPoint = new Vector3(0f,0f,0f);
		}

		if(on && Input.GetKey(KeyCode.Tab) && paused == false)
		{
			theLight.enabled = false;
			on = false;
			StartCoroutine(lightWait());

		}
		else if(!on && Input.GetKey(KeyCode.Tab) && paused == false)
		{
			theLight.enabled = true;
			on = true;
			StartCoroutine(lightWait());
		}


	}


	public IEnumerator lightWait()
	{
		paused = true;
		yield return new WaitForSeconds(.2f);
		paused = false;

	}
}
