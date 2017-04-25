using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelection : MonoBehaviour 
{
	private EyeRaycasting eyeCaster;
	private MeshSelection meshSelect;
	private GameObject camera;
	private ToggleVR vrToggle;
	// Use this for initialization
	void Start () 
	{
		eyeCaster = GameObject.Find("GameController").GetComponent<EyeRaycasting> ();
		vrToggle = GameObject.Find ("RequiredPrefab").GetComponent<ToggleVR> ();
	}

	public void placePlotOnWorld(GameObject placeholder, int triIndex)
	{
		if (vrToggle.vrActive) 
		{
			camera = GameObject.Find ("Camera (eye)");
		} 
		else 
		{
			GameController.FPSController = GameObject.Find("FirstPersonCharacter") as GameObject;
			camera = GameController.FPSController;
		}

	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
