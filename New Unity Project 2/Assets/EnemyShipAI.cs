using UnityEngine;
using System.Collections;

public class EnemyShipAI : MonoBehaviour {

	public GameObject playerShip;
	public Vector3 angleToPlayer;
	public float distanceToPlayerShip;
	public Vector3 startPosition;
	public Vector3 startRotation;
	public bool navigating = false;
	public bool attacking = false;
	public float speed = 0f;
	public float maxSpeed = 25f;
	public GameObject EnemyLaser;
	public bool shooting = false;
	public int enemyHealth;
	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindGameObjectWithTag("Fighter");
		enemyHealth = 1000;
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate () {

		distanceToPlayerShip = (transform.position - playerShip.transform.position).magnitude;
		transform.LookAt(playerShip.transform, transform.up);
		if(distanceToPlayerShip < 3000f)
		{
			if(distanceToPlayerShip > 40f)
			{
				navigating = true;
				attacking = false;
			}
			else
			{
				attacking = true;
				navigating = false;
			}

			if(navigating)
			{
				if(speed < 35f)
				{
				speed += 5*Time.deltaTime;
				}
				transform.Translate(transform.forward * speed * Time.deltaTime,Space.World);
			}
			else
			{
				if(speed > 0)
				{
				speed -= 12*Time.deltaTime;
				}
				transform.Translate(transform.forward * speed * Time.deltaTime);
			}

			if(attacking == true)
			{
				if(shooting == false)
				{
				StartCoroutine(shoot());
				}

			}

			if(enemyHealth <= 0)
			{
				GameObject player = GameObject.Find ("Capsule");
				foreach(Transform thing in player.transform)
				{
					if(thing.name == "Mission(Clone)")
					{
						if(thing.GetComponent<MissionDetails>().mission.missionType == "Kill")
						{
							thing.GetComponent<KillMission>().addKill();
						}
					}
				}
				Destroy(this.gameObject);
			}
		}
	}

	public void OnCollisionEnter(Collision hitCollision)
	{
		string hitName = hitCollision.gameObject.name;
		Debug.Log (hitName);
		if(hitName == "LaserShot(Clone)")
		{
			Debug.Log ("got shot");
			enemyHealth -= 50;
		}
	}

	public IEnumerator shoot()
	{
		shooting = true;
		Instantiate(EnemyLaser,transform.position,transform.rotation);
		yield return new WaitForSeconds(.25f);
		shooting = false;
	}
}
