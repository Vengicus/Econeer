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

	public GameObject attachedObject;
	public int attachedObjectSize;

	private ToggleVR vrToggle;

	void Start()
	{
		vrToggle = GameObject.Find ("RequiredPrefab").GetComponent<ToggleVR> ();
	}

	public void BeginDetectingInput (GameObject[] controllerDevices) 
	{
		try
		{
		controllers = controllerDevices;
		for(int x = 0; x < controllers.Length; x++)
		{
			GameObject cont = controllers [x];
			devices [x] = cont.GetComponent<ObtainInput> ();
		}

		toolTipsObject = GameObject.Find ("ControllerTooltips");
		}
		catch {

		}

		uiRingParent = uiRing.transform.parent.gameObject;
		uiAnim = uiRingParent.GetComponent (typeof(Animator)) as Animator;
		StartCoroutine ("CollapseUI");
	}
	public GameObject DetectInput () 
	{
		if (vrToggle.vrActive) 
		{
			padLocation = new Vector2 (devices [0].getX (), devices [0].getY ());
			for (int x = 0; x < controllers.Length; x++) 
			{
				if (devices [0].padUp ()) 
				{
					expandCollapseUI ();
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
				if (devices [1].triggerUp () || Input.GetMouseButtonUp (0)) 
				{
					return raycastToTile (controllers [1]);
				}
				else if (devices [0].gripUp ()) 
				{
					toolTips = !toolTips;
					toolTipsObject.SetActive (toolTips);
				}

			}
		} 
		else 
		{
			if (Input.GetMouseButtonUp (1)) 
			{
				expandCollapseUI ();
			}
			if (Input.GetMouseButtonUp (0)) 
			{
				return raycastToTile (GameController.FPSController);
			}
		}
		return null;
	}
	void FixedUpdate()
	{
		try
		{
		if (devices[0].getX() != 0 || devices[0].getY() != 0) 
		{
			//uiRing.transform.rotation = Quaternion.AngleAxis (analogRotationAngle (padLocation), uiRing.transform.up) * uiRing.transform.rotation;
			float rotation = analogRotationAngle (padLocation);

			Quaternion angleAxis = Quaternion.AngleAxis (analogRotationAngle (padLocation), Vector3.up);// * uiRing.transform.rotation;
			uiRing.transform.rotation = angleAxis;//Quaternion.Slerp (uiRing.transform.rotation, angleAxis, Time.deltaTime);
		}
		}
		catch{
		}
	}

	void expandCollapseUI()
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
	}
	GameObject raycastToTile(GameObject origin)
	{
		RaycastHit hit;
		if (Physics.Raycast (origin.transform.position, origin.transform.forward, out hit)) 
		{
			if (hit.transform.gameObject.name.ToLower ().Contains ("description") || hit.transform.gameObject.name.ToLower ().Contains ("icon")) 
			{
				if (attachedObject != null) {
					Destroy (attachedObject);
					attachedObject = null;
				}
				GameObject hitObject = hit.transform.gameObject;
				Transform spawnOrigin = GameController.FPSController.transform;
				if (vrToggle.vrActive) {
					spawnOrigin = controllers [1].transform;
				}
				GameObject spawnableObject = hitObject.GetComponentInParent<UIElement> ().SpawnablePrefab;
				int tileSize = hitObject.GetComponentInParent<UIElement> ().TileSize;
				Vector3 spawnPos = new Vector3 (spawnOrigin.position.x, spawnOrigin.position.y, spawnOrigin.position.z);
				attachedObject = (GameObject)Instantiate (spawnableObject, spawnPos, Quaternion.identity);
				attachedObject.name = "TestObject";
				attachedObject.transform.parent = spawnOrigin;
				attachedObject.transform.localScale = new Vector3 (0.08f * tileSize , 0.08f * tileSize, 0.08f * tileSize);

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
					Debug.Log ("Place");
					return returnObject;
				} 
				else 
				{

				}

			}

		}
		return null;
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
