using UnityEngine;
using System.Collections;

public class FighterMovement : MonoBehaviour {

	public FighterController fightControl;
	public Vector3 movement = Vector3.zero;
	public float speed = .25f;
	public GameObject Laser;
	public float shieldStrength;
	// Use this for initialization
	void Start () {
		fightControl = GetComponent<FighterController>();
		speed = .25f;
		shieldStrength = 100f;
	}
	
	// Update is called once per frame
	void Update () {
	if(fightControl.piloting == true)
		{
			if(Input.GetKey(KeyCode.W))
			{
				movement += transform.forward * speed;
			}
			if(Input.GetKey(KeyCode.A))
			{
				movement += -transform.right * speed;
			}
			if(Input.GetKey(KeyCode.S))
			{
				movement += -transform.forward * speed;
			}
			if(Input.GetKey(KeyCode.D))
			{
				movement += transform.right * speed;
			}
			if(Input.GetKey(KeyCode.F))
			{
				movement = Vector3.zero;
			}
			transform.Translate (movement * Time.deltaTime, Space.World);
			if(Input.GetMouseButton(0))
			   {
				Instantiate(Laser,transform.position, transform.rotation);
			}


		}
		else
		{
			movement = Vector3.zero;
		}


	}

	void OnCollisionEnter(Collision hitCollision)
	{
		string hitName = hitCollision.gameObject.name;
		if(hitName == "EnemyLaserShot")
		{
			Debug.Log ("got shot");
		}
	}

	void OnCollisionExit(Collision hitCollision)
	{
		string hitName = hitCollision.gameObject.name;
	}
}
