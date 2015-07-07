using UnityEngine;
using System.Collections;
using Vuforia;


public class CameraDeviceMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		bool focusModeSet = CameraDevice.Instance.SetFocusMode(  
		                                                       CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		
		if (!focusModeSet) {
			Debug.Log("Failed to set focus mode (unsupported mode).");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Look for all fingers
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			
			// -- Tap: quick touch & release
			// ------------------------------------------------
			if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
			{
				CameraDevice.Instance.SetFocusMode(  
				                                   CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
			}
		}
	}
}
