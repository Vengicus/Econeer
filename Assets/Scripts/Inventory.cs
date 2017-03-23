using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	private int cost, happiness, energy, people;
	private string name, description;
	private GameObject obj;
	private Texture2D icon;
	public void Init(int cost, int happiness, int energy, int people, string name, string desc, GameObject obj, Texture2D icon)
	{
		this.cost = cost;
		this.happiness = happiness;
		this.energy = energy;
		this.people = people;
		this.name = name;
		this.description = desc;
		this.obj = obj;
		this.icon = icon;
	}
	public virtual void buildPrefab()
	{

	}
}
