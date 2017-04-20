using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotData 
{
	public GameObject[,] objects;
	public List<int[]> objectLocations;
	// Use this for initialization
	public PlotData(GameObject[,] objectsOnGrid, List<int[]> objectsOnGridLocations)
	{
		objects = objectsOnGrid;
		objectLocations = objectsOnGridLocations;
	}
}
