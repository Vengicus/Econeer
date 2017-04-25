using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlotData 
{
	private string plotObjectName;
	private string creatorName;
	private DateTime timeStamp;
	private int contributedPeople;
	private int contributedPower;
	private int contributedHappiness;
	private GameObject[][] objects;
	private List<int[]> objectLocations;
	private GameObject creatorCard;

	public string CreatorName
	{
		get 
		{
			return creatorName;
		}
		set 
		{
			creatorName = value;
		}
	}

	// Use this for initialization
	public PlotData(string plotName, string defaultCreator, GameObject creatorCard)
	{
		plotObjectName = plotName;
		creatorName = defaultCreator;
		contributedHappiness = 0;
		contributedPeople = 0;
		contributedPower = 0;
		timeStamp = System.DateTime.Now;
		this.creatorCard = creatorCard;
		modifyCreatorCard (creatorName, timeStamp);
		objects = new GameObject[4][];
		for (int x = 0; x < 4; x++) {
			objects[x] = new GameObject[4];
		}
	}
	public void modifyCreatorCard(string creator, DateTime time)
	{
		string timeStamp = returnTimeString (time, "time");
		Transform[] childrenUI = creatorCard.GetComponentsInChildren<Transform> ();
		foreach (Transform uiElem in childrenUI) {
			if (uiElem.name.ToLower () == "creator") 
			{
				uiElem.GetComponent<Text> ().text = creator;
			} 
			else if (uiElem.name.ToLower () == "timestamp") 
			{
				uiElem.GetComponent<Text> ().text = timeStamp;
			}
		}
	}
	public string returnTimeString(DateTime time, string format)
	{
		format = format.ToLower ();
		switch(format)
		{
			case "time":
				format = "hh:mm:ss";
				break;
			case "date":
				format = "{0:yyyy-MM-dd}";
				break;
			default:
				format = "hh:mm:ss";
				break;
		}
		return time.ToString(format);
	}
	public GameObject returnCreatorCard()
	{
		return creatorCard;
	}
	public GameObject[][] returnPlotObjects()
	{
		return objects;
	}
	public void savePlotData(GameObject[][] objectData, string creator)
	{
		objects = objectData;
		creatorName = creator;
		timeStamp = DateTime.Now;
		modifyCreatorCard (creator, timeStamp);
	}

	public void printObjectNames()
	{
		string objectNames = "NAMES: ";
		for (int x = 0; x < objects.Length; x++) {
			for (int y = 0; y < objects [x].Length; y++) {
				if (objects [x] [y] != null) 
				{
					objectNames += " (" + x + ", " + y + ") " + objects [x] [y] + "  ||  ";
				}
			}
		}
		Debug.Log (objectNames);
	}
}
