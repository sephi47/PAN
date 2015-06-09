using UnityEngine;
using System.Collections;

/* Script qui permet la gestion du bouton Jouer du Menu */
public class BoutonLIRE : MonoBehaviour {


	//public GUITexture lireClicked;
	//public GUIText loadingDots;

		
	//appelé au click de la souris
	public void LoadCamera(){
		Debug.Log ("click!");
		//guiTexture.enabled = false;
		//lireClicked.enabled = true;
		//loadingDots.enabled = true;
		//Application.LoadLevelAsync("cameraRA"); //on charge la scène de jeu
		//StartCoroutine("Loading");
		Application.LoadLevel("cameraRA"); //on charge la scène de jeu
	}


	
	
	
}
