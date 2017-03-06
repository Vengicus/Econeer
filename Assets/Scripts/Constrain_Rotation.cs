using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Constrain_Rotation : MonoBehaviour {
    private Quaternion initialRotation;
	public GameObject player;
	private List<GameObject> childrenElements = new List<GameObject>();
	public GameObject uiRing;
	public bool forUiRing = false;
	// Use this for initialization
	void Start ()
    {
		initialRotation = transform.rotation;
		if (forUiRing) 
		{
			int numChildren = transform.childCount;
			for (int x = 0; x < numChildren; x++) 
			{
				if (!transform.GetChild (x).name.ToLower ().Contains ("ui")) 
				{
					childrenElements.Add (transform.GetChild (x).gameObject);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player) 
		{
			Vector3 direction = (player.transform.position - transform.position).normalized;
			Quaternion lookRot = Quaternion.LookRotation (direction * -1);
			if (forUiRing) 
			{
				foreach (GameObject child in childrenElements) 
				{
					child.transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, Time.deltaTime * 100);
					//Quaternion.AngleAxis (analogRotationAngle (padLocation), new Vector3 (0, -1, 0));
				}
				transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z);
			}
			else
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, lookRot, Time.deltaTime * 100);
			}
		}




	}
	void LateUpdate()
	{
		if (uiRing != null) 
		{
			Debug.Log ("FOUND");
			uiRing.transform.eulerAngles = new Vector3 (Mathf.Clamp(uiRing.transform.eulerAngles.x, -1, 1), uiRing.transform.eulerAngles.y, Mathf.Clamp(uiRing.transform.eulerAngles.z, -1, 1));
		}
	}
}
