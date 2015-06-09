using UnityEngine;
using System.Collections;

public class BoutonMENU : MonoBehaviour {

	//appelé au click de la souris
	public void LoadMenu(){
		Debug.Log ("click!");
		Application.LoadLevel("menu");
		
	}

}
