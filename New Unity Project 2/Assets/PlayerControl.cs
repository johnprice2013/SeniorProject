using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {



	public bool piloting = false;
	public bool fighting = false;
	public bool docked = false;
	public PlayerState state;
	// Use this for initialization
	void Start () {
		state = gameObject.AddComponent<PlayerStateFree>();
	}
	
	// Update is called once per frame


	public void FixedUpdate()
	{
		state.movement();
		state.checkNextState();

	}
}
