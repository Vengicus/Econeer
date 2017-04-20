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
		GameObject castedElement = eyeCaster.closestUIElement (camera, "SelectableWorldSection");
		meshSelect = castedElement.GetComponent<MeshSelection> ();
		Vector3 pos = meshSelect.get3DPositionOfTri (triIndex);
		Debug.Log (pos.x + "  ||  " + pos.y + "  ||  " + pos.z);
		GameObject placeHolderObj = (GameObject)Instantiate (placeholder, pos, Quaternion.identity);
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
