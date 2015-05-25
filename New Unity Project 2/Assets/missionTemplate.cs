using UnityEngine;
using System.Collections;

public abstract class MissionTemplate : MonoBehaviour {

	public bool isComplete = false;
	public string missionType = null;
	public int payout = 0;
	public string missionInfoText;
	public string missionFlavorText;
	public int seed = 0;
	// Use this for initialization
	public abstract void Start ();
	public int missionNumber = 0;
	public bool cancelled = false;
	public bool started = false;
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	//public abstract string getMissionText();

	public abstract string getMissionProgress();

	public abstract void updateProgress();

	public abstract void setPayout();

	public abstract void setFlavorText();

	public abstract void setMissionInfoText();

	public abstract void finishMission();

	public string getMissionText()
	{
		//updateProgress();
		return missionFlavorText + missionInfoText + getMissionProgress();
	}

	public bool getIsComplete()
	{
		return isComplete;
	}




}

public class KillMission : MissionTemplate
{
	public int numToKill = 0;
	public int numKilled = 0;
	public override void Start ()
	{
		if(started == false)
		{
		Debug.Log("kill mission generated");
		missionType = "Kill";
		numToKill = Random.Range(3,7);
		setFlavorText();
		setMissionInfoText();
		setPayout();
		Debug.Log (getMissionText());
		started = true;
		}
	}

	public void addKill()
	{
		numKilled++;
	}

	public override string getMissionProgress ()
	{
		return numKilled + " out of " + numToKill + " enemies killed.";
	}

	public override void updateProgress()
	{
		if(numKilled >= numToKill)
		{
			isComplete = true;
		}
	}

	public override void setPayout()
	{
		payout = numToKill * 1000;
	}

	#region implemented abstract members of MissionTemplate

	public override void setFlavorText ()
	{
		missionFlavorText = "flavor text not set, please implement";
		//throw new System.NotImplementedException ();
	}

	public override void setMissionInfoText ()
	{
		missionInfoText = "We need you to eliminate " + numToKill + " bandits!";
		//throw new System.NotImplementedException ();
	}

	public override void finishMission()
	{
		GameObject.Find ("Capsule").GetComponent<Initialize>().currency += payout;
		Destroy (this);
	}


	#endregion
}

public class FetchOreMission : MissionTemplate
{
	public Ore oreType = null;
	public int amountToGet = 0;
	public int amountHeld = 0;

	public override void Start()
	{
//		Debug.Log("fetch ore mission generated");
		if(started == false)
		{
		missionType = "Fetch Ore";
		oreType = GameObject.Find ("OreInfo").GetComponent<OreInfoScript>().fetchOre(Random.Range (0,1000));
//		Debug.Log (oreType.oreName);
		amountToGet = Random.Range (10,25);
		setPayout();
		setFlavorText();
		setMissionInfoText();
		started = true;
		}
//		Debug.Log (getMissionText());

	}

	public override void updateProgress ()
	{
		foreach(var ore in GameObject.Find ("ShipInventory").GetComponent<ShipInventoryScript>().ores)
		{
			if(ore.oreName == oreType.oreName)
			{
				amountHeld = ore.count;
			}
		}
		if(amountHeld >= amountToGet)
		{
			isComplete = true;
		}
		else
		{
			isComplete = false;
		}
	}

	public override void setPayout()
	{
		payout = (int)(amountToGet * oreType.baseValue * 1.5f);
	}

	public override string getMissionProgress()
	{
		return "Holding " + amountHeld + " out of " + amountToGet + " " + oreType.oreName;
	}

	#region implemented abstract members of MissionTemplate
	public override void setFlavorText ()
	{
		missionFlavorText = "this is not implemented";
		//throw new System.NotImplementedException ();
	}
	public override void setMissionInfoText ()
	{
		missionInfoText = "We need you to bring us " + amountToGet + " units of " + oreType.oreName;
//		Debug.Log (missionInfoText);
		//throw new System.NotImplementedException ();
	}

	public override void finishMission()
	{
		ShipInventoryScript shipInv = GameObject.Find ("ShipInventory").GetComponent<ShipInventoryScript>();
		for(int x = 0; x < amountToGet; x++)
		{
			shipInv.removeSingleItem(oreType);
		}

		GameObject.Find ("Capsule").GetComponent<Initialize>().currency += payout;
		Destroy (this);
	}

	#endregion
}

public class FetchItemMission : MissionTemplate
{
	#region implemented abstract members of missionTemplate
	public Item itemType = null;
	public int amountToGet = 0;
	public int amountHeld = 0;
	public override void Start ()
	{
//		Debug.Log("fetch item mission generated");
		if(started == false)
		{
		missionType = "Fetch Item";
		itemType = GameObject.Find ("ItemList").GetComponent<ItemListInitializer>().fetchItem(Random.Range (0,1000));
		amountToGet = Random.Range (10,25);
		setFlavorText();
		setMissionInfoText();
		setPayout();
		started = true;
		}
		//throw new System.NotImplementedException ();
	}

	public override string getMissionProgress ()
	{
		return "Holding " + amountHeld + " out of " + amountToGet + " " + itemType.name;
	}

	public override void updateProgress ()
	{
		foreach(var ore in GameObject.Find ("Inventory").GetComponent<PlayerInventory>().items)
		{
			if(ore.name == itemType.name)
			{
				amountHeld = ore.count;
			}
		}
		if(amountHeld >= amountToGet)
		{
			isComplete = true;
		}
		else
		{
			isComplete = false;
		}
	}

	public override void setPayout ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	#region implemented abstract members of MissionTemplate

	public override void setFlavorText ()
	{
		missionFlavorText = "not implemented yet, do it soon";
		//throw new System.NotImplementedException ();
	}

	public override void setMissionInfoText ()
	{
		missionInfoText = "We need you to bring us " + amountToGet + " units of " + itemType.name;
		//throw new System.NotImplementedException ();
	}

	public override void finishMission()
	{
			//not implemented////
	}

	#endregion


}

public class VisitMission : MissionTemplate
{

	public override void Start()
	{
		if(started == false)
		{
		Debug.Log("visit mission generated");
		missionType = "Visit";
		setFlavorText();
		setMissionInfoText();
		setPayout();
		started = true;
		}
	}
	#region implemented abstract members of missionTemplate

	public override string getMissionProgress ()
	{

		throw new System.NotImplementedException ();
	}

	public override void updateProgress ()
	{
		throw new System.NotImplementedException ();
	}

	public override void setPayout ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion



	#region implemented abstract members of MissionTemplate
	public override void setFlavorText ()
	{
		missionFlavorText = "not implemented yet, fix soon!";
		//throw new System.NotImplementedException ();
	}
	public override void setMissionInfoText ()
	{
		missionInfoText = "this is a visit mission";
		//throw new System.NotImplementedException ();
	}

	public override void finishMission()
	{
		//not implemented
	}
	#endregion
}
