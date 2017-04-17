using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour 
{
	Object [] inventoryAssets;
	private EyeRaycasting eyeCaster;
	private grid gridSystem;
	private ControllerInput controllerInputManager;
	private UI_Interaction uiInteraction;
	private List<Inventory_Object> inventory;
	private JSON_Management jsonManagement;
	private ToggleVR vrToggle;

	public GameObject[] controllers;
	private GameObject camera;
	public GameObject[,] objectsOnGrid;
	private GameObject interactionTransform;
	public static GameObject FPSController;

	private Vector2 previousTile;
	private bool controllersFound;
	private GameObject world;
	private WorldSelection worldSelection;

	void Start () 
	{
		vrToggle = GameObject.Find ("RequiredPrefab").GetComponent<ToggleVR> ();
		world = GameObject.Find ("WorldSelector");
		worldSelection = world.GetComponent<WorldSelection> ();
		inventory = new List<Inventory_Object> ();
		inventoryAssets = Resources.LoadAll("BuildingIcons", typeof(Texture2D));

		for(int x = 0; x < inventoryAssets.Length; x++)
		{
			

		}

		gridSystem = this.gameObject.GetComponent<grid>();
		gridSystem.BuildGrid (new Vector2 (4, 4));
		controllerInputManager = this.gameObject.GetComponent<ControllerInput>();
		controllerInputManager.BeginDetectingInput(controllers);

		eyeCaster = this.gameObject.GetComponent<EyeRaycasting> ();
		uiInteraction = this.gameObject.GetComponent<UI_Interaction> ();
		controllersFound = false;

		if (vrToggle.vrActive) 
		{
			interactionTransform = controllers [1];
			camera = GameObject.Find ("Camera (eye)");
		} 
		else 
		{
			interactionTransform = GameObject.Find("FirstPersonCharacter") as GameObject;
			GameController.FPSController = GameObject.Find("FirstPersonCharacter") as GameObject;
			camera = GameController.FPSController;
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
		handleWorldSelect ();
	}
	void handleUI()
	{
		GameObject castedElement = eyeCaster.closestUIElement (camera, "UI");
		if (castedElement != null) 
		{
			if (castedElement.name.ToLower ().Contains ("icon")) 
			{
				Debug.Log (castedElement.name);
				uiInteraction.toggleUIElement (castedElement);
			}
		}
	}
	void handleWorldSelect()
	{
		Dictionary<int, GameObject> highlightInfo = eyeCaster.highlightedTri (camera, "SelectableWorldSection");
		if (highlightInfo != null) 
		{
			int triIndex = -1;
			GameObject hitObj = null;
			foreach (KeyValuePair<int, GameObject> info in highlightInfo) 
			{
				triIndex = info.Key;
				hitObj = info.Value;
			}
			if (triIndex >= 0) 
			{
				Debug.Log (triIndex + "  ||  " + hitObj.name);
				MeshSelection meshSelect = hitObj.GetComponent<MeshSelection> ();
				meshSelect.highlightSelection (triIndex);
			}
		}
	}

	public GameObject[] getControllers()
	{
		return controllers;
	}

}
