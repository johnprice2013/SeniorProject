using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {
	public bool piloting = false;
	public bool fighting = false;
	public bool docked = false;
	public bool linedUp = false;
	public bool docking = false;
	public ShipState state;
	public GameObject dockingObject;
	public Vector3 distanceToDock;
	public GameObject shipDockPoint;
	// Use this for initialization
	void Start () {
	
		state = gameObject.AddComponent<ShipStateNoControl>();
		shipDockPoint = this.transform.FindChild("ShipDockPort").FindChild("ShipDockPoint").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FixedUpdate()
	{
		distanceToDock = getDistanceToDock();
		state.movement ();
		state.checkNextState();
	}

	public Vector3 getDistanceToDock()
	{
		if(dockingObject != null)
		{
			return shipDockPoint.transform.position - dockingObject.transform.position;// - new Vector3(-.35f,0f,0f);
		}
		else
			return Vector3.zero;
	}
}
