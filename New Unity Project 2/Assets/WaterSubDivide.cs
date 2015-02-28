using UnityEngine;
using System.Collections;
//using UnityEditor;

public class WaterSubDivide : MonoBehaviour {

	private Vector3[] baseVertices;
	bool recalculateNormals = true;

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Mesh newMesh = mesh;

		MeshHelper.Subdivide9(newMesh);
		MeshHelper.Subdivide4 (newMesh);    //should be 9
		MeshHelper.Subdivide4 (newMesh);
		mesh = newMesh;
		if (baseVertices == null)
			baseVertices = mesh.vertices;
		var vertices = new Vector3[baseVertices.Length];

				for (int i=0;i<vertices.Length;i++)
				{
					var vertex = baseVertices[i];
		

					vertex = baseVertices[i].normalized * .5f;
		
					vertices[i] = vertex;
				}
		mesh.vertices = vertices;
		if (recalculateNormals)	
			mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		//(PrefabUtility)
		//AssetDatabase.CreateAsset(mesh,"Assets/HighPolySphere.Asset");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
