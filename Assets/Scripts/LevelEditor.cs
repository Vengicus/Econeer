using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour 
{
	private Dictionary<GameObject, Vector2> objectList = new Dictionary<GameObject, Vector2> ();
	public bool inVR= false;
	private GameObject userInterface;
	// Use this for initialization
	void Start () 
	{
		if (!inVR) 
		{
			userInterface = GameObject.Find ("Canvas");
		} 
		else 
		{
			userInterface = GameObject.Find ("UI_Ring");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void spawnObject()
	{
		
	}
}
