using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryList
{
	[System.Serializable]
	public struct InventoryStruct
	{
		[System.Serializable]
		public struct InventoryArray
		{
			public string name, description;
			public int cost, people, power, happiness;
		}
		public InventoryArray[] inv;
	}
}
