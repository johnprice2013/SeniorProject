using UnityEngine;
using System.Collections;

public abstract class MissionTemplate : MonoBehaviour {

	public bool isComplete = false;
	public string missionType = null;
	public int payout = 0;
	public string missionInfoText;
	public string missionFlavorText;
	public int seed = 0;
	public string missionName = "blank";
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
//		Debug.Log (missionNumber);
		if(started == false)
		{
//			Debug.Log("kill mission generated");
			missionType = "Kill";
			numToKill = Random.Range(3,7);
			setFlavorText();
			setMissionInfoText();
			setPayout();
//			Debug.Log (getMissionText());
			missionName = "Kill " + numToKill;
			started = true;
		}
	}

	public void addKill()
	{
		numKilled++;
	}

	public override string getMissionProgress ()
	{
		return numKilled + " out of " + numToKill + " enemies killed.  ";
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
		int randomNum = Random.Range (0,4);
		switch (randomNum)
		{
		case 0:
			missionFlavorText = "We have heard rumor of bandits attacking miners in asteroid fields. This must be stopped!  ";
				break;
		case 1:
			missionFlavorText = "One of our ambassadors was attacked while passing through a nearby asteroid field.  They must not escape justice!  ";
				break;
		case 2:
			missionFlavorText = "You like killin' bandits in asteroid fields?  Well, today is your lucky day!  ";
				break;
		case 3:
			missionFlavorText = "There is a bounty out for some bandits.  They were last seen ambushing folks in an asteroid field.  ";
				break;
	    default:
		    missionFlavorText = "Bandits in an asteroid field, kill them all!  ";
			    break;
		}

		//throw new System.NotImplementedException ();
	}

	public override void setMissionInfoText ()
	{
		missionInfoText = "We need you to eliminate " + numToKill + " bandits!  ";
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
			missionName = "Fetch " + oreType.oreName;
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
		return "Holding " + amountHeld + " out of " + amountToGet + " " + oreType.oreName + ".  ";
	}

	#region implemented abstract members of MissionTemplate
	public override void setFlavorText ()
	{
		int randomNum = Random.Range (0,4);
		switch (randomNum)
		{
		case 0:
			missionFlavorText = "We need your help!  We are desperately low on " + oreType.oreName + "!  We will pay you handsomely for any you can find!  ";
			break;
		case 1:
			missionFlavorText = "One of our blacksmiths is looking to make a " + oreType.oreName + " blaster, but we are all out of " + oreType.oreName + "!  ";
			break;
		case 2:
			missionFlavorText = "If you happen across some " + oreType.oreName + " on your travels, bring some here.  We are out and will pay big money for some!  ";
			break;
		case 3:
			missionFlavorText = "One of our researchers needs some " + oreType.oreName + " for their work on n-field generators.  We will pay through the nose for it!  ";
			break;
		default:
			missionFlavorText = "We need " + oreType.oreName + " for hull repairs.  We will pay good money for what you bring us!  ";
			break;
		}
	}
	public override void setMissionInfoText ()
	{
		missionInfoText = "We need you to bring us " + amountToGet + " units of " + oreType.oreName + ".  ";
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
			missionName = "Fetch " + itemType.name;
			started = true;
		}
		//throw new System.NotImplementedException ();
	}

	public override string getMissionProgress ()
	{
		return "Holding " + amountHeld + " out of " + amountToGet + " " + itemType.name + ".  ";
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
		payout = (int)(amountToGet * itemType.baseValue * 1.75f);
	}

	#endregion

	#region implemented abstract members of MissionTemplate

	public override void setFlavorText ()
	{
		int randomNum = Random.Range (0,4);
		switch (randomNum)
		{
		case 0:
			missionFlavorText = "We need your help!  We are desperately low on " + itemType.name + "!  We will pay you handsomely for any you can find!  ";
			break;
		case 1:
			missionFlavorText = "One of our researchers is looking to make a " + itemType.name + " replacement, but we are all out of " + itemType.name + "s!  ";
			break;
		case 2:
			missionFlavorText = "If you happen across some " + itemType.name + "s on your travels, bring some here.  We are out and will pay big money for some!  ";
			break;
		case 3:
			missionFlavorText = "One of our researchers needs some " + itemType.name + "s for personal reasons.  We will pay through the nose for some!  ";
			break;
		default:
			missionFlavorText = "We need " + itemType.name + "s for reasons that don't concern you.  We will pay good money for what you bring us!  ";
			break;
		}
	}

	public override void setMissionInfoText ()
	{
		missionInfoText = "We need you to bring us " + amountToGet + " units of " + itemType.name + ".  ";
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
