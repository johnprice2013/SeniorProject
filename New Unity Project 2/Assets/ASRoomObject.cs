using UnityEngine;
using System.Collections;

public class ASRoomObject : MonoBehaviour {
	public GameObject parent;
	public string selfMainType;
	public string selfSubType;
	public int selfID;
	public GameObject self;
	public bool isContainer;
	public GameObject[] possibleIdentities;
	public GameObject myRoom;
	public ASRoomBuilder builder;
	// Use this for initialization
	void Start () {
		parent = this.transform.parent.gameObject;//.transform.parent.transform.parent.gameObject;
		myRoom = parent.transform.parent.transform.parent.gameObject;
		builder = myRoom.GetComponent<ASRoomBuilder>();
		selfMainType = myRoom.GetComponent<ASRoomBuilder>().roomType;
		if(this.name== "ASBed(Clone)")
		{
			selfSubType = "Bed";
		}
		else if(this.name == "ASChair(Clone)")
		{
			selfSubType = "Chair";
		}
		else if(this.name == "ASContainer(Clone)")
		{
			selfSubType = "Container";
		}
		else if(this.name == "ASTable(Clone)")
		{
			selfSubType = "Table";
		}
		else if(this.name == "ASDeskStuff(Clone)")
		{
			selfSubType = "DeskStuff";
		}
		else if(this.name == "ASOther(Clone)")
		{
			selfSubType = "Other";
		}
		selfID = this.transform.parent.transform.GetSiblingIndex() + builder.roomID;

		Random.seed = selfID;

		if(selfSubType == "Bed")
		{
			possibleIdentities = builder.furnishList.beds;
		}
		else if(selfSubType == "Chair")
		{
			possibleIdentities = builder.furnishList.chairs;
		}
		else if(selfSubType == "Container")
		{
			possibleIdentities = builder.furnishList.containers;
		}
		else if(selfSubType == "Table")
		{
			possibleIdentities = builder.furnishList.tables;
		}
		else if(selfSubType == "DeskStuff")
		{
			possibleIdentities = builder.furnishList.deskstuff;
		}
		else if(selfSubType == "Other")
		{
			possibleIdentities = builder.furnishList.others;
		}

		if(possibleIdentities.Length != 0)
		{
		self = possibleIdentities[Random.Range(0,possibleIdentities.Length)];
		GameObject realSelf = (GameObject) Instantiate(self,this.transform.position,Quaternion.identity);
		realSelf.transform.parent = this.transform;
		realSelf.transform.rotation = this.transform.rotation;


			//This is all test code!!!!
			//realSelf.GetComponent<Renderer>().renderer.enabled = false;

			StartCoroutine(goInvisible(realSelf.transform));


//			if(realSelf.transform.childCount != 0)
//			 {
//				foreach(Transform child in this.transform)
//				{
//					
//					if(child.transform.childCount!= 0)
//					{
//						child.transform.GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
//						foreach(Transform grandKid in child.transform)
//						{
//							if(grandKid.transform.childCount != 0)
//							{
//								grandKid.transform.GetComponentInChildren<MeshRenderer>().renderer.enabled = false;
//
//							}
//
//						}
//					}
//				}
//			}


			//end test code




		}
	}

	// Update is called once per frame
	void Update () {
	
	}


	public IEnumerator goInvisible(Transform item)
	{

		if(item.transform.childCount!= 0)
		{
			foreach(Transform child in item.transform)
			{
				StartCoroutine(goInvisible(child));
			}
		}
		if(item.GetComponent<MeshRenderer>()!= null)
		{
			item.GetComponent<MeshRenderer>().renderer.enabled =false;
		}
				 
		yield return new WaitForSeconds(0);
	}



}
