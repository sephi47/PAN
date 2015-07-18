/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
	public class TargetBarTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
		
		private bool voixOffAlreadyPlays;

        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES

		public AudioSource voixOff_Bar;
		public AudioSource dialogueAveugle;
		public AudioSource dialogueRasta;
		public AudioSource dialogueCougars;
		public AudioSource dialogueBarman;
		public AudioSource dialogueMecBourre;

        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
			
			voixOffAlreadyPlays = false;
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
		
			// évite la répétition si on reste sur la target
			if (!voixOffAlreadyPlays)
			{
				// si la voix off n'est pas déjà en cours on lance la voix off
				if (!voixOff_Bar.isPlaying) 
				{
					voixOff_Bar.Play ();
					voixOffAlreadyPlays = true;
				}
			}

			// joue les paroles d'ambiances
			dialogueAveugle.Play();
			dialogueRasta.Play();
			dialogueCougars.Play();
			dialogueBarman.Play();
			dialogueMecBourre.Play();
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

			// voix off finish
			voixOffAlreadyPlays = false;
			// si l'audio est joué alors on mets en pause sinon on fait rien
			//if (dialogueAveugle.isPlaying || dialogueRasta.isPlaying || dialogueCougars.isPlaying || dialogueBarman.isPlaying || dialogueMecBourre.isPlaying) 
			if (dialogueAveugle.isPlaying)
			{
				dialogueAveugle.Pause ();
				dialogueRasta.Pause ();
				dialogueCougars.Pause ();
				dialogueBarman.Pause ();
				dialogueMecBourre.Pause ();

			}
		}

        #endregion // PRIVATE_METHODS
    }
}
