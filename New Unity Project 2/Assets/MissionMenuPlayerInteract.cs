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
	public GameObject completeButton;
	
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
			Destroy(completeButton);
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
//				Debug.Log ("found a mission");
				//if(mission.GetComponent<MissionInfo>().cancelled == false)
				//{
				if(mission.GetComponent<MissionDetails>().mission.cancelled == false)
				{
					//new code
					MissionTemplate template = mission.GetComponent<MissionDetails>().mission;
					//
					GameObject missionButton = (GameObject) Instantiate (showMissions);
					missionButton.transform.parent = this.gameObject.transform;
					missionButton.transform.localPosition = new Vector3(0,(localButtonHeight*30)-10,0);
					localButtonHeight--;
					missionButton.GetComponent<Image>().enabled = true;
					missionButton.GetComponent<Button>().enabled = true;
					missionButton.GetComponentInChildren<Text>().enabled = true;
					missionButton.transform.localScale = new Vector3(1,1,1);
					missionButton.transform.localEulerAngles = Vector3.zero;
					//int quickIndex = mission.GetComponent<MissionInfo>().missionNumber;
					int quickIndex = template.missionNumber;
					GameObject mission2 = mission.gameObject;
					missionButton.GetComponentInChildren<Text>().text = template.missionName;

					//missionButton.GetComponentInChildren<Text>().text = mission.GetComponent<MissionInfo>().missionNumber.ToString();
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
		this.GetComponent<Canvas>().worldCamera = GameObject.Find("Capsule").transform.FindChild("Camera").camera;
		if(closeButton != null)
		{
			Destroy(closeButton);
			Destroy(completeButton);
		}
		//this.GetComponent<RectTransform>().sizeDelta = new Vector2(10f,10f);
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
//		Debug.Log ("made it here");
		foreach(Transform button in transform)
		{
			if(button.name == "MissionButton(Clone)")
			{
	
			Destroy(button.gameObject);
			}
		}
//		Debug.Log ("made it here 2");
		if(closeButton != null)
		{
			Destroy(closeButton);
			Destroy (completeButton);
		}
//		Debug.Log ("made it here 3");
		menuLevel = 2;
		createCloseButton(menuLevel);
//		Debug.Log ("made it here 4");
		createCompleteButton(menuLevel,mission);
//		Debug.Log ("still here");
		this.transform.FindChild ("Image").GetComponent<Image>().enabled = true;
	
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().enabled = true;
		this.transform.FindChild("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(195f,200f);//mission.GetComponent<MissionDetails>().mission.getMissionText();
		this.transform.FindChild("Image").GetComponent<RectTransform>().localPosition = new Vector2(0f,-15f);
		//this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().text = mission.GetComponent<MissionInfo>().missionText;
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().text = mission.GetComponent<MissionDetails>().mission.getMissionText();
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<RectTransform>().localPosition = new Vector2(0f,10f);//mission.GetComponent<MissionDetails>().mission.getMissionText();

		acceptButton = (GameObject) Instantiate (showMissions);
		acceptButton.transform.parent = this.gameObject.transform;
		acceptButton.transform.localPosition = new Vector3(0,-60f,0);
		acceptButton.transform.localScale =  new Vector3(1,1,1);

		acceptButton.transform.localEulerAngles = Vector3.zero;
		acceptButton.GetComponent<Image>().enabled = true;
		acceptButton.GetComponent<Button>().enabled = true;
		acceptButton.GetComponentInChildren<Text>().enabled = true;
		acceptButton.GetComponentInChildren<Text>().text = "Cancel Mission";
//		Debug.Log ("really still here");
		acceptButton.GetComponent<Button>().onClick.AddListener(() => { this.deleteAcceptButton(); this.setMissionParentToPlayer(mission.gameObject);this.disableTextAndImage();this.getClosestMissions();});
	
	
	}
	
	public void setMissionParentToPlayer(GameObject mission)
	{

		int missionIndex = 0;
		mission.GetComponent<MissionDetails>().mission.cancelled = true;
		//mission.GetComponent<MissionInfo>().cancelled = true;
		Destroy (mission);
	
	
	}

	public void deleteAcceptButton()
	{
	
		Destroy (acceptButton);
	}

	public void destroyMission(GameObject mission)
	{
		Destroy (mission);
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

	public void createCompleteButton(int level, GameObject mission)
	{
		if(this.transform.parent.parent.parent.name == "GoodStatInterior(Clone)")
		{
//		Debug.Log ("complete button");
		completeButton = (GameObject) Instantiate (showMissions);
		completeButton.transform.parent = this.gameObject.transform;
		completeButton.transform.localPosition = new Vector3(0,-90,0);
	//		completeButton.GetComponent<RectTransform>().sizeDelta = new Vector2(300f,30f);
		completeButton.transform.localScale = new Vector3(1,1,1);
		completeButton.transform.localEulerAngles = Vector3.zero;
//		Debug.Log ("complete button2");
		completeButton.GetComponentInChildren<Text>().text = "Complete";
		mission.GetComponent<MissionDetails>().mission.updateProgress();
//		Debug.Log ("did I make it?");
		if(mission.GetComponent<MissionDetails>().mission.isComplete == false)
		{
			completeButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			completeButton.GetComponent<Button>().interactable = true;
		}
//		Debug.Log ("complete button3");
		completeButton.GetComponent<Button>().onClick.AddListener(() => { completeMission (mission); this.setMissionParentToPlayer(mission.gameObject); this.createMissionButtons (); deleteAcceptButton(); disableTextAndImage();});
//		Debug.Log ("complete button4");
		}
	}

	public void completeMission(GameObject mission)
	{
		mission.GetComponent<MissionDetails>().mission.finishMission();
	}

	public void disableTextAndImage()
	{
	
		this.transform.FindChild ("Image").GetComponent<Image>().enabled = false;
		this.transform.FindChild("Image").transform.FindChild("Text").GetComponent<Text>().enabled = false;
		
	}
}
