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
	public int passCount = 0;
	public GameObject growlAudio;
	public GameObject realGrowl;
	public GameObject roarAudio;
	public GameObject realRoar;
	public GameObject thumpAudio;
	public GameObject realThump;

	// Use this for initialization
	void Start () {
		realRoar = (GameObject) Instantiate(roarAudio);
		realRoar.transform.parent = this.transform;
		realRoar.transform.localPosition = Vector3.zero;
		realGrowl = (GameObject) Instantiate(growlAudio);
		realGrowl.transform.parent = this.transform;
		realGrowl.transform.localPosition = Vector3.zero;
		realThump = (GameObject) Instantiate(thumpAudio);
		realThump.transform.parent = this.transform;
		realThump.transform.localPosition = Vector3.zero;
		realRoar.GetComponent<AudioSource>().enabled = false;
		realGrowl.GetComponent<AudioSource>().enabled = false;
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
		//if(passCount < 5)
		//{
//			passCount++;//
//		}//
		//else
		//{
		state.movement();
		//state.setAnimBool();
		state.checkNextState ();
	//	}
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
