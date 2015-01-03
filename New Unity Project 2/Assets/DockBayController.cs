using UnityEngine;
using System.Collections;

public class DockBayController : MonoBehaviour {

	public GameObject fighter;
	public GameObject[] walls;
	public GameObject floor;
	public GameObject leftDoor;
	public GameObject rightDoor;
	public GameObject mainDoor;
	public bool loweringWalls = false;
	public bool loweringFloor = false;
	public bool openingDoor = false;
	public bool closingDoor = false;
	public bool openingMainDoor = false;
	public bool closingMainDoor = false;
	public bool raisingWalls = false;
	public bool raisingFloor = false;
	public float wallTime = 0f;
	public float doorTime = 0f;
	public float mainDoorTime = 0f;
	public float floorTime = 0f;
	// Use this for initialization
	void Start () {
		fighter = GameObject.FindGameObjectWithTag("Fighter");
		walls = GameObject.FindGameObjectsWithTag("DockBayWall");
		leftDoor = GameObject.FindGameObjectWithTag("DockBayLeftDoor");
		rightDoor = GameObject.FindGameObjectWithTag("DockBayRightDoor");
		mainDoor = GameObject.FindGameObjectWithTag("DockBayDoor");
		floor = GameObject.FindGameObjectWithTag("DockBayFloor");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	public void undockProcedure()
	{
		StartCoroutine (lowerWalls());
		StartCoroutine (lowerFloor());
		StartCoroutine (closeDoubleDoor());
		StartCoroutine (openDoor());

	}

	public void dockProcedure()
	{
		StartCoroutine (alignFighter());
		StartCoroutine (closeDoor());
		StartCoroutine (openDoubleDoor());
		StartCoroutine (raiseFloor());
		StartCoroutine (raiseWalls());
	}

	public IEnumerator lowerWalls()
	{
		Debug.Log ("lowering walls");
		foreach(var wall in walls)
		{
			wall.GetComponent<lowerSelf>().lowering = true;
		}
		mainDoor.GetComponent<lowerSelf>().lowering = true;
	
		yield return new WaitForSeconds(5f);
	
		foreach(var wall in walls)
		{
			wall.GetComponent<lowerSelf>().lowering = false;
		}
		mainDoor.GetComponent<lowerSelf>().lowering = false;
	}

	public IEnumerator lowerFloor()
	{
				
		Debug.Log ("lowering floor");
		floor.GetComponent<floorMover>().lowering = true;
		fighter.GetComponent<lowerFighter>().lowering = true;
		yield return new WaitForSeconds(5f);
		floor.GetComponent<floorMover>().lowering = false;
		fighter.GetComponent<lowerFighter>().lowering = false;
	}

	public IEnumerator closeDoubleDoor()
	{
				mainDoorTime = 0f;
		yield return new WaitForSeconds(5f);
		leftDoor.GetComponent<OpenCloseLeft>().closing = true;
		rightDoor.GetComponent<OpenCloseRight>().closing = true;
		yield return new WaitForSeconds(3f);
		GameObject.Find ("MainShip").GetComponentInChildren<MeshRenderer>().renderer.enabled = true;
		leftDoor.GetComponent<OpenCloseLeft>().closing = false;
		rightDoor.GetComponent<OpenCloseRight>().closing = false;
		Debug.Log ("closing main door");
	}

	public IEnumerator openDoor()
	{
				doorTime = 0f;
		yield return new WaitForSeconds(8f);
		mainDoor.GetComponent<lowerSelf>().opening = true;
		yield return new WaitForSeconds(3f);
		mainDoor.GetComponent<lowerSelf>().opening = false;
		fighter.GetComponent<FighterController>().piloting = true;
		Debug.Log ("opening door");
	}

	public IEnumerator alignFighter()
	{
		fighter.GetComponent<FighterController>().piloting = false;
		fighter.GetComponent<lowerFighter>().align ();
		fighter.GetComponent<lowerFighter>().aligning = true;
		yield return new WaitForSeconds(3f);
		fighter.GetComponent<lowerFighter>().aligning = false;
	}

	public IEnumerator closeDoor()
	{
		mainDoor.GetComponent<lowerSelf>().closing = true;
		yield return new WaitForSeconds(3f);
		mainDoor.GetComponent<lowerSelf>().closing = false;
	}

	public IEnumerator openDoubleDoor()
	{
		yield return new WaitForSeconds(6f);
		GameObject.Find ("MainShip").GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
		leftDoor.GetComponent<OpenCloseLeft>().opening = true;
		rightDoor.GetComponent<OpenCloseRight>().opening = true;
		yield return new WaitForSeconds(3f);
		leftDoor.GetComponent<OpenCloseLeft>().opening = false;
		rightDoor.GetComponent<OpenCloseRight>().opening = false;
	}

	public IEnumerator raiseFloor()
	{
		yield return new WaitForSeconds(9f);
		floor.GetComponent<floorMover>().raising = true;
		fighter.GetComponent<lowerFighter>().raising = true;
		yield return new WaitForSeconds(5f);
		floor.GetComponent<floorMover>().raising = false;
		fighter.GetComponent<lowerFighter>().raising = false;
	}

	public IEnumerator raiseWalls()
	{
		yield return new WaitForSeconds(9f);
		foreach(var wall in walls)
		{
			wall.GetComponent<lowerSelf>().raising = true;
		}
		yield return new WaitForSeconds(5f);
		foreach(var wall in walls)
		{
			wall.GetComponent<lowerSelf>().raising = false;
		}
	}
}
