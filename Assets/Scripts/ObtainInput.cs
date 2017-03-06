using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainInput : MonoBehaviour {
	private SteamVR_TrackedObject trackedObject;
	public SteamVR_Controller.Device device
	{
		get {
			return SteamVR_Controller.Input ((int)trackedObject.index);
		}
	}
	// Use this for initialization
	void Awake () 
	{
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
	}

	public bool padUp()
	{
		return device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad);
	}
	public bool triggerUp()
	{
		return device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger);
	}
	public bool gripUp()
	{
		return device.GetPressDown (SteamVR_Controller.ButtonMask.Grip);
	}
	public float getX()
	{
		return device.GetAxis ().x;
	}
	public float getY()
	{
		return device.GetAxis ().y;
	}
}
