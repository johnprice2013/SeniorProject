using UnityEngine;
using System.Collections;

public class roomspawner : MonoBehaviour {
	public GameObject room0;
	public GameObject room1;
	public GameObject room2;
	public GameObject wallpiece;
	
	// Use this for initialization
	void Start () {
	for(int x = -20; x<21;)
		{
			for(int z = 0; z<60;)
			{
				int i = Random.Range (0,4);
				Quaternion rot = Quaternion.Euler (new Vector3(0,90*i,0));
				i = Random.Range(0,3);
				if(i == 0)
					{
					GameObject room = (GameObject) Instantiate(room0, new Vector3(x,0,z),rot);
					room.transform.parent = this.transform;
					room.transform.localPosition = new Vector3(x,0,z);
					}
				else if(i == 1)
					{
					GameObject room = (GameObject) Instantiate(room1, new Vector3(x,0,z),rot);

					room.transform.parent = this.transform;
					room.transform.localPosition = new Vector3(x,0,z);
					}
				else
					{
					GameObject room = (GameObject) Instantiate(room2, new Vector3(x,0,z),rot);
					room.transform.parent = this.transform;
					room.transform.localPosition = new Vector3(x,0,z);
					}
			z += 19;
			}
			x += 19;
		}
		
	
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
