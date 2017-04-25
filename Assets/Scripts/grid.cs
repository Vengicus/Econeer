using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour 
{
	public GameObject plane;
	private Vector2 size = new Vector2 ();
	private float blockSize;
	private GameObject [][] plotGrid;

	private bool exists = false;
	public Material highlightMaterial;
	public Material unhighlightMaterial;
	public void BuildGrid(Vector2 plotSize)
	{
		size = plotSize;
		Debug.Log (size.y);
		plotGrid = new GameObject[(int)size.x][];
		for (int x = 0; x < plotGrid.Length; x++) 
		{
			plotGrid [x] = new GameObject[(int)size.y];
			for (int z = 0; z < plotGrid[x].Length; z++) 
			{
				GameObject gridPlane = (GameObject)Instantiate (plane);
				gridPlane.transform.parent = GameObject.Find ("Grid").transform as Transform;
				// name planes so they are unique
				//gridPlane.name = "tile" + "[" + nextNumber + "," + nextNumber2 + "]";
				gridPlane.name = "Tile (" + x + ", " + z + ")";
				gridPlane.transform.position = new Vector3 (gridPlane.transform.position.x + x,
					gridPlane.transform.position.y, gridPlane.transform.position.z + z);
				plotGrid [x][z] = gridPlane;
			}
		}
		blockSize = plotGrid [0][0].transform.localScale.x;
		exists = true;
	}
	public void ClearGrid()
	{
		for (int x = 0; x < plotGrid.Length; x++) 
		{
			for (int z = 0; z < plotGrid [x].Length; z++) 
			{
				Destroy (plotGrid [x] [z]);
			}
		}
		plotGrid = null;
		exists = false;
	}
	public bool PlotGridExists()
	{
		return exists;
	}
	public Vector2 DetectGridHover(Transform controllerTranform, int tileSize)
	{
		Vector2 returnVector = Vector3.zero;
		Vector3 fwd = transform.TransformDirection(controllerTranform.forward);
		Debug.DrawRay (controllerTranform.position, fwd, Color.green);
		RaycastHit hit;
		if (Physics.Raycast (controllerTranform.position, fwd, out hit)) 
		{
			for (int x = 0; x < plotGrid.Length; x++)
			{
				for (int z = 0; z < plotGrid[x].Length; z++) 
				{
					if (hit.transform.position == plotGrid [x][z].transform.position) 
					{
						return new Vector2 (x, z);
					}
				}
			}
		}

		return returnVector;
	}

	public void HighlightTile(Vector2 position, bool highlight)
	{
		Renderer renderer = plotGrid [(int)position.x][(int)position.y].GetComponent<Renderer> ();
		if (highlight) 
		{
			renderer.material = highlightMaterial;
		} 
		else 
		{
			renderer.material = unhighlightMaterial;
		}
	}
	public Vector3 HoveredTilePosition(Vector2 index)
	{
		Vector3 tilePos = plotGrid [(int)index.x][(int)index.y].transform.position;
		return new Vector3 (tilePos.x, tilePos.y + 0.05f, tilePos.z);
	}
	public GameObject HoveredTileObject(Vector2 index)
	{
		return plotGrid [(int)index.x][(int)index.y].gameObject;
	}

	/*void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 150, 100), "Delete grid [3,3]"))
			Destroy (Grid [2, 2]);
	}*/


}
