using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPrefab : MonoBehaviour {
	//public Transform prefab;
	Ray ray;
	RaycastHit hit;
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		/*
		for(int i=0;i<1;i++)
		{
			Instantiate (prefab, new Vector3(i * 2.0f,0,0), Quaternion.identity);
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {

			if (Input.GetKey (KeyCode.Mouse0)) 
			{
				GameObject obj = Instantiate (prefab, new Vector3 (hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
			}
		}



	}

	void spawnObject(){
		
	}
}
