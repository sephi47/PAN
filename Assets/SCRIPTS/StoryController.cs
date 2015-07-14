using UnityEngine;
using System.Collections;
using Vuforia;

public class StoryController : MonoBehaviour {


	public GameObject bandeauLustre;
//	public GameObject lesAveugles;
	//public GameObject mecBourre;
	public GameObject augmentationLustre;
	//public GameObject cougars;
	//public GameObject end;
	private Lustre_Controller lustreController;
	public GameObject[] imageTargetStorys;

	private bool oneTime;


	void DeactivateTargetWithTag ()
	{
		foreach (GameObject imageTargetStory in imageTargetStorys) {
			MonoBehaviour[] scripts = imageTargetStory.GetComponents<MonoBehaviour> ();
			foreach (MonoBehaviour script in scripts) {
				script.enabled = false;
			}
			imageTargetStory.SetActive (false);
		}
	}

	void ActivateTargetWithTag ()
	{
		foreach (GameObject imageTargetStory in imageTargetStorys) {
			MonoBehaviour[] scripts = imageTargetStory.GetComponents<MonoBehaviour> ();
			foreach (MonoBehaviour script in scripts) {
				script.enabled = true;
			}
			imageTargetStory.SetActive (true);
		}
	}

	// Use this for initialization
	IEnumerator Disable(float delay)
	{
		yield return new WaitForSeconds(delay);
		DeactivateTargetWithTag (); 
	}

	void Start () 
	{
		oneTime = false;
		imageTargetStorys = GameObject.FindGameObjectsWithTag ("ImageTargetStory");
		StartCoroutine (Disable (1.0f));

		// active seulement le bandeau
		// tant que le luste n'est pas flashé on ne commence pas l'histoire
		bandeauLustre.SetActive (true);
	//	lesAveugles.SetActive (false);
	//	mecBourre.SetActiveRecursively (false);
	//	cougars.SetActive (false);
	//	end.SetActive (false);	
		lustreController = augmentationLustre.GetComponent<Lustre_Controller> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		// le lustre viens d'etre flashé.
		// on active tout 
		// sauf end (actif pour le moment
		if (lustreController.IsFlashed & !oneTime) 
		{
			bandeauLustre.SetActive (false);
			ActivateTargetWithTag();
			oneTime = true;
	

		}

		// une fois toutes les intéractions faite on active la fin




	
	}
}
