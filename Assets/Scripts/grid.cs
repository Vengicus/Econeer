using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour 
{
	public GameObject plane;
	private Vector2 size = new Vector2 ();
	private float blockSize;
	private GameObject [,] plotGrid;

	public void BuildGrid(Vector2 plotSize)
	{
		size = plotSize;
		plotGrid = new GameObject[(int)size.x, (int)size.y];
		for (int x = 0; x < size.x; x++) 
		{
			for (int z = 0; z < size.y; z++) 
			{
				GameObject gridPlane = (GameObject)Instantiate (plane);
				gridPlane.transform.parent = GameObject.Find ("Grid").transform as Transform;
				// name planes so they are unique
				//gridPlane.name = "tile" + "[" + nextNumber + "," + nextNumber2 + "]";
				gridPlane.name = "Tile (" + x + ", " + z + ")";
				gridPlane.transform.position = new Vector3 (gridPlane.transform.position.x + x,
					gridPlane.transform.position.y, gridPlane.transform.position.z + z);
				plotGrid [x, z] = gridPlane;
			}
		}
		blockSize = plotGrid [0, 0].transform.localScale.x;
	}
	public Vector2 DetectGridHover(Transform controllerTranform)
	{
		Vector3 fwd = transform.TransformDirection(controllerTranform.forward);
		Debug.DrawRay (controllerTranform.position, fwd, Color.green);
		RaycastHit hit;
		if (Physics.Raycast (controllerTranform.position, fwd, out hit)) 
		{
			for (int x = 0; x < size.x; x++)
			{
				for (int z = 0; z < size.y; z++) 
				{
					if (hit.transform.position == plotGrid [x, z].transform.position) 
					{
						return new Vector2 (x, z);
					}
				}
			}
		}

		return new Vector2 (-1, -1);
	}
	public void HighlightTile(Vector2 position, bool highlight)
	{
		Renderer renderer = plotGrid [(int)position.x, (int)position.y].GetComponent<Renderer> ();
		if (highlight) 
		{
			renderer.material.color = Color.cyan;
		} 
		else 
		{
			renderer.material.color = Color.white;
		}
	}
	public Vector3 HoveredTilePosition(Vector2 index)
	{
		Vector3 tilePos = plotGrid [(int)index.x, (int)index.y].transform.position;
		return new Vector3 (tilePos.x, tilePos.y + 0.15f, tilePos.z);
	}
	public GameObject HoveredTileObject(Vector2 index)
	{
		return plotGrid [(int)index.x, (int)index.y].gameObject;
	}

	/*void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 150, 100), "Delete grid [3,3]"))
			Destroy (Grid [2, 2]);
	}*/


}
