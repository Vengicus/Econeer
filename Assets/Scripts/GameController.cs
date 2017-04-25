using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour 
{
	private EyeRaycasting eyeCaster;
	private grid gridSystem;
	private ControllerInput controllerInputManager;
	private UI_Interaction uiInteraction;
	private List<Inventory_Object> inventory;
	private JSON_Management jsonManagement;
	private ToggleVR vrToggle;

	public GameObject[] controllers;
	private GameObject camera;
	public GameObject[][] objectsOnGrid;
	public List<int[]> objectsOnGridLocations;
	private GameObject interactionTransform;
	public static GameObject FPSController;

	private Vector2 previousTiles;
	private bool controllersFound;
	private GameObject world;
	private WorldSelection worldSelection;

	public GameObject placeholder;
	private GameObject previousHighlight = null;
	private Animator worldAnimator;

	private GameObject endSessionButton;
	private string currentPlotName;

	private MeshSelection meshSelect;
	public string creatorName = "Noah";

	void Start () 
	{
		vrToggle = GameObject.Find ("RequiredPrefab").GetComponent<ToggleVR> ();
		world = GameObject.Find ("WorldSelector");
		worldSelection = world.GetComponent<WorldSelection> ();
		worldAnimator = world.GetComponent<Animator> ();
		endSessionButton = GameObject.Find ("EndSessionButton");
		endSessionButton.SetActive (false);
		inventory = new List<Inventory_Object> ();
		objectsOnGridLocations = new List<int[]> ();
		meshSelect = GameObject.Find ("Selectable_Areas").GetComponent<MeshSelection> ();


		gridSystem = this.gameObject.GetComponent<grid>();

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
		if (gridSystem.PlotGridExists ()) {
			Vector2 hoveredTiles = gridSystem.DetectGridHover (interactionTransform.transform, controllerInputManager.attachedObjectSize);
			if (hoveredTiles != null) {
				if (previousTiles != null) {
					gridSystem.HighlightTile (previousTiles, false);
					//gridSystem.HighlightTile (previousTiles, false);
				}
				gridSystem.HighlightTile (hoveredTiles, true);
				previousTiles = hoveredTiles;
			}

			GameObject placedObject = controllerInputManager.DetectInput ();

			if (placedObject != null) {
				placedObject.transform.localScale *= 10;
				objectsOnGridLocations.Add (new int[] { (int)hoveredTiles.x, (int)hoveredTiles.y });
				objectsOnGrid [(int)hoveredTiles.x][(int)hoveredTiles.y] = (GameObject)Instantiate (placedObject, gridSystem.HoveredTilePosition (hoveredTiles), Quaternion.identity);
				objectsOnGrid [(int)hoveredTiles.x][(int)hoveredTiles.y].name = "TestObject";
				//objectsOnGrid[(int)hoveredTile.x, (int)hoveredTile.y].transform.parent = gridSystem.HoveredTileObject(hoveredTile).transform;

			}
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
		GameObject castedElement = eyeCaster.raycastToElement (camera, "UI");
		if (castedElement != null) 
		{
			if (castedElement.name.ToLower ().Contains ("icon")) 
			{
				uiInteraction.toggleUIElement (castedElement);
			} 
			if (castedElement.name.ToLower ().Contains ("session")) 
			{
				if (Input.GetMouseButtonUp (0)) 
				{
					ClearPlot ();

				}
			}
		}
	}
	void handleWorldSelect()
	{
		
		GameObject highlightInfo = eyeCaster.raycastToElement (camera, "SelectableWorldSection");
		if (highlightInfo != null) 
		{
			MeshSelection meshSelect = highlightInfo.transform.parent.parent.GetComponent<MeshSelection> ();
			//Debug.Log (highlightInfo.name);
			if (meshSelect != null) 
			{
				meshSelect.performMeshSelection (highlightInfo, previousHighlight);
			}
			previousHighlight = highlightInfo;
		}
	}

	public void BuildPlot(GameObject[][] objectsOnPlot, string plotName)
	{
		currentPlotName = plotName;
		gridSystem.BuildGrid (new Vector2 (4, 4));
		worldAnimator.SetBool ("showWorld", false);
		endSessionButton.SetActive (true);
		objectsOnGrid = new GameObject[4][];
		for (int x = 0; x < 4; x++) 
		{
			objectsOnGrid[x] = new GameObject[4];
			for (int y = 0; y < 4; y++) 
			{

			}
		}
		if (objectsOnPlot != null) 
		{
			for (int x = 0; x < objectsOnPlot.Length; x++) 
			{
				for (int z = 0; z < objectsOnPlot [x].Length; z++) 
				{
					if (objectsOnPlot [x] [z] != null) 
					{
						objectsOnGrid [x] [z] = Instantiate (objectsOnPlot [x] [z], gridSystem.HoveredTilePosition (new Vector2 (x, z)), Quaternion.identity);
						objectsOnGrid [x] [z].name = "TestObject";
					}
				}
			}
		} 
		else 
		{
			
		}
	}
	public void ClearPlot()
	{
		worldAnimator.SetBool ("showWorld", true);
		endSessionButton.SetActive (false);
		meshSelect.savePlot (objectsOnGrid, currentPlotName, creatorName);
		for (int x = 0; x < objectsOnGrid.Length; x++) {
			for (int z = 0; z < objectsOnGrid [x].Length; z++) {
				Destroy (objectsOnGrid [x] [z]);
			}
		}
		objectsOnGrid = null;
		gridSystem.ClearGrid ();
	}


	public GameObject[] getControllers()
	{
		return controllers;
	}

	public GameObject [][] obtainGridObjects()
	{
		return objectsOnGrid;
	}

}
