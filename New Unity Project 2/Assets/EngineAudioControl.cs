using UnityEngine;
using System.Collections;

public class EngineAudioControl : MonoBehaviour {

	public movement shipMov;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FixedUpdate()
	{
		if(shipMov != null)
		{
			this.GetComponent<AudioSource>().pitch = (float)(shipMov.totalVelocity/5000f + .3f);
			if(this.GetComponent<AudioSource>().pitch > 5f)
			{
				this.GetComponent<AudioSource>().pitch = 5f;
			}
		}
	}
}
