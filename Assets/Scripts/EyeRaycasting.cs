using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class EyeRaycasting : MonoBehaviour {

    public float maxDist;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        


	}

	public GameObject closestUIElement(GameObject camera, string tag)
	{
		RaycastHit hit;
		Vector3 eyes = camera.transform.position;
		Vector3 ray = camera.transform.forward;
		float distanceToObstacle = 0;
		Debug.DrawRay (eyes, ray,Color.green);

		if (Physics.Raycast (eyes, ray, out hit))
		{
			if (hit.transform.gameObject.tag == tag) 
			{
				return hit.transform.gameObject;
			}
		}
		return null;

	}
	public Dictionary<int, GameObject> highlightedTri(GameObject camera, string tag)
	{
		Dictionary<int, GameObject> highlightedInfo = new Dictionary<int, GameObject> ();
		RaycastHit hit;
		Vector3 eyes = camera.transform.position;
		Vector3 ray = camera.transform.forward;
		float distanceToObstacle = 0;
		Debug.DrawRay (eyes, ray,Color.green);

		if (Physics.Raycast (eyes, ray, out hit))
		{
			if (hit.transform.gameObject.tag == tag) 
			{
				highlightedInfo.Add (hit.triangleIndex, hit.transform.gameObject);
				return highlightedInfo;
			}
		}
		return null;
	}

		
}
