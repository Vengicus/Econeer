using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeshSelection : MonoBehaviour 
{
	private Dictionary<string, PlotData> plots = new Dictionary<string, PlotData>();
	private Dictionary<string, GameObject> plotObjects = new Dictionary<string, GameObject>();
	public GameObject creatorCard;
	public Material highlightedMaterial;
	public Material unhighlightedMaterial;
	private GameObject currentPlotCard;
	private bool plotCardBuilt = false;
	GameController gameController;
	void Start()
	{
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		Transform[] childTransforms = this.transform.GetComponentsInChildren<Transform> ();
		foreach (Transform trans in childTransforms) 
		{
			if (!trans.name.ToLower ().Contains ("select") && !trans.name.ToLower ().Contains ("object")) {
				string plotName = trans.name;
				MeshCollider col = trans.gameObject.AddComponent <MeshCollider> () as MeshCollider;
				Mesh mesh = trans.gameObject.GetComponent<MeshFilter> ().mesh;
				col.sharedMesh = mesh;
				plotObjects.Add (plotName, trans.gameObject);
				PlotData newPlot = new PlotData (plotName, "No Creator", creatorCard);
				plots.Add (plotName, newPlot);
			}
		}

	}
	public void performMeshSelection(GameObject highlighted, GameObject previouslyHighlighted)
	{
		string name = highlighted.name;
		highlighted.GetComponent<Renderer>().material = highlightedMaterial;
		if (previouslyHighlighted != null) 
		{
			if (previouslyHighlighted != highlighted) 
			{
				previouslyHighlighted.GetComponent<Renderer> ().material = unhighlightedMaterial;
				Destroy (currentPlotCard);
				plotCardBuilt = false;
			} 
			else if(previouslyHighlighted == highlighted)
			{
				if (plotCardBuilt == false) 
				{
					//Debug.Log ("Build Card");
					Vector3 highlightedPlotPos = Vector3.zero;
					try
					{
						if (highlighted.transform.GetChild(0)) 
						{
							Debug.Log ("SELECTABLE");
							Vector3 pos = highlighted.transform.GetChild(0).transform.position;
							pos *= 0.95f;
							highlightedPlotPos = pos;
						}
					}
					catch
					{
						highlightedPlotPos = highlighted.transform.position;
					}
					Quaternion highlightedPlotQuat = Quaternion.LookRotation(highlighted.transform.forward);
					currentPlotCard = Instantiate(plots [name].returnCreatorCard (), highlightedPlotPos, highlightedPlotQuat) as GameObject;
					//currentPlotCard.transform.SetParent (highlighted.transform);
					plotCardBuilt = true;
				}
			}
		}


		if (Input.GetMouseButtonUp (0)) 
		{
			Debug.Log ("SAVE");
			loadPlot (name);
		}
	}

	public void loadPlot(string plotName)
	{
		PlotData selectedPlotData = plots [plotName];
		if (selectedPlotData.CreatorName == "No Creator") 
		{
			gameController.BuildPlot (null, plotName);
		} 
		else 
		{
			gameController.BuildPlot (selectedPlotData.returnPlotObjects(), plotName);
		}
	}
	public void savePlot(GameObject[][] objectsOnPlot, string plotName, string creatorName)
	{
		PlotData selectedPlotData = plots [plotName];
		GameObject spawnOnMap = new GameObject();
		for (int x = 0; x < objectsOnPlot.Length; x++) 
		{
			for (int y = 0; y < objectsOnPlot[x].Length; y++) 
			{
				if (objectsOnPlot [x] [y] != null) 
				{
					spawnOnMap = objectsOnPlot [x] [y];
					continue;
				}
			}
		}
		selectedPlotData.savePlotData (objectsOnPlot, creatorName);
		selectedPlotData.printObjectNames ();
		try
		{
			if (plotObjects[plotName].transform.GetChild(0)) 
			{
				Debug.Log("Spawn On Plot");
				Vector3 pos = plotObjects[plotName].transform.GetChild(0).transform.position;
				spawnOnMap = Instantiate(spawnOnMap, pos, plotObjects[plotName].transform.GetChild(0).transform.rotation) as GameObject;
				spawnOnMap.transform.SetParent(plotObjects[plotName].transform);
				spawnOnMap.transform.localScale *= 0.07f;
			}
		}
		catch 
		{

		}

	}
}
