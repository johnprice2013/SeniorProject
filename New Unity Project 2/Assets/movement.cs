using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {


	public float playerVelocity = 0f;
	public Vector3 movementDirection = Vector3.zero;
	public float localXAccel = 0f;
	public float localYAccel = 0f;
	public float localZAccel = 0f;
	public float speed = 5000f;
	public float localXRot = 0f;
	public float localYRot = 0f;
	public float localZRot = 0f;
	public Vector3 localRot = Vector3.zero;
	public float dTime = 0f;
	public float lastTime = 0f;
	public GameObject player;
	public Vector3 upVector = Vector3.zero;
	public Vector3 forwardVector = Vector3.zero;
	public Vector3 rightVector = Vector3.zero;
	public Vector3 totalAccel = Vector3.zero;
	public Vector3 forceFromPlanets = Vector3.zero;
	public float starMass;
	public float G = 2f;
	public Vector3 forceFromStar = Vector3.zero;
	public float distanceToPlayer = 0f;
	public Vector3 directionToPlayer = Vector3.zero;
	public float forceNumber = 0f;
	public double trueX = 0d;
	public double trueY = 0d;
	public double trueZ = 0d;
	public Vector3 telePosition = Vector3.zero;
	public bool hasMoved = false;
	public float totalVelocity = 0f;
	public float changeInAccel = 0f;
	public float tempTime = 0f;
	public float accelHolder = 0f;
	public int planetJumpNum = 0;
	public GameObject playerBody;
	public Initialize playerInit;
	public PlanetInfoUI deets;
	public GameObject planetHolder;
	public bool docking = false;
	public ShipController shipControl;

	// Use this for initialization
	void Start () {
		speed = 500f;
		G = .1f;
		player = GameObject.FindGameObjectWithTag("Player");
		shipControl = player.GetComponent<ShipController>();
		SwitchSector secSwitch = player.GetComponent<SwitchSector>();
		totalAccel.x = secSwitch.savedX;
		totalAccel.y = secSwitch.savedY;
		totalAccel.z = secSwitch.savedZ;
		playerBody = GameObject.FindGameObjectWithTag("PlayerBody");
		playerInit = playerBody.GetComponent<Initialize>();


		light.transform.LookAt(player.transform); 


		if(secSwitch.firstStart)
		{
			transform.position = new Vector3(50000f, 0f, 0f);
			secSwitch.firstStart = false;
		}
		else{
		transform.position = secSwitch.savedPosition;
		}

		trueX = transform.position.x;
		trueY = transform.position.y;
		trueZ = transform.position.z;
	
		starMass = Mathf.Pow((transform.localScale.x/10),3f);


		planetHolder = GameObject.FindGameObjectWithTag("FlightNavigation");
	}
	
	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate()
	{
		dTime = Time.deltaTime;

		deets = planetHolder.GetComponent<PlanetInfoUI>();




	


		if(playerInit.piloting == true)
		{
		if(Input.GetKey(KeyCode.A))
		{
			localXAccel = -1f;
			
		}
		if(Input.GetKey (KeyCode.S))
		{
			localZAccel = -1f;
			
		}
		if(Input.GetKey(KeyCode.W))
		{
			localZAccel = 1f;
		}
		if(Input.GetKey(KeyCode.D))
		{
			localXAccel = 1f;
		}
		if(Input.GetKey(KeyCode.Q))
		{
			player.transform.Rotate (new Vector3(0f,0f,.5f));
		
		}
		if(Input.GetKey(KeyCode.E))
		{
			player.transform.Rotate (new Vector3(0f,0f,-.5f));
			
		}
		if(Input.GetKey (KeyCode.F))
		{
			localXAccel = 0f;
			localYAccel = 0f;
			localZAccel = 0f;
			totalAccel = Vector3.zero;
			forceFromPlanets = Vector3.zero;
		}
		if(Input.GetKey(KeyCode.T))
		{
			
		if(hasMoved == false)
			{
					telePosition = Vector3.zero;
					Vector3 teleOffset = new Vector3(100000f,0f,0f);
						if(deets.orbitals[planetJumpNum].transform.name == "Asteroids(Clone)")
					{
						teleOffset = new Vector3(500f,0f,0f);
					}
					//Debug.Log (deets.planets.Length);//.transform.position + new Vector3(100000f,0f,0f));
				telePosition = -deets.orbitals[planetJumpNum].transform.position + teleOffset;
				trueX += telePosition.x;
				trueY += telePosition.y;
				trueZ += telePosition.z;
				transform.position = telePosition;
			planetJumpNum++;
			if(planetJumpNum > deets.orbitals.Length -1)
			{
				planetJumpNum = 0;
			}
				hasMoved = true;
				StartCoroutine( jumpWait());

			}
				Debug.Log ("jumping?");
		}
		if(Input.GetKey(KeyCode.V))
		{
			GameObject[] stations = GameObject.FindGameObjectsWithTag("station");
			StationMovement statMov = stations[0].GetComponent<StationMovement>();
			totalAccel = -statMov.usedDirection *Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.P) && hasMoved == false)
			{
				hasMoved = true;
				speed *= 10f;
				StartCoroutine(jumpWait());
			}
		if(Input.GetKey(KeyCode.O) && hasMoved == false)
			{
				hasMoved = true;
				speed /= 10f;
				StartCoroutine(jumpWait());
			}
		if(!Input.anyKey)
		{
			localXAccel = 0f;
			localYAccel = 0f;
			localZAccel = 0f;
		}
		}

		localRot = Vector3.zero;
		localXRot = 0f;
		localYRot = 0f;
		if(dTime > .045f)
		{
			Debug.Log ("long wait" + dTime);
		}
		forwardVector = -player.transform.forward * localZAccel;
		rightVector = -player.transform.right * localXAccel;
		upVector = -player.transform.up * localYAccel;
		Vector3 newVector = (forwardVector + rightVector + upVector) * speed;
		directionToPlayer = player.transform.position - transform.position;
		distanceToPlayer = directionToPlayer.magnitude;

		forceFromStar = ((G*starMass)/Mathf.Pow(distanceToPlayer,2f)) * directionToPlayer.normalized; 
		newVector *= Time.deltaTime;
	
		if(shipControl.docking == false)
		{
		totalAccel += newVector*Time.deltaTime + forceFromPlanets * Time.deltaTime;
		}
		if(shipControl.docking == true)// && shipControl.docked == false)
		{
			//shipControl.inControl = false;
			shipControl.disableControl();
			totalAccel = Vector3.zero;
			if(shipControl.distanceToDock.magnitude < 3f)
			{
				totalAccel += -shipControl.distanceToDock;
				shipControl.docked = true;
				//Debug.Log ("tight dock");
			}
			else
			{
			totalAccel += -shipControl.distanceToDock * (1/(1+((shipControl.distanceToDock.magnitude*(Time.deltaTime*50)))));
			}
		}
		if(shipControl.docked == true)
		{
			//shipControl.inControl = false;
		//	totalAccel += -shipControl.distanceToDock;
		}
		trueX += totalAccel.x;
		trueY += totalAccel.y;
		trueZ += totalAccel.z;
		totalVelocity = totalAccel.magnitude;
		changeInAccel += forceFromPlanets.magnitude;
		tempTime += Time.deltaTime;
		if(tempTime >= 1f)
		{
			accelHolder = changeInAccel;
			changeInAccel = 0f;
			tempTime = 0f;
		}
		transform.Translate(totalAccel);
	
	}
	public IEnumerator jumpWait()
	{

		yield return  new WaitForSeconds (0.5f);
		hasMoved = false;
	}
}
