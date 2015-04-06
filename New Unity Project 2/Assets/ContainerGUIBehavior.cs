using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ContainerGUIBehavior : MonoBehaviour {
	public GameObject itemButton;
	public Transform panel;
	public Transform otherPanel;
	RectTransform myRect;
	Transform button;
	public GameObject[] buttons;
	public GameObject player;
	RectTransform myRect2;
	bool buttonsActive = false;
	public ContainerInventory contInv;
	public PlayerInventory playInv;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Capsule");
		panel = this.transform.FindChild("Canvas").transform.FindChild("ContainerPanel");
		otherPanel = this.transform.FindChild ("Canvas").transform.FindChild("PlayerInvPanel");
		myRect = panel.GetComponent<RectTransform>();
		myRect2 = otherPanel.GetComponent<RectTransform>();
		button = panel.FindChild("ItemButton");
		playInv = player.transform.Find ("Inventory").GetComponent<PlayerInventory>();
		contInv = transform.parent.GetComponent<ContainerInventory>();

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

	public void shutDownButtons()
	{

		int buttonsToDestroy = panel.childCount + otherPanel.childCount;
		int buttonsDestroyed = 0;
		int passCount = 0;
		int buttonsInCont = panel.childCount;
		int buttonsInPlay = otherPanel.childCount;
		Debug.Log ("found " + buttonsInCont + " cont buttons");
		Debug.Log ("found " + buttonsInPlay + " play buttons");
		while(panel.childCount > 0)
		{
			buttonsDestroyed++;
			DestroyImmediate(panel.GetChild(0).gameObject);
		}
		while(otherPanel.childCount > 0)
		{
			buttonsDestroyed++;
			DestroyImmediate(otherPanel.GetChild(0).gameObject);
		}

	//	Debug.Log ("buttons found = " + buttonsToDestroy + " buttons destroyed = " + buttonsDestroyed);
//		while(panel.transform.childCount > 0 || otherPanel.childCount > 0)
//		{
//		//yield return new WaitForSeconds(0f);
//		}
		this.gameObject.SetActive(false);
	}

	public void createContainerButtons()
	{
		//Debug.Log (contInv.items.Count);
		if(contInv.items.Count > 0)// && buttonsActive == false)
		{
			int itemCount = 0;
			//buttonsActive = true;
//			Debug.Log ("in button creation");
			for(int i = 0; i < contInv.items.Count;i++)
			{
				int index = i;
//				Debug.Log ("creating button");
				GameObject button = (GameObject) Instantiate(itemButton);
				int maxCol = 3;
				int rowNum = 3;
				Item quickItem = new Item(contInv.items[index]);
				//Debug.Log (quickItem.name + quickItem.count);
				//if(itemCount * 80 > myRect.rect.width)
				button.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0f);
				
				button.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0f);
				//Debug.Log (itemCount);
				float xLoc =  ((itemCount%maxCol * 85)+ 50);
				float yLoc = myRect.rect.height - (((int)(itemCount/2))* 85 + 50);
				button.transform.parent = panel.transform;
				button.transform.localPosition = new Vector2(xLoc,yLoc);
				button.transform.localScale = new Vector3(.8f,.8f,.8f);
				button.transform.FindChild("Text").GetComponent<Text>().text = quickItem.count + " " + quickItem.name + " x " + xLoc + " y " + yLoc;
				
				button.GetComponent<Button>().onClick.AddListener(delegate { transferToPlayer (index); UpdateContainerButtons();});
				//buttons[itemCount] = button;
				
				itemCount++;
				//Debug.Log (panel.childCount);
			//	yield return new WaitForSeconds(0f);

			}
		}
//		Debug.Log ("container buttons created = " + panel.childCount);

	}

	public void createPlayerButtons()
	{
		if(playInv.items.Count > 0)
		{
			int itemCount = 0;
			//buttonsActive = true;
			//Debug.Log ("in button creation");
			for(int i = 0; i < playInv.items.Count; i++)
			{
				int index = i;
				//Debug.Log ("creating button");
				GameObject button = (GameObject) Instantiate(itemButton);
				int maxCol = 3;
				int rowNum = 3;
				Item quickItem = new Item(playInv.items[index]);
				//Debug.Log (quickItem.name + quickItem.count);
				//if(itemCount * 80 > myRect.rect.width)
				button.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0f);
				
				button.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0f);
				//Debug.Log (itemCount);
				float xLoc =  ((itemCount%maxCol * 85)+ 50);
				float yLoc = myRect2.rect.height - (((int)(itemCount/2))* 85 + 50);
				button.transform.parent = otherPanel.transform;
				
				button.transform.localPosition = new Vector2(xLoc,yLoc);
				button.transform.localScale = new Vector3(.8f,.8f,.8f);
				button.transform.FindChild("Text").GetComponent<Text>().text = quickItem.count + " " + quickItem.name + " x " + xLoc + " y " + yLoc;
				//Debug.Log (index);
				button.GetComponent<Button>().onClick.AddListener(delegate { transferToContainer (index);});
				//buttons[itemCount] = button;
				
				itemCount++;
			//	Debug.Log (otherPanel.childCount);
			//	yield return new WaitForSeconds(0f);
			}
		}
//		Debug.Log ("player buttons created = " + otherPanel.childCount);

	}

	public void UpdateContainerButtons()
	{
		if(transform.parent != null)
		{
			GameObject player = GameObject.Find ("Capsule");
			deleteContainerButtons();
			deletePlayerButtons();
			//Debug.Log ("in update container function");
			createContainerButtons();

			createPlayerButtons();

		}
	}


	public void deleteContainerButtons()
	{
		int buttonsToDestroy = panel.childCount;
		int buttonsDestroyed = 0;
		int passCount = 0;


		while(panel.childCount > 0)
		{
			passCount++;
			buttonsDestroyed++;
			panel.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
			DestroyImmediate(panel.GetChild(0).gameObject);
		}
		//Debug.Log ("cont buttons found = " + buttonsToDestroy + " cont buttons destroyed = " + buttonsDestroyed);
		//yield return new WaitForSeconds(0f);
	}


	public void deletePlayerButtons()
	{
		int buttonsToDestroy = otherPanel.childCount;
		int buttonsDestroyed = 0;
		int passCount = 0;

		while(otherPanel.childCount > 0)
		{
			passCount++;
			buttonsDestroyed++;
			otherPanel.GetChild(0).GetComponent<Button>().enabled = false;
			DestroyImmediate(otherPanel.GetChild(0).gameObject);
		}

		//Debug.Log ("play buttons found = " + buttonsToDestroy + " play buttons destroyed = " + buttonsDestroyed);
		//yield return new WaitForSeconds(0f);

	}

	public void transferToPlayer(int index)
	{
	//	StartCoroutine(deleteContainerButtons());
	//	StartCoroutine(deletePlayerButtons());
	//	StartCoroutine (waitAndUpdate());
	//	Debug.Log (index);
		Item transferMe = new Item(contInv.items[index]);
		deleteContainerButtons();
		deletePlayerButtons();
	//	Debug.Log (index);
		//Debug.Log ("deleted buttons, removing item" + transferMe.name);

		contInv.removeSingle(transferMe);
		playInv.addSingleItem(transferMe);
		//GameObject.Find ("EvenSystem").SetActive(true);
		//UpdateContainerButtons();
	}


	public void transferToContainer(int index)
	{

		Item transferMe = new Item(playInv.items[index]);
		deleteContainerButtons();
		deletePlayerButtons();
		playInv.removeSingle(transferMe);
		contInv.addSingleItem(transferMe);
		UpdateContainerButtons();
	}

	public IEnumerator waitAndUpdate()
	{
		//Debug.Log(transform.parent.GetComponent<ContainerInventory>().items[1].count);
		//yield return new WaitForSeconds(0);
		yield return new WaitForSeconds(0f);
	//	Debug.Log ("here");
		UpdateContainerButtons();
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
