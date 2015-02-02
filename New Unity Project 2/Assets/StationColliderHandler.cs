using UnityEngine;
using System.Collections;

public class StationColliderHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.name == "Capsule")
		{
			foreach(Transform thing in transform)
			{
				//Debug.Log ("found a thing");
				if(thing.childCount != 0)
				{
					foreach(Transform thing2 in thing.transform)
					{
						if(thing2.childCount != 0)
						{
							foreach(Transform thing3 in thing.transform)
							{
								thing3.GetComponent<BoxCollider>().enabled = true;
								
							}
						}
						thing2.GetComponent<BoxCollider>().enabled = true;

					}
				}
				thing.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void OnTriggerExit(Collider collider)
	{
		if(collider.transform.name == "Capsule")
		{
			foreach(Transform thing in transform)
			{
				//Debug.Log ("found a thing");
				if(thing.childCount != 0)
				{
					foreach(Transform thing2 in thing.transform)
					{
						if(thing2.childCount != 0)
						{
							foreach(Transform thing3 in thing.transform)
							{
								thing3.GetComponent<BoxCollider>().enabled = false;
								
							}
						}
						thing2.GetComponent<BoxCollider>().enabled = false;
						
					}
				}
				thing.GetComponent<BoxCollider>().enabled = false;
			}
		}

	}

}
