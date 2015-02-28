using UnityEngine;
using System.Collections;

public class ContainerGUIBehavior : MonoBehaviour {
	public GameObject itemButton;


	// Use this for initialization
	void Start () {
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

	public void UpdateContainerButtons()
	{
		if(transform.parent != null)
		{
			float xPos = 0;
			float yPos = 0;
			foreach(Item item in transform.parent.GetComponent<ContainerInventory>().items)
			{
				Debug.Log (item.name);
				GameObject newButton = (GameObject) Instantiate(itemButton);
				newButton.transform.parent = this.transform.FindChild("Canvas").transform.FindChild("ContainerPanel");
				newButton.transform.position = new Vector2(xPos,yPos);
			}
		}
	}

	public void UpdatePlayerInvButtons()
	{

	}


}
