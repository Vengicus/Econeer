using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface_Object : MonoBehaviour {

	public GameObject spawnableObject;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public GameObject obtainObject()
	{
		spawnableObject.transform.position = Vector3.zero;
		return spawnableObject;
	}
}
