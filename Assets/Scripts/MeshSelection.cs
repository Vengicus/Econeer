using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSelection : MonoBehaviour 
{
	Mesh meshInfo;
	private Vector3 [] surfaceNorms;
	private Vector3 [] verts;
	private int [] tris;

	void Start()
	{
		meshInfo = this.gameObject.GetComponent<MeshFilter>().mesh;
		verts = meshInfo.vertices;
		tris = meshInfo.triangles;
		surfaceNorms = meshInfo.normals;


	}
	public void highlightSelection(int triangleIndex)
	{
		

		Vector3 p0 = verts [tris [triangleIndex * 3 + 0]];
		Vector3 p1 = verts [tris [triangleIndex * 3 + 1]];
		Vector3 p2 = verts [tris [triangleIndex * 3 + 2]];

		int secondIndex = triangleIndex++;
		//int secondIndex = getNeighborIndex (new List<Vector3>{p0, p1, p2}, triangleIndex);
		Vector3 P0 = verts [tris [secondIndex * 3 + 0]];
		Vector3 P1 = verts [tris [secondIndex * 3 + 1]];
		Vector3 P2 = verts [tris [secondIndex * 3 + 2]];
		p0 = this.GetComponent<Collider>().transform.TransformPoint (p0);
		p1 = this.GetComponent<Collider>().transform.TransformPoint (p1);
		p2 = this.GetComponent<Collider>().transform.TransformPoint (p2);




		Debug.Log (p0 + "  ||  " + p1 + "  ||  " + p2);
		P0 = this.GetComponent<Collider>().transform.TransformPoint (P0);
		P1 = this.GetComponent<Collider>().transform.TransformPoint (P1);
		P2 = this.GetComponent<Collider>().transform.TransformPoint (P2);
		Debug.DrawLine (p0, p1);
		Debug.DrawLine (p1, p2);
		Debug.DrawLine (p2, p0);

		Debug.DrawLine (P0, P1);
		Debug.DrawLine (P1, P2);
		Debug.DrawLine (P2, P0);
	}

	private int getNeighborIndex(List<Vector3> neighboringVerts, int triIndex)
	{
		List<int> adjacentTris = new List<int> ();
		List<Vector3> adjacentVerts = new List<Vector3> ();
		List<Vector3> matchingVerts = new List<Vector3> ();
		for (int x = 0; x < tris.Length; x++) 
		{
			Vector3 currentVert = verts [tris [x]];
			if (currentVert == neighboringVerts [0] || currentVert == neighboringVerts [1] || currentVert == neighboringVerts [2]) 
			{
				if (matchingVerts.Count > 0 && currentVert == matchingVerts [0]) 
				{
					int triNum = x / 3;
					if (triNum != triIndex) 
					{
						return triNum;
					}
				}
			}
			/*if (currentVert == neighboringVerts [0] || currentVert == neighboringVerts [1] || currentVert == neighboringVerts [2]) 
			{
				int triNum = x / 3;
				if (triNum != triIndex) 
				{
					adjacentTris.Add (triNum);
				}
			}*/
		}


		for (int x = 0; x < adjacentTris.Count; x++) 
		{
			Vector3 currentVert = verts [tris [adjacentTris[x]]];
			if (matchingVerts.Count == 2) 
			{

			}


		}
		return -1;
	}

}
