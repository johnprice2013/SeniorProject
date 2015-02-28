using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour {

	public Animator anim;
	public bool attacking = true;
	public bool chasing = false;
	public bool wandering = false;
	public bool idle = false;
	public bool grounded = false;
	public GameObject player;
	public float runSpeed = 3f;
	public float walkSpeed = 1f;
	public MonsterState state;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		//anim.SetFloat("speed", 5f);
		player = GameObject.Find ("Capsule");
		state = this.gameObject.AddComponent<MonsterStateWandering>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FixedUpdate()
	{
		state.movement();
		//state.setAnimBool();
		state.checkNextState ();
	}


	public void setAllFalse()
	{
		attacking = false;
		chasing = false;
		wandering = false;
		idle = false;
		grounded = false;

	}

}
