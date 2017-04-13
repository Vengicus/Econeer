using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Object
{
	public string name, description;
	public int cost, people, power, happiness;
	public Texture2D icon;
	public Inventory_Object(string name, string desc, int cost, int people, int power, int happy, Texture2D tex)
	{
		this.name = name;
		this.description = desc;
		this.cost = cost;
		this.people = people;
		this.power = power;
		this.happiness = happy;
		this.icon = tex;
	}
}
