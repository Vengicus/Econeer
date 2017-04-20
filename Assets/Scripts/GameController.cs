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
	public GameObject[,] objectsOnGrid;
	public List<int[]> objectsOnGridLocations;
	private GameObject interactionTransform;
	public static GameObject FPSController;

	private Vector2[] previousTiles;
	private bool controllersFound;
	private GameObject world;
	private WorldSelection worldSelection;

	public GameObject placeholder;

	private Dictionary<int, PlotData> plots = new Dictionary<int, PlotData> ();

	void Start () 
	{
		vrToggle = GameObject.Find ("RequiredPrefab").GetComponent<ToggleVR> ();
		world = GameObject.Find ("WorldSelector");
		worldSelection = world.GetComponent<WorldSelection> ();
		plots = new Dictionary<int, PlotData> ();
		inventory = new List<Inventory_Object> ();
		objectsOnGridLocations = new List<int[]> ();


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
		Vector2 [] hoveredTiles = gridSystem.DetectGridHover (interactionTransform.transform, controllerInputManager.attachedObjectSize);
		if (hoveredTiles.Length > 0) 
		{
			if (previousTiles != null) 
			{
				foreach (Vector2 tile in previousTiles) 
				{
					gridSystem.HighlightTile (tile, false);
				}
				//gridSystem.HighlightTile (previousTiles, false);
			}
			foreach (Vector2 tile in hoveredTiles) 
			{
				gridSystem.HighlightTile (tile, true);
			}
			previousTiles = hoveredTiles;
		}

		GameObject placedObject = controllerInputManager.DetectInput ();

		if (placedObject != null) 
		{
			placedObject.transform.localScale *= 10;
			objectsOnGridLocations.Add (new int[] { (int)hoveredTiles [0].x, (int)hoveredTiles [0].y });
			objectsOnGrid[(int)hoveredTiles[0].x, (int)hoveredTiles[0].y] = (GameObject)Instantiate (placedObject, gridSystem.HoveredTilePosition(hoveredTiles[0]), Quaternion.identity);
			objectsOnGrid[(int)hoveredTiles[0].x, (int)hoveredTiles[0].y].name = "TestObject";
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
				int selectedTri = meshSelect.highlightSelection (triIndex);
				if (Input.GetMouseButtonUp (0)) 
				{
					Debug.Log ("SAVE");
					savePlot (triIndex);
					clearPlot (triIndex);
				}
			}
		}
	}

	void savePlot(int triIndex)
	{
		plots [triIndex] = new PlotData (objectsOnGrid, objectsOnGridLocations);

	}
	void clearPlot(int triIndex)
	{
		Debug.Log (triIndex);
		GameObject firstInstance = null;
		/*for(int x = 0; x < objectsOnGrid.Length; x++) 
		{
			for (int z = 0; z < objectsOnGrid.Length; z++) 
			{
				if (objectsOnGrid [x, z] != null) {
					firstInstance = objectsOnGrid [x, z];
				}
				Destroy (objectsOnGrid[x, z]);
			}
		}*/
		//Array.Clear (objectsOnGrid, 0, objectsOnGrid.Length);
		worldSelection.placePlotOnWorld (placeholder, triIndex);
	}
	void plotSelected(int triIndex)
	{
		foreach (KeyValuePair<int, PlotData> plot in plots) 
		{
			if (plot.Key == triIndex) 
			{
				foreach (int[] loc in plot.Value.objectLocations) 
				{
					objectsOnGrid [loc[0], loc[1]] = (GameObject)Instantiate (plot.Value.objects[loc[0], loc[1]], gridSystem.HoveredTilePosition (new Vector2(loc[0], loc[1])), Quaternion.identity);
					objectsOnGrid [loc[0], loc[1]].name = "TestObject";
				}
			}
		}
	}

	public GameObject[] getControllers()
	{
		return controllers;
	}

}
