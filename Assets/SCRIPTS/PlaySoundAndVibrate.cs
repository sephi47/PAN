using UnityEngine;
using System.Collections;

public class PlaySoundAndVibrate : MonoBehaviour {


	public AudioSource son;
	
	// Update is called once per frame
	void Update () 
	{
		// si l'image augmenté est visible alors on joue le son
		if (renderer.isVisible)
		{
			//si la voix off n'est pas en train de jouer, on la lance, sinon c'est qu'elle est déjà en cours!
			if (!son.isPlaying) 
			{
				Handheld.Vibrate ();
				son.Play ();
			}
	
		}
	}
}
