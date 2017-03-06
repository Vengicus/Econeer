using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour 
{
	Object [] inventoryAssets;
	private EyeRaycasting eyeCaster;
	public grid gridSystem;
	private ControllerInput controllerInputManager;
	private UI_Interaction uiInteraction;
	public GameObject[] controllers;
	public GameObject camera;

	private Vector2 previousTile;
	// Use this for initialization
	public GameObject[,] objectsOnGrid;
	private bool controllersFound;

	void Start () 
	{
		inventoryAssets = AssetDatabase.LoadAllAssetsAtPath ("Assets");
		foreach (Object o in inventoryAssets) 
		{
			Debug.Log (o.name);
		}
		gridSystem = this.gameObject.GetComponent<grid>();
		gridSystem.BuildGrid (new Vector2 (4, 4));
		controllerInputManager = this.gameObject.GetComponent<ControllerInput>();
		controllerInputManager.BeginDetectingInput(controllers);

		eyeCaster = this.gameObject.GetComponent<EyeRaycasting> ();
		uiInteraction = this.gameObject.GetComponent<UI_Interaction> ();

		controllersFound = false;


	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 hoveredTile = gridSystem.DetectGridHover (controllers[1].transform);
		if (hoveredTile.x >= 0) 
		{
			if (previousTile != null) 
			{
				gridSystem.HighlightTile (previousTile, false);
			}
			gridSystem.HighlightTile (hoveredTile, true);
			previousTile = hoveredTile;
		}
		GameObject placedObject = controllerInputManager.DetectInput ();

		if (placedObject != null) 
		{
			placedObject.transform.localScale *= 10;
			objectsOnGrid[(int)hoveredTile.x, (int)hoveredTile.y] = (GameObject)Instantiate (placedObject, gridSystem.HoveredTilePosition(hoveredTile), Quaternion.identity);
			objectsOnGrid[(int)hoveredTile.x, (int)hoveredTile.y].name = "TestObject";
			//objectsOnGrid[(int)hoveredTile.x, (int)hoveredTile.y].transform.parent = gridSystem.HoveredTileObject(hoveredTile).transform;

		}

		if (controllersFound == false && controllers != null) 
		{
			controllersFound = true;
			uiInteraction.InitializeUI ();

		}
		if (controllersFound) 
		{
			handleUI ();
		}


	}
	void handleUI()
	{
		GameObject castedElement = eyeCaster.closestUIElement (camera);
		if (castedElement != null) 
		{
			if (castedElement.name.ToLower ().Contains ("icon")) 
			{
				Debug.Log (castedElement.name);
				uiInteraction.toggleUIElement (castedElement);
			}
		}
	}
	public GameObject[] getControllers()
	{
		return controllers;
	}
}
