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

	public GameObject raycastToElement(GameObject camera, string tag)
	{
		Vector3 eyes = camera.transform.position;
		Vector3 ray = camera.transform.forward;
		float distanceToObstacle = 0;
		return returnRaycastData (eyes, ray, distanceToObstacle, tag);

	}

	private GameObject returnRaycastData(Vector3 start, Vector3 ray, float dist, string tag)
	{
		RaycastHit hit;
		Debug.DrawRay (start, ray,Color.green);
		if (Physics.Raycast (start, ray, out hit))
		{
			if (hit.transform.gameObject.tag == tag) 
			{
				return hit.transform.gameObject;
			}
		}
		return null;
	}


		
}
