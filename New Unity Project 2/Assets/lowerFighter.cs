using UnityEngine;
using System.Collections;

public class lowerFighter : MonoBehaviour {
	
	public Vector3 startPosition;
	public Vector3 endPosition;
	public bool lowered = false;
	public bool lowering = false;
	public bool raising = false;
	public float elapsedTime = 0f;
	public Vector3 alignedRotation = Vector3.zero;
	public Vector3 preAlignedRotation;
	public Vector3 midRotation = Vector3.zero;
	public Vector3 alignedPosition;
	public bool aligning = false;
	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		endPosition = transform.localPosition - new Vector3(0f,10f,0f);
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
		else if(aligning)
		{
			elapsedTime += Time.deltaTime;
			doAlign ();
		}
		else
		{
			elapsedTime = 0f;
		}
	}
	
	public void lower()
	{
		transform.localPosition = Vector3.Lerp (startPosition, endPosition, elapsedTime/5);
	}
	
	public void raise()
	{
		transform.localPosition = Vector3.Lerp (endPosition, startPosition, elapsedTime/5);
	}

	public void align()
	{
		alignedPosition = transform.localPosition;
		preAlignedRotation = transform.localRotation.eulerAngles;

	}

	public void doAlign()
	{
		transform.localPosition = Vector3.Lerp (alignedPosition, endPosition, elapsedTime/3f);
		transform.localEulerAngles = Vector3.Lerp (preAlignedRotation, alignedRotation, elapsedTime/3f);
	}
}