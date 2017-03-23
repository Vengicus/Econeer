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
	public GameObject inventoryPrefab;

	private Vector2 previousTile;
	// Use this for initialization
	public GameObject[,] objectsOnGrid;
	private bool controllersFound;
	public static bool noVR;
	public bool noVirtualReality;
	public static GameObject FPSController;
	private GameObject interactionTransform;


	[System.Serializable]
	public struct InventoryStruct
	{
		[System.Serializable]
		public struct InventoryArray
		{
			public string name, description;
			public int cost, people, power, happiness;
		}
		public InventoryArray[] inv;
	}

	void Start () 
	{
		InventoryStruct inventory = new InventoryStruct ();
		GameController.noVR = noVirtualReality;
		inventoryAssets = Resources.LoadAll("BuildingIcons", typeof(Texture2D));
		foreach (Object o in inventoryAssets) 
		{
			inventory = JsonUtility.FromJson<InventoryStruct> ("inventory.json");
			Debug.Log (inventory.inv[0].name);
		}
		gridSystem = this.gameObject.GetComponent<grid>();
		gridSystem.BuildGrid (new Vector2 (4, 4));
		controllerInputManager = this.gameObject.GetComponent<ControllerInput>();
		controllerInputManager.BeginDetectingInput(controllers);

		eyeCaster = this.gameObject.GetComponent<EyeRaycasting> ();
		uiInteraction = this.gameObject.GetComponent<UI_Interaction> ();

		controllersFound = false;

		if (!noVR) 
		{
			interactionTransform = controllers [1];
		} 
		else 
		{
			interactionTransform = GameObject.Find("FirstPersonCharacter") as GameObject;
			GameController.FPSController = GameObject.Find("FirstPersonCharacter") as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		Vector2 hoveredTile = gridSystem.DetectGridHover (interactionTransform.transform);
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


		}
		if (controllersFound) 
		{
			
		}
		uiInteraction.InitializeUI ();
		handleUI ();
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
