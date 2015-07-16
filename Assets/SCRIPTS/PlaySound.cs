using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioSource audioSound;
	
	IEnumerator Delay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		audioSound.Play ();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (renderer.isVisible) 
		{
			Delay(2.0f);
		}

	
	}
}
