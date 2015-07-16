using UnityEngine;
using System.Collections;

public class ButtonPage_Controller : MonoBehaviour {

	#region PRIVATE_MEMBER_VARIABLES

	private bool voixOffAlreadyPlays;

	#endregion // PRIVATE_MEMBER_VARIABLES


	#region PUBLIC_MEMBER_VARIABLES

	public AudioSource voixOff_Bar;
/*	public AudioSource bourreDialogue;
	public AudioSource cougarsDialogue;
	public AudioSource barmanDialogue;
	public AudioSource rastaDialogue;
	public AudioSource aveuglesDialogue;
	public Renderer contourRenderer;*/


	#endregion // PUBLIC_MEMBER_VARIABLES
	// Use this for initialization
	void Start () 
	{
		voixOffAlreadyPlays = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{


		// si l'image est visible et que le bool voix off n'a pas déjà été joué (évite la répétition) et qu'on est pas en train de jouer
		if (renderer.isVisible) 
		{
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
		}
		else
			voixOffAlreadyPlays = false;







	}
}
