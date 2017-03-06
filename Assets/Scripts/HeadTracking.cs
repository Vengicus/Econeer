using UnityEngine;
using System.Collections;

public class HeadTracking : MonoBehaviour {
	public GameObject headPointer;
	// Use this for initialization
	void Start () 
	{
		headPointer = (GameObject)Instantiate (headPointer, transform.forward * 3, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (headPointer.transform.position);
		Vector3 forward = (transform.forward.normalized * 1.5f) - transform.position;
		headPointer.transform.position = new Vector3 (forward.x - 0.25f, forward.y - 0.25f, forward.z);

	}
}
