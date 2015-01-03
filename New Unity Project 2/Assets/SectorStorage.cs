using UnityEngine;
using System.Collections;

public class SectorStorage : MonoBehaviour {

	public GameObject tempGhostSector;
	public GameObject ghostSector;
	public Vector3 currentSector;
	// Use this for initialization
	void Start () {
		StartCoroutine (startWait());

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public IEnumerator startWait()
	{
		yield return new WaitForSeconds(.05f);
		int sectorCount = 0;
		currentSector.x = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorX;
		currentSector.y = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorY;
		currentSector.z = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorZ;
//		Debug.Log ("current sector = " + currentSector.x + " " + currentSector.y + " " + currentSector.z);		
		for(int x = -4; x<5; x++)
		{
			for(int y = -4; y<5; y++)
			{
				for(int z = -4; z<5; z++)
				{
					
					GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
					tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(currentSector.x+x,currentSector.y+y,currentSector.z+z));
					tempGhostSector.transform.parent = this.gameObject.transform;
					sectorCount++;
				}
			}
		}
//		Debug.Log ("num of sectors = " + sectorCount);
	}

	public void updateSectors(int posNeg, char dimension)
	{
		StartCoroutine(waitUpdateSectors(posNeg,dimension));



	}

	public IEnumerator waitUpdateSectors(int posNeg, char dimension)
	{
		yield return new WaitForSeconds(.05f);
		currentSector.x = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorX;
		currentSector.y = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorY;
		currentSector.z = GameObject.FindGameObjectWithTag("sectorCenter").GetComponent<SectorGenerator>().sectorZ;
		//0 is negative 1 is positive
		//char can be x y or z indicating which direction we are travelling
		Debug.Log ("updating");
		if(posNeg == 0)
		{
			if(dimension == 'x')
			{
				updateXNeg();
			}
			else if(dimension == 'y')
			{
				updateYNeg();
			}
			else if(dimension == 'z')
			{
				updateZNeg();
				
			}
			
		}
		else if(posNeg == 1)
		{
			if(dimension == 'x')
			{
				updateXPos();
			}
			else if(dimension == 'y')
			{
				updateYPos();
			}
			else if(dimension == 'z')
			{
				updateZPos();
			}
			
		}
		
	}


	public void updateXNeg()
	{
		int numDeleted = 0;
		foreach(Transform sector in this.transform)
		{
			if(sector.GetComponent<InitializeGhostSector>().sectorX > currentSector.x + 4)
			{
				float tempY = sector.GetComponent<InitializeGhostSector>().sectorY;
				float tempZ = sector.GetComponent<InitializeGhostSector>().sectorZ;
				Destroy (sector.gameObject);
				GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
				tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(currentSector.x -2,tempY,tempZ));
				tempGhostSector.transform.parent = this.gameObject.transform;
				numDeleted++;
			}
			
		}
		Debug.Log (numDeleted + "sectors removed and added");
	}

	public void updateYNeg()
	{
		int numDeleted = 0;
		foreach(Transform sector in this.transform)
		{
			if(sector.GetComponent<InitializeGhostSector>().sectorY > currentSector.y + 4)
			{
				float tempX = sector.GetComponent<InitializeGhostSector>().sectorX;
				float tempZ = sector.GetComponent<InitializeGhostSector>().sectorZ;
				Destroy (sector.gameObject);
				GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
				tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(tempX,currentSector.y-2,tempZ));
				tempGhostSector.transform.parent = this.gameObject.transform;
				numDeleted++;
			}
			
		}
		Debug.Log (numDeleted + "sectors removed and added");
	}

	public void updateZNeg()
	{
		int numDeleted = 0;
		foreach(Transform sector in this.transform)
		{
			if(sector.GetComponent<InitializeGhostSector>().sectorZ > currentSector.z + 4)
			{
				float tempY = sector.GetComponent<InitializeGhostSector>().sectorY;
				float tempX = sector.GetComponent<InitializeGhostSector>().sectorX;
				Destroy (sector.gameObject);
				GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
				tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(tempX,tempY,currentSector.z-2));
				tempGhostSector.transform.parent = this.gameObject.transform;
				numDeleted++;
			}
			
		}
		Debug.Log (numDeleted + "sectors removed and added");
	}

	public void updateXPos()
	{
		int numDeleted = 0;
		foreach(Transform sector in this.transform)
		{
			if(sector.GetComponent<InitializeGhostSector>().sectorX < currentSector.x - 4)
			{
				float tempY = sector.GetComponent<InitializeGhostSector>().sectorY;
				float tempZ = sector.GetComponent<InitializeGhostSector>().sectorZ;
				Destroy (sector.gameObject);
				GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
				tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(currentSector.x +2,tempY,tempZ));
				tempGhostSector.transform.parent = this.gameObject.transform;
				numDeleted++;
			}
			
		}
		Debug.Log (numDeleted + "sectors removed and added");
	}

	public void updateYPos()
	{
		int numDeleted = 0;
		foreach(Transform sector in this.transform)
		{
			if(sector.GetComponent<InitializeGhostSector>().sectorY < currentSector.y - 4)
			{
				float tempX = sector.GetComponent<InitializeGhostSector>().sectorX;
				float tempZ = sector.GetComponent<InitializeGhostSector>().sectorZ;
				Destroy (sector.gameObject);
				GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
				tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(tempX,currentSector.y +2,tempZ));
				tempGhostSector.transform.parent = this.gameObject.transform;
				numDeleted++;
			}
			
		}
		Debug.Log (numDeleted + "sectors removed and added");
	}

	public void updateZPos()
	{
		int numDeleted = 0;
		foreach(Transform sector in this.transform)
		{
			
			if(sector.GetComponent<InitializeGhostSector>().sectorZ < currentSector.z - 4)
			{
				float tempY = sector.GetComponent<InitializeGhostSector>().sectorY;
				float tempX = sector.GetComponent<InitializeGhostSector>().sectorX;
				Destroy (sector.gameObject);
				GameObject tempGhostSector =  (GameObject) Instantiate(ghostSector);
				tempGhostSector.GetComponent<InitializeGhostSector>().Initialize(new Vector3(tempX,tempY,currentSector.z+2));
				tempGhostSector.transform.parent = this.gameObject.transform;
				numDeleted++;
			}
		}
		Debug.Log (numDeleted + "sectors removed and added");
	}
}
