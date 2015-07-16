/*==============================================================================
Copyright (c) 2010-2013 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class BAR_TrackableEventHandler : MonoBehaviour,
ITrackableEventHandler
{
	#region PRIVATE_MEMBER_VARIABLES
	
	private TrackableBehaviour mTrackableBehaviour;

	//private bool voixOff;
	
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	#region PUBLIC_MEMBER_VARIABLES
	
	//public AudioSource voixOff_Bar;
	
	#endregion // PUBLIC_MEMBER_VARIABLES
	
	
	
	#region UNTIY_MONOBEHAVIOUR_METHODS
	
	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}
	
	#endregion // UNTIY_MONOBEHAVIOUR_METHODS
	
	
	
	#region PUBLIC_METHODS
	
	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	
	#endregion // PUBLIC_METHODS
	
	
	
	#region PRIVATE_METHODS
	
	
	private void OnTrackingFound()
	{
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
		
		// Enable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = true;
		}
		
		// Enable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = true;
		}
		
		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

		/*// si la voix off n'est pas déjà en cours, on la lance!
		if (!voixOff_Bar.isPlaying) {
			voixOff_Bar.Play ();
		}*/

	}
	
	
	private void OnTrackingLost()
	{
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
		
		// Disable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}
		
		// Disable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
		
		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
	}
	
	#endregion // PRIVATE_METHODS
}
