using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MissionMenuPlayerInteract : MonoBehaviour {
	
	public GameObject showMissions;// = new Button(); 
	public GameObject showButton;
	public GameObject closestStation;
	public int localButtonHeight = 3;
	public GameObject[] missionButtons;
	public int buttonIndex = 0;
	public GameObject[] currentMissions;
	public GameObject acceptButton;
	public int menuLevel = 0;
	public GameObject closeButton;
	
	// Use this for initialization
	void Start () {
		createMenuStart();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	
	void Update()
	{
		
		
	}
	
	public void getClosestMissions()
	{

		float currentClosest = 10000000000;
		GameObject stations = GameObject.FindGameObjectWithTag("PlayerBody");

				closestStation = stations;
				
		createMissionButtons ();
	}
	
	public void createMissionButtons()
	{

		Destroy(showButton);
		if(closeButton != null)
		{
			Destroy(closeButton);
		}
		menuLevel = 1;
		createCloseButton(menuLevel);
		localButtonHeight = 3;
		buttonIndex = 0;
		missionButtons = new GameObject[4];
		currentMissions = new GameObject[4];

		foreach(Transform mission in closestStation.transform)
		{


			
			if(mission.transform.name == "Mission(Clone)")
			{	
				if(mission.GetComponent<MissionInfo>().cancelled == false)
				{
				
				GameObject missionButton = (GameObject) Instantiate (showMissions);
				missionButton.transform.parent = this.gameObject.transform;
				missionButton.transform.localPosition = new Vector3(0,(localButtonHeight*30)-10,0);
				localButtonHeight--;
				missionButton.GetComponent<Image>().enabled = true;
				missionButton.GetComponent<Button>().enabled = true;
				missionButton.GetComponentInChildren<Text>().enabled = true;
				missionButton.transform.localScale = new Vector3(1,1,1);
				missionButton.transform.localEulerAngles = Vector3.zero;
				int quickIndex = mission.GetComponent<MissionInfo>().missionNumber;
				GameObject mission2 = mission.gameObject;
				missionButton.GetComponentInChildren<Text>().text = mission.GetComponent<MissionInfo>().missionNumber.ToString();
				missionButton.GetComponent<Button>().onClick.AddListener(delegate {this.showMissionDetails(mission2.gameObject);});
//				
			
				}
//	
			}

			buttonIndex++;
		}
		
		
	}
	
	public void createMenuStart()
	{
		if(closeButton != null)
		{
			Destroy(closeButton);
		}
		menuLevel = 0;
		createCloseButton(menuLevel);
		foreach(Transform button in transform)
		{
			if(button.name == "MissionButton(Clone)")
			{
				Destroy(button.gameObject);
			}
		}
		showButton = (GameObject) Instantiate(showMissions);
		showButton.transform.parent = this.gameObject.transform;
		showButton.transform.localPosition = new Vector3(0,0,0);
		showButton.transform.localScale =  new Vector3(1,1,1);
		showButton.transform.localEulerAngles = Vector3.zero;
		
		showButton.GetComponentInChildren<Text>().text = "Show Missions";
		showButton.GetComponent<Button>().onClick.AddListener(() => {this.getClosestMissions();});

	}
	
	
	public void showMissionDetails(GameObject mission)
	{

		foreach(Transform button in transform)
		{
			if(button.name == "MissionButton(Clone)")
			{
	
			Destroy(button.gameObject);
			}
		}
		if(closeButton != null)
		{
			Destroy(closeButton);
		}
		menuLevel = 2;
		createCloseButton(menuLevel);

		this.transform.FindChild ("Image").GetComponent<Image>().enabled = true;
	
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().enabled = true;
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().text = mission.GetComponent<MissionInfo>().missionText;
		acceptButton = (GameObject) Instantiate (showMissions);
		acceptButton.transform.parent = this.gameObject.transform;
		acceptButton.transform.localPosition = new Vector3(0,-50f,0);
		acceptButton.transform.localScale =  new Vector3(1,1,1);

		acceptButton.transform.localEulerAngles = Vector3.zero;
		acceptButton.GetComponent<Image>().enabled = true;
		acceptButton.GetComponent<Button>().enabled = true;
		acceptButton.GetComponentInChildren<Text>().enabled = true;
		acceptButton.GetComponentInChildren<Text>().text = "Cancel Mission";

		acceptButton.GetComponent<Button>().onClick.AddListener(() => { this.deleteAcceptButton(); this.setMissionParentToPlayer(mission.gameObject);this.disableTextAndImage();this.getClosestMissions();});
	
	
	}
	
	public void setMissionParentToPlayer(GameObject mission)
	{

		int missionIndex = 0;

		mission.GetComponent<MissionInfo>().cancelled = true;
		Destroy (mission);
	
	
	}
	
	public void deleteAcceptButton()
	{
	
		Destroy (acceptButton);
	}
	
	public void createCloseButton(int level)
	{
		closeButton = (GameObject) Instantiate(showMissions);
		closeButton.transform.parent = this.gameObject.transform;
		closeButton.transform.localPosition = new Vector3(0,110,0);
		closeButton.transform.localScale =  new Vector3(1,1,1);
		closeButton.transform.localEulerAngles = Vector3.zero;
		
		closeButton.GetComponentInChildren<Text>().text = "Close";
		if(level == 1)
		{
	
			closeButton.GetComponent<Button>().onClick.AddListener(() => { this.createMenuStart();});
		}
		else if(level ==2)
		{
			closeButton.GetComponent<Button>().onClick.AddListener(() => { this.createMissionButtons(); deleteAcceptButton(); disableTextAndImage();});
		}
	}
	
	public void disableTextAndImage()
	{
	
		this.transform.FindChild ("Image").GetComponent<Image>().enabled = false;
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().enabled = false;
		
	}
}
