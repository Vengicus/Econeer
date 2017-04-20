using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
	public GameObject spawnablePrefab;
	public string name;
	public string description;
	public int tileSize;
	public int people, power, happiness;
	public int cost;

	public int TileSize
	{
		get 
		{
			return tileSize;
		}
	}
	public GameObject SpawnablePrefab
	{
		get 
		{
			return spawnablePrefab;
		}
	}

	public UIElement()
	{

	}

}
