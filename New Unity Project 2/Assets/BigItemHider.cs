using UnityEngine;
using System.Collections;

public class BigItemHider : MonoBehaviour {



	public bool hidden = false;
	public bool visible = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		if(transform.position.magnitude > 10000000 && hidden == false)
		{
			StartCoroutine(disableRenderer(this.transform));
			hidden = true;
			visible = false;
		}
		else if(transform.position.magnitude < 10000000 && hidden == true)
		{
			StartCoroutine(enableRenderer (this.transform));
			hidden = false;
			visible = true;
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
