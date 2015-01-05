using UnityEngine;
using System.Collections;

public class movementmaker : MonoBehaviour 
{

	public float transx;
	public float transy;
	public float transz;
	public float speedchecker;
	public float speedfixer;
	public float topspeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(Input.GetKey(KeyCode.LeftShift))
		{
			topspeed = .4f;

		}
		else
			topspeed = .25f;
		if(Input.GetKey (KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			transz+=.02f;
			if(transz > topspeed)
			{
				transz = topspeed;

			}

		}
		else
		{
			if(transz > 0 && !Input.GetKey (KeyCode.S))
			{
				transz -= .05f;
				if(transz < 0)
				{
					transz = 0;

				}

			}
		}

		if(Input.GetKey (KeyCode.S)&& !Input.GetKey(KeyCode.W))
		{
			transz-=.02f;
			if(transz < -topspeed)
			{
				transz = -topspeed;
				
			}
			
		}
		else
		{
			if(transz < 0 && !Input.GetKey (KeyCode.W))
			{
				transz += .05f;
				if(transz > 0)
				{
					transz = 0;
					
				}
				
			}
		}

		if(Input.GetKey (KeyCode.A)&& !Input.GetKey(KeyCode.D))
		{
			transx-=.02f;
			if(transx < -topspeed)
			{
				transx = -topspeed;
				
			}
			
		}
		else
		{
			if(transx < 0 && !Input.GetKey (KeyCode.D))
			{
				transx += .05f;
				if(transx > 0)
				{
					transx = 0;
					
				}
				
			}
		}
		if(Input.GetKey (KeyCode.D)&& !Input.GetKey(KeyCode.A))
		{
			transx+=.02f;
			if(transx > topspeed)
			{
				transx = topspeed;
				
			}
			
		}
		else
		{
			if(transx > 0 && !Input.GetKey (KeyCode.A))
			{
				transx -= .05f;
				if(transx < 0)
				{
					transx = 0;
					
				}
				
			}
		}
		speedchecker = (transx*transx + transz*transz);
		speedchecker = Mathf.Sqrt(speedchecker);
		if(speedchecker > topspeed)
		{
			speedfixer = topspeed/speedchecker;

		}
		else
			speedfixer = 1;

		transform.Translate(new Vector3(transx*speedfixer,0,transz*speedfixer));


	}
}
