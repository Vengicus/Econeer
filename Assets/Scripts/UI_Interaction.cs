using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interaction : MonoBehaviour {
	//<CATEGORY <NAME, GAMEOBJECT>>
	public Dictionary<string, Dictionary<string, GameObject>> inventory = new Dictionary<string, Dictionary<string, GameObject>>();
	private GameObject player;
	private GameObject hierarchy;
	private List<GameObject> UI_Elements;
	private List<GameObject> UI_Icons;
	private List<GameObject> UI_Descriptions;
	private GameObject activeUIElement;

	// Use this for initialization
	public void InitializeUI () 
	{
		UI_Elements = new List<GameObject> ();
		UI_Icons = new List<GameObject> ();
		UI_Descriptions = new List<GameObject> ();
		player = GameObject.FindGameObjectWithTag ("MainCamera") as GameObject;
		//hierarchy = GameObject.Find ("UI_Ring_Hierarchy");
		GameObject [] objects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in objects) 
		{
			if (obj.name.Contains("UI_Element")) 
			{
				
				UI_Elements.Add (obj);
				UI_Icons.Add (obj.transform.Find ("UI_Icon").gameObject);
				UI_Descriptions.Add (obj.transform.Find ("UI_Description").gameObject);
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void toggleUIElement(GameObject newElement)
	{
		GameObject newElementParent = newElement.transform.parent.gameObject;
		for(int x = 0; x < UI_Elements.Count; x++)
		{
			if (newElementParent == UI_Elements [x]) 
			{
				UI_Icons [x].SetActive (false);
				UI_Descriptions [x].SetActive (true);
			} 
			else 
			{
				UI_Icons [x].SetActive (true);
				UI_Descriptions [x].SetActive (false);
			}
		}
	}
}



/*transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z);
		float rotationAngle = Quaternion.LookRotation (player.transform.forward.normalized).eulerAngles.y * Mathf.Deg2Rad;
		Vector3 hierarchyPos = hierarchy.transform.position;
		hierarchy.transform.position = new Vector3((transform.position.x + Mathf.Sin(rotationAngle)), 2.25f, (transform.position.z + Mathf.Cos(rotationAngle)));*/