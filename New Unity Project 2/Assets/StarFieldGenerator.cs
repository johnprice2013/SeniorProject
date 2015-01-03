using UnityEngine;
using System.Collections;

public class StarFieldGenerator : MonoBehaviour {
	public Vector4[] starLocations;
	public Vector3 currentSector;
	public Initialize init;
	public int secX;
	public int secY;
	public int secZ;
	public ParticleSystem star;
	// Use this for initialization
	void Start () {
		starLocations = new Vector4[125];

		init = GetComponentInParent<Initialize>();
		updateStarField();
	}

	public void updateStarField()
	{
//		Debug.Log ("starfield");
		currentSector = new Vector3(init.sectorX,init.sectorY,init.sectorZ);
		int num = 0;
		for(secX = (int)currentSector.x-2; secX < (int)currentSector.x + 3; secX++)
		{
			for(secY = (int)currentSector.y-2; secY < (int)currentSector.y + 3; secY++)
			{		
				for(secZ = (int)currentSector.z-2; secZ < (int)currentSector.z + 3; secZ++)
				{
					float newRandomNumber = init.seed + secX * 10000 + secY * 100 + secZ;
					Random.seed = (int)(newRandomNumber % 2000000000);
					
					int newSeed = (int)Random.Range (0f,8000f);
					if(newSeed < 5000F)
					{
						starLocations[num] = new Vector4(secX - (int)currentSector.x,secY - (int)currentSector.y,secZ - (int)currentSector.z,1f);
					}
					else
					{
						starLocations[num] = new Vector4(secX - (int)currentSector.x,secY - (int)currentSector.y,secZ - (int)currentSector.z,0f);
					}
					num++;
				}
			}
		}
		foreach(var starLoc in starLocations)
		{

		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
