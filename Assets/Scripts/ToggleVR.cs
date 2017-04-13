using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.VR;

public class ToggleVR : MonoBehaviour {

    public bool vrActive;
    public bool VrActive
    {
        get
        {
            return vrActive;
        }
    }
    private GameObject vrRig;
    private GameObject fpsRig;
	private GameObject uiRingContainer;
	private GameObject VRTK_Obj;
	private GameObject VRTK_UI_Obj;
	private GameObject eventSystem;
	// Use this for initialization
	void Awake ()
    {
        vrRig = GameObject.Find("[CameraRig]") as GameObject;
        fpsRig = GameObject.Find("Player") as GameObject;
		uiRingContainer = GameObject.Find ("UI_Ring_Container");
		VRTK_Obj = GameObject.Find ("[VRTK]");
		VRTK_UI_Obj = GameObject.Find ("VRTK_UI");
		eventSystem = GameObject.Find ("EventSystem");
        Transform parentTrans;
		if(vrActive)
        {
            VRSettings.enabled = true;
            vrRig.SetActive(true);
            fpsRig.SetActive(false);
            parentTrans = vrRig.transform;
        }
        else
        {
			VRTK_Obj.SetActive (false);
			VRTK_UI_Obj.SetActive (false);
			eventSystem.SetActive (false);
            SteamVR.SafeDispose();
            VRSettings.enabled = false;
            vrRig.SetActive(false);
            fpsRig.SetActive(true);
            parentTrans = fpsRig.transform;
        }
		uiRingContainer.transform.SetParent(parentTrans);
    }
	
}
