using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterState : MonoBehaviour 
{
	public GameObject player;
	public Animator animator;
	public void Start()
	{
		//player = GameObject.Find ("Capsule");
	}
	public MonsterState()
	{}



	public abstract void movement();

	public abstract void setAnimBool();

	public abstract void checkNextState();

}






public class MonsterStateChasing : MonsterState
{
	float speed = 2f;
	float rotSpeed = 3f;
	public void Start()
	{
		player = GameObject.Find ("Capsule");
		animator = transform.GetComponent<Animator>();

		setAnimBool();
		//Debug.Log ("startScript");
	}
	public override void movement()
	{
		rigidbody.velocity = Vector3.zero;
		Vector3 playerPos = new Vector3 (player.transform.position.x, 0, player.transform.position.z);
		Vector3 dir = (playerPos - new Vector3(transform.position.x, 0f, transform.position.z));
		Quaternion look = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(this.transform.rotation, look, Time.deltaTime * rotSpeed);


		//float moveX = player.transform.position.x - transform.position.x;
		//float moveZ = player.transform.position.z - transform.position.z;
		//Vector3 move = new Vector3(moveX,0 ,moveZ).normalized;
		//moveX = move.x * Time.deltaTime * speed;
		//moveZ = move.z * Time.deltaTime * speed;
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		//Debug.Log ("moving");
	}

	public override void setAnimBool()
	{
		//Debug.Log ("setting anim bool");
		animator.SetBool ("chasing", true);
	}

	public override void checkNextState()
	{
		RaycastHit hit;
		Vector3 dir = (player.transform.position - this.transform.position);
		if(Physics.Raycast(this.transform.position, dir,out hit, 30))
		{
		if(hit.transform.name != "Capsule")
		{
			//Debug.Log (hit.transform.name);
			animator.SetBool("chasing", false);
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = true;
			//Debug.Log ("Chasing, couldn't see player, inv last known");
			this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateInvestigating>();
			this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;
			
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = false;
			
			this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = false;

			gameObject.GetComponent<MonsterStateInvestigating>().lastKnownPos = player.transform.position;
			Destroy(this);
		}
		else if((player.transform.position - transform.position).magnitude < 1.5)
		{
			animator.SetBool ("chasing",false);
			//Debug.Log ("chasing, close to player, attacking");
			this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateAttacking>();
			this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = false;
			
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = true;
			
			this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = false;


			Destroy(this);
		}
		}
	}

}






public class MonsterStateInvestigating : MonsterState
{
	public float speed = 1f;
	public Vector3 lastKnownPos;
	public float rotSpeed = 3f;
	GameObject light;
	public float timeSpent = 0f;

	public void Start()
	{
		player = GameObject.Find ("Capsule");
		animator = transform.GetComponent<Animator>();
		setAnimBool();
		light = GameObject.Find("FlashLight");
		//Debug.Log ("startScript");
	}
	public override void movement()
	{
		rigidbody.velocity = Vector3.zero;
		lastKnownPos = new Vector3(lastKnownPos.x, 0f, lastKnownPos.z);
		Vector3 dir = (lastKnownPos - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
		Quaternion look = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(this.transform.rotation, look, Time.deltaTime * rotSpeed);
		//float moveX = lastKnownPos.x - transform.position.x;
		//float moveZ = lastKnownPos.z - transform.position.z;
		//Vector3 move = new Vector3(moveX,0 ,moveZ).normalized;
		//moveX = move.x * Time.deltaTime * speed;
		//moveZ = move.z * Time.deltaTime * speed;
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		if(light.GetComponent<FlashLightScript>().on == true)
		{
			lastKnownPos = light.GetComponent<FlashLightScript>().hitPoint;
		}
		//transform.Rotate(0f, 1f, 0f);
		//Debug.Log ("moving");
	}
	
	public override void setAnimBool()
	{
		//Debug.Log ("setting anim bool");
		animator.SetBool ("investigating", true);
	}


	public void FixedUpdate()
	{
		timeSpent += Time.deltaTime;
	}

	public override void checkNextState()
	{
		RaycastHit hit;
		Vector3 dir = (player.transform.position - this.transform.position).normalized;
		Physics.Raycast(this.transform.position, dir,out hit, 30);
		
		if(hit.transform.name == "Capsule" && light.GetComponent<FlashLightScript>().on == true)
		{
			animator.SetBool ("investigating",false);
			Debug.Log ("investigating spotted player with light on");
			this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateChasing>();
			this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;
			
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = true;
			
			this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = false;



			Destroy(this);
		}
		else if((transform.position - lastKnownPos).magnitude < 1)
		{
			animator.SetBool ("investigating",false);
			Debug.Log ("investigating made it to search location");
			this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateSearching>();
			this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = false;
			
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = false;
			
			this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = true;

			Destroy(this);
		}
		else if(timeSpent > 20f)
		{
			animator.SetBool ("investigating",false);
			Debug.Log ("investigating timed out, searching here");
			this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateSearching>();
			this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = false;
			
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = false;
			
			this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = true;

			Destroy(this);
		}

	}
}






public class MonsterStateAttacking : MonsterState
{	
	float speed = 0f;
	
	public void Start()
	{
		player = GameObject.Find ("Capsule");
		animator = transform.GetComponent<Animator>();
		setAnimBool();
		//Debug.Log ("startScript");
	}
	public override void movement()
	{
		rigidbody.velocity = Vector3.zero;
		//float moveX = player.transform.position.x - transform.position.x;
		//float moveZ = player.transform.position.z - transform.position.z;
		//Vector3 move = new Vector3(moveX,0 ,moveZ).normalized;
		//moveX = move.x * Time.deltaTime * speed;
		//moveZ = move.z * Time.deltaTime * speed;
		//transform.Translate(moveX, 0f, moveZ);
		//Debug.Log ("moving");
	}
	
	public override void setAnimBool()
	{
		//Debug.Log ("setting anim bool");
		animator.SetBool ("attacking", true);
	}


	public override void checkNextState()
	{
		if((player.transform.position - transform.position).magnitude > 2)
		{
		animator.SetBool ("attacking",false);
			Debug.Log ("attacking, not close enough, chasing now.");
		this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateChasing>();
			this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;
			
			this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = true;
			
			this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = false;

		Destroy(this);
		}
	}
}





public class MonsterStateWandering : MonsterState
{
	float speed = 	1f;
	GameObject playerFlashlight;
	float rotSpeed = 10f;
	public Vector3 target;
	public bool hasTarget = false;
	public GameObject targetedObject;
	public List<GameObject> listOfValidCorners = new List<GameObject>();
	public int index;
	public float weirdDistance;
	public bool firstPass = true;
	public void Start()
	{
		player = GameObject.Find ("Capsule");
		animator = transform.GetComponent<Animator>();
		playerFlashlight = GameObject.Find ("FlashLight");
		setAnimBool();
		//Debug.Log ("startScript");
	}
	public override void movement()
	{
		if(hasTarget == false)
		{
			findTargets();

		}

		//float moveX = player.transform.position.x - transform.position.x;
		//float moveZ = player.transform.position.z - transform.position.z;
		//Vector3 move = new Vector3(moveX,0 ,moveZ).normalized;
		//moveX = move.x * Time.deltaTime * speed;
		//moveZ = move.z * Time.deltaTime * speed;
		rigidbody.velocity = Vector3.zero;
		if(hasTarget)
		{
		rotateIfNeeded();
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
		Vector3 tempPos = new Vector3(transform.position.x, 0f, transform.position.z);
		if((tempPos - target).magnitude < 1)
		{
	
			hasTarget = false;
		}

		//Debug.Log ("moving");
	}

	public void findTargets()
	{
		listOfValidCorners = new List<GameObject>();
		GameObject[] corners = GameObject.FindGameObjectsWithTag("ASCorner");

		if(corners.Length == 75)
		{
			for(int i = 0; i < corners.Length; i++)
			{
				if((corners[i].transform.position - transform.position).magnitude < 13 && firstPass == true)
				{
					listOfValidCorners.Add(corners[i]);
				}
				else if((corners[i].transform.position - transform.position).magnitude < 26.5 && firstPass == false)
				{
					listOfValidCorners.Add(corners[i]);
				}
			}
			//Debug.Log (transform.position);

			index = Random.Range (0,listOfValidCorners.Count-1);
			Vector3 tar = listOfValidCorners[index].transform.position;
			target = new Vector3(tar.x, 0f, tar.z);
			//Debug.Log (target);
			hasTarget = true;
			targetedObject = listOfValidCorners[index];
			weirdDistance = (targetedObject.transform.position - transform.position).magnitude;
			//Debug.Log ("monster at " + transform.position + " target at " + targetedObject.transform.position + "distance = " + weirdDistance);

			listOfValidCorners.Clear();
			firstPass = false;
		}
	}


	public void rotateIfNeeded()
	{
		RaycastHit hit;
		Physics.Raycast (transform.position, transform.forward, out hit,20f);
		Vector3 tempPos = new Vector3(transform.position.x, 0f, transform.position.z);

		if(hit.distance < 1f && hit.point != Vector3.zero)
		{
			//Debug.Log (hit.transform.name + " " + hit.distance);
			Vector3 dir = (target - tempPos);//this.transform.position - (transform.position + transform.right);
			Quaternion look = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(this.transform.rotation, look, Time.deltaTime * rotSpeed);
		}
		else
		{
			Vector3 dir;
			int randomDir = Random.Range (0, 2);
			//Debug.Log (randomDir);
			//if(randomDir == 1)
			//{
			//	dir = this.transform.position - (transform.position - transform.right);
			//}
			//else
			//{
			//	dir = this.transform.position - (transform.position + transform.right);
			//}
			dir = target - tempPos;

			Quaternion look = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(this.transform.rotation, look, Time.deltaTime * rotSpeed/5);
		}
	}


	public override void setAnimBool()
	{
		//Debug.Log ("setting anim bool");
		animator.SetBool ("wandering", true);
	}

	public override void checkNextState()
	{
		RaycastHit hit;
		FlashLightScript light = playerFlashlight.GetComponent<FlashLightScript>();
		if(light.hitPoint != Vector3.zero)
		{
			Vector3 lookDirection = light.hitPoint - this.transform.position;
			Physics.Raycast(this.transform.position, lookDirection, out hit, 30f);
			if(hit.point == light.hitPoint)
			{
				animator.SetBool ("wandering",false);
		//		Debug.Log ("wandering stopped, detected light hit");	
				this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateInvestigating>();
				this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;
				
				this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = false;
				
				this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = true;

				this.GetComponent<MonsterStateInvestigating>().lastKnownPos = light.hitPoint;
				Destroy(this);
			}
		}
	}
}






public class MonsterStateSearching : MonsterState
{
	float speed = 0f;
	float timeSearched = 0f;
	GameObject light;
	
	public void Start()
	{
		player = GameObject.Find ("Capsule");
		animator = transform.GetComponent<Animator>();
		setAnimBool();
		light = GameObject.Find ("FlashLight");

		//Debug.Log ("startScript");
	}
	public override void movement()
	{
		rigidbody.velocity = Vector3.zero;
		transform.Rotate(0f, .5f, 0f);
		//Debug.Log ("moving");
	}

	public override void setAnimBool()
	{
		//Debug.Log ("setting anim bool");
		animator.SetBool ("searching", true);
	}

	public override void checkNextState()
	{
		RaycastHit hit;
		FlashLightScript lightScript = light.GetComponent<FlashLightScript>();
		Physics.Raycast(this.transform.position, Vector3.forward, out hit, 100f);
//		Debug.Log (hit.distance);
		if(hit.collider != null)
		{
		if(hit.collider.transform.name == "Capsule" && lightScript.on == true )
		{
			animator.SetBool ("searching",false);
		//	Debug.Log ("Searching stopped,  found player");
			this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateChasing>();
				this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;
				
				this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = true;
				
				this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = false;
			Destroy(this);
			
		}
		}
		if(lightScript.hitPoint != Vector3.zero || timeSearched > 10f)
		{
			Vector3 lookDirection = lightScript.hitPoint - this.transform.position;
			Physics.Raycast(this.transform.position, lookDirection, out hit, 100f);
			if(hit.point == lightScript.hitPoint)
			{
				animator.SetBool ("searching",false);
		//		Debug.Log ("Searching stopped,  found light hit");
				this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateInvestigating>();
				this.GetComponent<MonsterStateInvestigating>().lastKnownPos = lightScript.hitPoint;
				this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;
				
				this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = false;
				
				this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = true;

				Destroy(this);
			}
			else if(timeSearched > 10f)
			{
				animator.SetBool ("searching",false);
			//	Debug.Log ("searching stopped, timed out");
				this.GetComponent<MonsterAI>().state = gameObject.AddComponent<MonsterStateWandering>();
				this.transform.GetComponent<MonsterAI>().realThump.GetComponent<AudioSource>().enabled = true;

				this.transform.GetComponent<MonsterAI>().realRoar.GetComponent<AudioSource>().enabled = false;

				this.transform.GetComponent<MonsterAI>().realGrowl.GetComponent<AudioSource>().enabled = false;


				Destroy(this);
			}
		}

	}

	public void FixedUpdate()
	{
		timeSearched += Time.deltaTime;
	}

}