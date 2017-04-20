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
	public Vector2[] DetectGridHover(Transform controllerTranform, int tileSize)
	{
		Vector2[] returnVector = new Vector2[16];
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
						List<int[]> adjacentIndeces = new List<int[]> ();
						if (tileSize > 1) 
						{
							for (int i = 1; i < tileSize / 2; i++) 
							{
								adjacentIndeces.Add (new int[] { x - i, z - i });
								adjacentIndeces.Add (new int[] { x - i, z + i });
								adjacentIndeces.Add (new int[] { x + i, z - i });
								adjacentIndeces.Add (new int[] { x + i, z + i });
								adjacentIndeces.Add (new int[] { x + i, z});
								adjacentIndeces.Add (new int[] { x - i, z});
								adjacentIndeces.Add (new int[] { x, z + i});
								adjacentIndeces.Add (new int[] { x, z - i});
							}
							returnVector = new Vector2[adjacentIndeces.Count];
							for(int i = 0; i < adjacentIndeces.Count; i++)
							{
								returnVector [i] = new Vector2 (adjacentIndeces [i] [0], adjacentIndeces [i] [1]);
							}
							return returnVector;
						}
						return new Vector2[] { new Vector2 (x, z) };
					}
				}
			}
		}

		return returnVector;
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
		return new Vector3 (tilePos.x, tilePos.y + 0.05f, tilePos.z);
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
