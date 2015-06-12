using UnityEngine;
using System.Collections;

public class HandleTriggerRenderStation : MonoBehaviour {

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
//			Debug.Log ("entering");
			foreach(Transform item in this.transform)
			{
				if(item.transform.name == "ASParentObject")
				{
					StartCoroutine(enableRenderer (item.transform));
				}
			}
		}

	}
	public void OnTriggerExit(Collider collider)
	{
		if(collider.transform.name == "Capsule")
		{
			foreach(Transform item in this.transform)
			{
				if(item.transform.name == "ASParentObject")
				{
			
				StartCoroutine(disableRenderer (item.transform));
				}
			}
		}
	}


	public IEnumerator enableRenderer(Transform item)
	{
		if(item.transform.childCount!= 0)
		{
			foreach(Transform child in item.transform)
			{
				StartCoroutine(enableRenderer(child));
			}
		}
		if(item.GetComponent<MeshRenderer>()!= null)
		{
			item.GetComponent<MeshRenderer>().renderer.enabled = true;
		}
		
		yield return new WaitForSeconds(0);

	}

	public IEnumerator disableRenderer(Transform item)
	{
		if(item.transform.childCount!= 0)
		{
			foreach(Transform child in item.transform)
			{
				StartCoroutine(disableRenderer(child));
			}
		}
		if(item.GetComponent<MeshRenderer>()!= null)
		{
			item.GetComponent<MeshRenderer>().renderer.enabled =false;
		}
		
		yield return new WaitForSeconds(0);

	}
}
