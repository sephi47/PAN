﻿using UnityEngine;
using UiImage = UnityEngine.UI.Image;
using System.Collections;
using Vuforia;
/// <summary>
/// This class implements the IVirtualButtonEventHandler interface and
/// contains the logic to swap materials for the teapot model depending on what 
/// virtual button has been pressed.
/// </summary>
public class VirtualButtonEventHandler : MonoBehaviour,
IVirtualButtonEventHandler
{


	public AudioSource voixOff_Bar;
	public AudioSource dialogueAveugle;
	public AudioSource dialogueRasta;
	public AudioSource dialogueCougars;
	public AudioSource dialogueBarman;
	public AudioSource dialogueMecBourre;
	public GameObject cougarsContour;
	public GameObject rastaContour;
	public GameObject barContour;
	public GameObject aveugleContour;


	//public UiImage bandeau;
	public UiImage bandeauCougars;
	public UiImage bandeauRasta;

	#region UNITY_MONOBEHAVIOUR_METHODS
	
	void Start()
	{
		// Register with the virtual buttons TrackableBehaviour
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
		for (int i = 0; i < vbs.Length; ++i)
		{
			vbs[i].RegisterEventHandler(this);
		}
	

	}
	
	#endregion // UNITY_MONOBEHAVIOUR_METHODS
	
	
	
	#region PUBLIC_METHODS
	
	/// <summary>
	/// Called when the virtual button has just been pressed:
	/// </summary>
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log("OnButtonPressed::" + vb.VirtualButtonName);



		if (dialogueAveugle.isPlaying)
		{
			dialogueAveugle.Stop ();
			dialogueRasta.Stop ();
			dialogueCougars.Stop ();
			dialogueBarman.Stop ();
			dialogueMecBourre.Stop ();
			if (voixOff_Bar.isPlaying)
				voixOff_Bar.Stop();
			
		}


		// Add the material corresponding to this virtual button
		// to the active material list:
		switch (vb.VirtualButtonName)
		{
			case "rasta_button":
				//StartCoroutine(PlayAnim());
				Handheld.PlayFullScreenMovie ("animation_zoom_bar.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
				bandeauRasta.gameObject.SetActive(true);
				Debug.Log("bandeau is active = "+ bandeauRasta.IsActive());
				break;
			/*
			case "cougars_button":

				//StartCoroutine(PlayAnim());
				Handheld.PlayFullScreenMovie ("animation_zoom_bar_cougars.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
				bandeauCougars.gameObject.SetActive(true);
				Debug.Log("bandeau is active = "+ bandeauCougars.IsActive());
				break;
*/
			default:
				break;
		}
	}


	

	
	/// <summary>
	/// Called when the virtual button has just been released:
	/// </summary>
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
	{
	/*	if (!IsValid())
		{
			return;
		}*/

		/*switch (vb.VirtualButtonName)
		{
		case "rasta_button":
			
			//StartCoroutine(PlayAnim());
			Handheld.PlayFullScreenMovie ("animation_zoom_bar.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
			bandeauRasta.gameObject.SetActive(true);
			Debug.Log("bandeau is active = "+ bandeauRasta.IsActive());
			break;
			
		case "cougars_button":
			
			//StartCoroutine(PlayAnim());
			Handheld.PlayFullScreenMovie ("animation_zoom_bar_cougars.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
			bandeauCougars.gameObject.SetActive(true);
			Debug.Log("bandeau is active = "+ bandeauCougars.IsActive());
			break;
		}*/


	}



	
	
	#endregion // PUBLIC_METHODS
}
