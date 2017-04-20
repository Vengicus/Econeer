using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSelection : MonoBehaviour 
{
	Mesh meshInfo;
	private Vector3 [] surfaceNorms;
	private Vector3 [] verts;
	private int [] tris;
	private List<int[]> quads;
	Dictionary<int, Vector3[]> quadVerts;


	void Start()
	{
		meshInfo = this.gameObject.GetComponent<MeshFilter>().mesh;
		verts = meshInfo.vertices;
		tris = meshInfo.triangles;
		surfaceNorms = meshInfo.normals;
		quadVerts = new Dictionary<int, Vector3[]> ();
		quads = setupQuads ();
	}

	private List<int[]> setupQuads()
	{
		List<int[]> tempQuadLayout = new List<int[]> ();
		Dictionary<int, Vector3> tempTriVerts = new Dictionary<int, Vector3> ();
		for(int x = 0; x < tris.Length - 1; x++) 
		{
			Vector3 p0 = verts [tris [x / 3 + 0]];
			Vector3 p1 = verts [tris [x / 3 + 1]];
			Vector3 p2 = verts [tris [x / 3 + 2]];

			Vector3 hyp = getSharedEdge(new Vector3[]{p0, p1, p2});
			foreach (KeyValuePair<int, Vector3> tri in tempTriVerts) 
			{
				if (hyp == tri.Value && tri.Key != x / 3) 
				{
					tempQuadLayout.Add(new int[]{x / 3, tri.Key});
					continue;
				}
			}
			if (!tempTriVerts.ContainsKey(x / 3)) 
			{
				tempTriVerts.Add (x / 3, hyp);
			}
		}
		return tempQuadLayout;
	}
	public int highlightSelection(int triangleIndex)
	{
		int[] matchedPair = new int[2];
		foreach (int[] pairs in quads) 
		{
			if (pairs [0] == triangleIndex || pairs [1] == triangleIndex) 
			{
				matchedPair = pairs;
			}
		}

		Debug.Log (matchedPair [0] + "  ||  " + matchedPair [1]);
		Vector3 p0 = verts [tris [matchedPair[0] * 3 + 0]];
		Vector3 p1 = verts [tris [matchedPair[0] * 3 + 1]];
		Vector3 p2 = verts [tris [matchedPair[0] * 3 + 2]];

		/*int secondIndex = getNeighborIndex (new List<Vector3>{p0, p1, p2}, triangleIndex);*/
		Vector3 P0 = verts [tris [matchedPair[1] * 3 + 0]];
		Vector3 P1 = verts [tris [matchedPair[1] * 3 + 1]];
		Vector3 P2 = verts [tris [matchedPair[1] * 3 + 2]];
		p0 = this.GetComponent<Collider>().transform.TransformPoint (p0);
		p1 = this.GetComponent<Collider>().transform.TransformPoint (p1);
		p2 = this.GetComponent<Collider>().transform.TransformPoint (p2);




		//Debug.Log (p0 + "  ||  " + p1 + "  ||  " + p2);
		P0 = this.GetComponent<Collider>().transform.TransformPoint (P0);
		P1 = this.GetComponent<Collider>().transform.TransformPoint (P1);
		P2 = this.GetComponent<Collider>().transform.TransformPoint (P2);
		Debug.DrawLine (p0, p1);
		Debug.DrawLine (p1, p2);
		Debug.DrawLine (p2, p0);

		Debug.DrawLine (P0, P1);
		Debug.DrawLine (P1, P2);
		Debug.DrawLine (P2, P0);

		return triangleIndex;
	}
	private Vector3 getSharedEdge(Vector3[] verts)
	{
		Vector3 opp = verts[2] - verts[0];
		Vector3 adj = verts[1] - verts[0];
		Vector3 hyp = opp - adj;
		return hyp;
	}

	private int getNeighborIndex(List<Vector3> neighboringVerts, int triIndex)
	{
		List<int> adjacentTris = new List<int> ();
		List<Vector3> matchingVerts = new List<Vector3> ();
		Vector3 matchingVert = Vector3.zero;
		int numberMatch = 0;
		for (int x = 0; x < neighboringVerts.Count; x++) 
		{
			if (matchingVert == Vector3.zero) 
			{
				matchingVert = neighboringVerts [x];
				continue;
			} 
			else 
			{
				if (neighboringVerts [x] == matchingVert) 
				{
					numberMatch++;
				}
			}
			if (numberMatch == 0 && x == neighboringVerts.Count - 1) 
			{
				matchingVert = neighboringVerts [x];
			}
		}
		for (int x = 0; x < tris.Length; x++) 
		{
			Vector3 currentVert = verts [tris [x]];
			if (currentVert == matchingVert) 
			{
				//Debug.Log (currentVert + "     || " + neighboringVerts[0] + "  ||  " + neighboringVerts[1] + "  ||  " + neighboringVerts[2]);
				matchingVerts.Add (currentVert);
				if (matchingVerts.Count == 2) 
				{
					int triNum = x / 3;
					if (triNum != triIndex) 
					{
						//Debug.Log (triNum);
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
	public Vector3 get3DPositionOfTri(int triIndex)
	{
		Debug.Log ("Get Norms");
		return this.transform.position + verts [tris [triIndex]];
	}

}
