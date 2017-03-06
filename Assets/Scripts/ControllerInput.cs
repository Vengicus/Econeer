using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerInput : MonoBehaviour {

	public GameObject uiRing;
	private GameObject uiRingParent;
	private bool UI_Toggle = false;
	private Animator uiAnim;
	private Vector2 padLocation;
	private bool toolTips = true;
	private GameObject toolTipsObject;
	// Use this for initialization

	private GameObject[] controllers;
	private SteamVR_TrackedObject[] trackedObjects;
	private ObtainInput[] devices = new ObtainInput[2];

	private GameObject attachedObject;

	public void BeginDetectingInput (GameObject[] controllerDevices) 
	{
		controllers = controllerDevices;
		for(int x = 0; x < controllers.Length; x++)
		{
			GameObject cont = controllers [x];
			devices [x] = cont.GetComponent<ObtainInput> ();
		}

		toolTipsObject = GameObject.Find ("ControllerTooltips");


		uiRingParent = uiRing.transform.parent.gameObject;
		uiAnim = uiRingParent.GetComponent (typeof(Animator)) as Animator;
		StartCoroutine ("CollapseUI");
	}
	public GameObject DetectInput () 
	{
		
		padLocation = new Vector2 (devices[0].getX(), devices[0].getY());
		for (int x = 0; x < controllers.Length; x++) 
		{
			if (devices[0].padUp()) 
			{
				float midTouchRad = 0.25F;
				Debug.Log ("UI");
				if (UI_Toggle) 
				{
					StartCoroutine ("CollapseUI");
				} 
				else 
				{
					StartCoroutine ("ExpandUI");
				}
				UI_Toggle = !UI_Toggle;
				/*if (device.GetAxis ().y < midTouchRad && device.GetAxis ().y > -midTouchRad && device.GetAxis ().x < midTouchRad && device.GetAxis ().x > -midTouchRad) 
			{
				if (UI_Toggle) 
				{
					uiAnim.Play ("Collapse_UI");
				} 
				else 
				{
					uiAnim.Play ("Expand_UI");
				}
				UI_Toggle = !UI_Toggle;
			} 
			else 
			{
				if (device.GetAxis ().y > midTouchRad) 
				{
					Debug.Log ("UP");
				} 
				else if (device.GetAxis ().y < -midTouchRad) 
				{
					Debug.Log ("DOWN");
				}
			}*/
			} 
			if (devices[1].triggerUp()) 
			{
				RaycastHit hit;
				if (Physics.Raycast (controllers [1].transform.position, controllers [1].transform.forward, out hit)) 
				{
					if (hit.transform.gameObject.name.ToLower ().Contains ("description") || hit.transform.gameObject.name.ToLower ().Contains ("icon")) 
					{
						if (attachedObject != null) {
							Destroy (attachedObject);
							attachedObject = null;
						}
						GameObject hitObject = hit.transform.gameObject;
						GameObject spawnableObject = hitObject.GetComponent<UserInterface_Object>().obtainObject();
						Vector3 spawnPos = new Vector3 (controllers [1].transform.position.x, controllers [1].transform.position.y + 0.05f, controllers [1].transform.position.z);
						attachedObject = (GameObject)Instantiate (spawnableObject, spawnPos, Quaternion.identity);
						attachedObject.name = "TestObject";
						attachedObject.transform.parent = controllers [1].transform;
						StartCoroutine ("CollapseUI");
						//attachedObject.transform.position = controllers [1].transform.forward;
					} 
					else if (hit.transform.gameObject.name.ToLower().Contains("tile")) 
					{
						if (attachedObject != null) 
						{
							GameObject returnObject = attachedObject;
							Destroy (attachedObject);
							attachedObject = null;
							returnObject.transform.parent = null;
							StartCoroutine ("ExpandUI");
							return returnObject;
						} 
						else 
						{

						}

					}

				}
			} 
			else if (devices[0].gripUp()) 
			{
				toolTips = !toolTips;
				toolTipsObject.SetActive (toolTips);
			}

		}
		return null;
	}
	void FixedUpdate()
	{
		if (devices[0].getX() != 0 || devices[0].getY() != 0) 
		{
			//uiRing.transform.rotation = Quaternion.AngleAxis (analogRotationAngle (padLocation), uiRing.transform.up) * uiRing.transform.rotation;
			float rotation = analogRotationAngle (padLocation);

			Quaternion angleAxis = Quaternion.AngleAxis (analogRotationAngle (padLocation), Vector3.up);// * uiRing.transform.rotation;
			uiRing.transform.rotation = angleAxis;//Quaternion.Slerp (uiRing.transform.rotation, angleAxis, Time.deltaTime);
		}
	}
	private void collapse()
	{
		

	}
	IEnumerator CollapseUI()
	{
		uiAnim.Play ("Collapse_UI");
		yield return new WaitForSeconds (0.25f);
		StopCoroutine ("CollapseUI");
	}
	IEnumerator ExpandUI()
	{
		uiAnim.Play ("Expand_UI");
		yield return new WaitForSeconds (0.25f);
		StopCoroutine ("ExpandUI");
	}
	float analogRotationAngle(Vector2 padLoc)
	{
		float angle = Mathf.Atan2 (padLoc.y, padLoc.x) * Mathf.Rad2Deg;
		if (angle < 0) {
			angle += 360;
		}
		else if(angle == 0)
		{
			angle = 360;
		}
		return angle;
	}
}
