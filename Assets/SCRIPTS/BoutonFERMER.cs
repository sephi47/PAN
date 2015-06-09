using UnityEngine;
using System.Collections;

/* Script qui permet la gestion du bouton Jouer du Menu */
public class BoutonFERMER : MonoBehaviour {
	
	
	//appelé au click de la souris
	public void Quitter(){
		Debug.Log ("click!");
		Application.Quit();
		
	}
	

	
	
}
