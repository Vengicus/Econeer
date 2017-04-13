using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class JSON_Management 
{
	public JSON_Management()
	{
		
	}
	public string loadJSON(string fileName)
	{
		string filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		if (File.Exists (filePath)) 
		{
			using (StreamReader sr = new StreamReader (filePath)) 
			{
				string data = sr.ReadToEnd ();
				Debug.Log (data);
				return data;
			}
		}
		return null;
	}
	public string[] splitJson(string json)
	{
		//(?<=}),(?<!{)
		//Split at the commas separating the json string
		return Regex.Split(json, "(?<=}),(?<!{)|[|]");

	}
}
