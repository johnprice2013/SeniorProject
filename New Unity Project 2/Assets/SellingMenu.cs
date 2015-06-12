using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SellingMenu : MonoBehaviour {

	public GameObject itemButton;
	public GameObject sellButton;
	public Transform panel;
	public Transform otherPanel;
	RectTransform myRect;
	Transform button;
	public GameObject[] buttons;
	public GameObject player;
	RectTransform myRect2;
	bool buttonsActive = false;
	public ShipInventoryScript contInv;
	public PlayerInventory playInv;
	public GameObject closeButton;
	public int menuLevel;
	public GameObject showMissions;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Capsule");
		panel = this.transform.FindChild("Panel");
		//otherPanel = this.transform.FindChild ("Canvas").transform.FindChild("PlayerInvPanel");
		myRect = this.GetComponent<RectTransform>();
		//myRect = panel.GetComponent<RectTransform>();
		//myRect2 = otherPanel.GetComponent<RectTransform>();
		button = panel.FindChild("ItemButton");
		playInv = player.transform.Find ("Inventory").GetComponent<PlayerInventory>();
		contInv = GameObject.Find ("ShipInventory").GetComponent<ShipInventoryScript>();
		createMenuStart();
		//This object will be instantiated when pressing E over a container.
		//when instantiated, display buttons for each of the items in the container
		//from left to right then top to bottom.
		//if there are more items in the container than the screen can hold,
		//use the scroll bar.
		//on click on an item button, pop up a sub menu that will allow you to transfer the item from the
		//container to the player inventory, then refresh the buttons and scrollbar if necessary.
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createMenuStart()
	{
	//	Debug.Log ("creating start menu");
		this.GetComponent<Canvas>().worldCamera = GameObject.Find("Capsule").transform.FindChild("Camera").camera;
		if(closeButton != null)
		{
			Destroy(closeButton);
		}
		clearMenu ();
		menuLevel = 0;
		//createCloseButton(menuLevel);
		foreach(Transform button in transform)
		{
			if(button.name == "MissionButton(Clone)")
			{
				Destroy(button.gameObject);
			}
		}
		sellButton = (GameObject) Instantiate(showMissions);
		sellButton.transform.parent = this.gameObject.transform;
		sellButton.transform.localPosition = new Vector3(0,0,0);
		sellButton.transform.localScale =  new Vector3(.8f,1f,1f);
		sellButton.transform.localEulerAngles = Vector3.zero;
		
		sellButton.GetComponentInChildren<Text>().text = "Sell";
		sellButton.GetComponent<Button>().onClick.AddListener(() => {this.clearMenu(); this.updateSellingButtons();});

	}

	public void clearMenu()
	{
		deleteSellingButtons ();
		//delete all information in menu
	}

	public void createCloseButton(int level)
	{
		closeButton = (GameObject) Instantiate(showMissions);
		closeButton.transform.parent = this.gameObject.transform;
		closeButton.transform.localPosition = new Vector3(0,-100,0);
		closeButton.transform.localScale =  new Vector3(.8f,1f,1f);
		closeButton.transform.localEulerAngles = Vector3.zero;
		
		closeButton.GetComponentInChildren<Text>().text = "Close";
		if(level == 1)
		{
			closeButton.GetComponent<Button>().onClick.AddListener(() => { this.createMenuStart();});
		}

	}

	public void shutDownButtons()
	{
	}
	
	public void createSellingButtons()
	{
		if(sellButton != null)
		{
			Destroy(sellButton);
		}
		menuLevel = 1;
	//	Debug.Log ("create selling buttons called");
		//Debug.Log (contInv.items.Count);
		int itemCount = 0;
		if(playInv.items.Count > 0)// && buttonsActive == false)
		{

			//buttonsActive = true;
			//			Debug.Log ("in button creation");
			for(int i = 0; i < playInv.items.Count;i++)
			{
				int index = i;
				Debug.Log (myRect.rect.width);

				//				Debug.Log ("creating button");
				GameObject button = (GameObject) Instantiate(itemButton);
				Debug.Log(button.GetComponent<RectTransform>().rect.width);
				int maxCol = (int)((myRect.rect.width*2.5) / (itemButton.GetComponent<RectTransform>().rect.width + 15));
				int rowNum = 3;
				Item quickItem = new Item(playInv.items[index]);
				//Debug.Log (quickItem.name + quickItem.count);
				//if(itemCount * 80 > myRect.rect.width)
				button.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0f);
				
				button.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0f);
				//Debug.Log (itemCount);
				float xLoc =  ((itemCount%maxCol * 50)+20);
				float yLoc = myRect.rect.height - (((int)(itemCount/maxCol))* 85 + 50);
				button.transform.parent = panel.transform;
			//	Debug.Log ("before translate");
			//	Debug.Log (button.transform.localPosition);
			//	Debug.Log (xLoc + " " + yLoc);
				//button.GetComponent<RectTransform>().localPosition = new Vector2(xLoc, yLoc);
				button.transform.localPosition = new Vector2(xLoc - 120,yLoc - 160);
				button.transform.localScale = new Vector3(.5f,.8f,.8f);
			//	Debug.Log (button.transform.localPosition);
			//	Debug.Log (button.transform.position);
			//	Debug.Log (button.GetComponent<RectTransform>().rect.position);
				button.transform.FindChild("Text").GetComponent<Text>().text = quickItem.count + " " + quickItem.name + " x " + xLoc + " y " + yLoc;
				
				button.GetComponent<Button>().onClick.AddListener(delegate { this.sellItem(quickItem);});
				//buttons[itemCount] = button;
				
				itemCount++;
				//Debug.Log (panel.childCount);
				//	yield return new WaitForSeconds(0f);
				
			}
		}
		if(contInv.ores.Count > 0)// && buttonsActive == false)
		{
			
			//buttonsActive = true;
			//			Debug.Log ("in button creation");
			for(int i = 0; i < contInv.ores.Count;i++)
			{
				int index = i;
				//				Debug.Log ("creating button");
				GameObject button = (GameObject) Instantiate(itemButton);
				int maxCol = (int)((myRect.rect.width*2.5) / (itemButton.GetComponent<RectTransform>().rect.width + 15));
				int rowNum = 3;
				//Ore quickItem = new Ore(contInv.ores[index]);
				Ore quickItem = new Ore();//this.gameObject.AddComponent<Ore>();
				Debug.Log(contInv.ores[i].count + "in menu");
				quickItem = quickItem.getOre(contInv.ores[index]);
				//Debug.Log (quickItem.name + quickItem.count);
				//if(itemCount * 80 > myRect.rect.width)
				button.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0f);
				
				button.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0f);
				//Debug.Log (itemCount);
				float xLoc =  ((itemCount%maxCol * 50)+20);
				float yLoc = myRect.rect.height - (((int)(itemCount/maxCol))* 85 + 50);
				button.transform.parent = panel.transform;
			//	Debug.Log ("before translate");
			//	Debug.Log (button.transform.localPosition);
			//	Debug.Log (xLoc + " " + yLoc);
				//button.GetComponent<RectTransform>().localPosition = new Vector2(xLoc, yLoc);
				button.transform.localPosition = new Vector2(xLoc - 120,yLoc - 160);
				button.transform.localScale = new Vector3(.5f,.8f,.8f);
			//	Debug.Log (button.transform.localPosition);
			//	Debug.Log (button.transform.position);
			//	Debug.Log (button.GetComponent<RectTransform>().rect.position);
				button.transform.FindChild("Text").GetComponent<Text>().text = quickItem.count + " " + quickItem.oreName + " x " + xLoc + " y " + yLoc;
				
				button.GetComponent<Button>().onClick.AddListener(delegate { this.sellOre(quickItem);});
				//buttons[itemCount] = button;
				
				itemCount++;
				//Debug.Log (panel.childCount);
				//	yield return new WaitForSeconds(0f);
				
			}
		}
		createCloseButton (menuLevel);
		//		Debug.Log ("container buttons created = " + panel.childCount);
		
	}

	public void sellItem(Item passedItem)
	{
		GameObject.Find ("Capsule").GetComponent<Initialize>().currency += (int)passedItem.baseValue;
		playInv.removeSingle(passedItem);
		//sell the item here
		updateSellingButtons();
	}

	public void sellOre(Ore passedOre)
	{
		GameObject.Find ("Capsule").GetComponent<Initialize>().currency += (int)passedOre.baseValue;
		contInv.removeSingleItem(passedOre);
		updateSellingButtons();
	}
	

	public void updateSellingButtons()
	{
		if(transform.parent != null)
		{
		//	Debug.Log ("updateSellingbuttons called");
			GameObject player = GameObject.Find ("Capsule");
			deleteSellingButtons();
			//Debug.Log ("in update container function");
			createSellingButtons();
			
		}
	}
	
	
	public void deleteSellingButtons()
	{
		int buttonsToDestroy = panel.childCount;
		int buttonsDestroyed = 0;
		int passCount = 0;
		
		
		while(panel.childCount > 0)
		{
		//	Debug.Log ("deleting button");
			passCount++;
			buttonsDestroyed++;
			panel.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
			DestroyImmediate(panel.GetChild(0).gameObject);
		}
		//Debug.Log ("cont buttons found = " + buttonsToDestroy + " cont buttons destroyed = " + buttonsDestroyed);
		//yield return new WaitForSeconds(0f);
	}

	public IEnumerator waitAndUpdate()
	{
		//Debug.Log(transform.parent.GetComponent<ContainerInventory>().items[1].count);
		//yield return new WaitForSeconds(0);
		yield return new WaitForSeconds(0f);
		//	Debug.Log ("here");
		updateSellingButtons();
		yield return new WaitForSeconds(0f);
	}
	
	public void FixedUpdate()
	{
		//Debug.Log ("rect min = " + myRect.rect.min);
		//Debug.Log ("rect max = " + myRect.rect.max);
		//Debug.Log (myRect.rect.width);
		//Debug.Log (button.GetComponent<RectTransform>().localPosition);
	}
	
	
}

