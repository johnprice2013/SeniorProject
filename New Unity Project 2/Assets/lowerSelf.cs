using UnityEngine;
using System.Collections;

public class lowerSelf : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public Vector3 openPosition;
	public Vector3 openScale;
	public Vector3 startScale;
	public Vector3 endScale;
	public bool lowered = false;
	public bool lowering = false;
	public bool raising = false;
	public bool opening = false;
	public bool closing = false;
	public float elapsedTime = 0f;
	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		endPosition = transform.localPosition - new Vector3(0f,5f,0f);
		openPosition = transform.localPosition - new Vector3(0f, 2.5f, 0f);
		startScale = transform.localScale;
		openScale = transform.localScale + new Vector3(0f,5f,0f);
		endScale = transform.localScale + new Vector3(0f,10f,0f);
	}

	void Update()
	{

	}
	// Update is called once per frame
	void FixedUpdate () {
	if(lowering)
		{
			elapsedTime += Time.deltaTime;
			lower();
		}
		else if(raising)
		{
			elapsedTime += Time.deltaTime;
			raise();
		}
		else if(opening)
		{
			elapsedTime += Time.deltaTime;
			open();
		}
		else if(closing)
		{
			elapsedTime += Time.deltaTime;
			close();
		}
		else
		{
			elapsedTime = 0f;
		}
	}

	public void lower()
	{
		transform.localPosition = Vector3.Lerp (startPosition, endPosition, elapsedTime/5);
		transform.localScale = Vector3.Lerp (startScale, endScale, elapsedTime/5);
	}

	public void raise()
	{
		transform.localPosition = Vector3.Lerp (endPosition, startPosition, elapsedTime/5);
		transform.localScale = Vector3.Lerp (endScale, startScale, elapsedTime/5);
	}

	public void open()
	{
		transform.localPosition = Vector3.Lerp (endPosition, openPosition, elapsedTime/3);
		transform.localScale = Vector3.Lerp (endScale, openScale, elapsedTime/3);
	}

	public void close()
	{
		transform.localPosition = Vector3.Lerp (openPosition, endPosition, elapsedTime/3);
		transform.localScale = Vector3.Lerp (openScale, endScale, elapsedTime/3);
	}
}
