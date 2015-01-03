using UnityEngine;
using System.Collections;

public class OpenCloseRight : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public float elapsedTime = 0f;
	public bool opening = false;
	public bool closing = false;
	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		endPosition = transform.localPosition + new Vector3(-4.5f, .1f, 0f); 
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
		if(opening)
		{
			elapsedTime += Time.deltaTime;
			open();
		}
		else if (closing)
		{
			elapsedTime += Time.deltaTime;
			close();
		}
		else
		{
			elapsedTime = 0f;
		}
	}

	public void open()
	{
		transform.localPosition = Vector3.Lerp (endPosition, startPosition, elapsedTime/3);
	}

	public void close()
	{
		transform.localPosition = Vector3.Lerp (startPosition,endPosition,elapsedTime/3);
	}
}
