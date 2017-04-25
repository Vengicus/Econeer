using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Menu_Options : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	[MenuItem("Transform/Wrap around Cylinder")]
	private static void MenuSnapToGrid() 
	{
		List<GameObject> objectsToWrap = new List<GameObject>();
		float radius = 0;
		Vector3 center = new Vector3(0,0,0);
		Transform[] transList = Selection.GetTransforms (SelectionMode.Unfiltered);
		int index = 0;
		foreach (Transform t in transList) 
		{
			if (t.gameObject.name.ToLower ().Contains ("ring")) {
				radius = 0.35f;
				center = t.position;
			} 
			else 
			{
				objectsToWrap.Add (t.gameObject);
			}
			index++;
		}
		for (int i = 0; i < objectsToWrap.Count; i++) 
		{
            //float angle = (360 / objectsToWrap.Count) * i;
            float prog = (i * 1.0f) / objectsToWrap.Count;
            float angle = prog * Mathf.PI * 2;
			Vector3 nextPosition = new Vector3 (
				(Mathf.Cos(angle) * radius) + center.x,
				center.y + 0.05f,
				(Mathf.Sin(angle) * radius) + center.z
			);
			objectsToWrap [i].transform.position = nextPosition;
		}

	}

	[MenuItem("GameObject/Create Empty in Child #&n")]
	static void CreateEmptyObjInChild()
	{
		GameObject go = new GameObject ("GameObject");
		if (Selection.activeTransform != null) 
		{
			go.transform.parent = Selection.activeTransform;
		}
	}
}
