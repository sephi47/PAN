using UnityEngine;
using System.Collections;
using Vuforia;

public class StoryController : MonoBehaviour {


	public GameObject bandeauLustre;
	public GameObject bandeauDemmarage;
	public float delayForRead = 5.0f;
	public GameObject augmentationLustre;
	private Lustre_Controller lustreController;
	public GameObject[] imageTargetStorys;

	private bool oneTime;
	private bool oneTime2;


	/* Désactive les images target et les scripts pour ne que rien ne fonctionne tant que l'utilisateur ne scan pas le lustre */
	void DeactivateTargetWithTag ()
	{
		// parcours le tableau de tout les objets avec le tag ImageTargetStory
		foreach (GameObject imageTargetStory in imageTargetStorys) {
			//liste tous les scripts attachés au gameobjet et les désactivent
			MonoBehaviour[] scripts = imageTargetStory.GetComponents<MonoBehaviour> ();
			foreach (MonoBehaviour script in scripts) {
				script.enabled = false;
			}
			imageTargetStory.SetActive (false);
		}
	}

	// active tout les scripts et les images target une fois que le lustre est vue
	void ActivateTargetWithTag ()
	{
		// parcours le tableau de tout les objets avec le tag ImageTargetStory
		foreach (GameObject imageTargetStory in imageTargetStorys) {
			MonoBehaviour[] scripts = imageTargetStory.GetComponents<MonoBehaviour> ();
			//liste tous les scripts attachés au gameobjet et les activent
			foreach (MonoBehaviour script in scripts) {
				script.enabled = true;
			}
			imageTargetStory.SetActive (true);
		}
	}

	IEnumerator Slider(float delay)
	{
		yield return new WaitForSeconds(delay);


		if (bandeauDemmarage.transform.eulerAngles.x >= 90)
			bandeauDemmarage.SetActive (false);
		else
			bandeauDemmarage.transform.Rotate(Vector3.right * Time.deltaTime*100);

		if (!oneTime2) 
		{
			bandeauLustre.SetActive (true);
			oneTime2 = true;
		}
		
		Debug.Log (bandeauLustre.transform.eulerAngles.x);
		if (bandeauLustre.transform.eulerAngles.x <= 180.0f)
			bandeauLustre.transform.Rotate(Vector3.left * Time.deltaTime*100);


	}



	// delay for disable all augmentation (sinon ça ne fonctionne pas quand on réactive)
	IEnumerator Disable(float delay)
	{
		yield return new WaitForSeconds(delay);
		DeactivateTargetWithTag (); 
	}

	// Use this for initialization
	void Start () 
	{
		// Assure la condition de l'update une seul fois, évite les ralentissements probable
		oneTime = false;
		oneTime2 = false;

		// cherche tous les gameobject avec le tag
		imageTargetStorys = GameObject.FindGameObjectsWithTag ("ImageTargetStory");

		// attend 1 secondes pour désactiver les objets sinon il est impossible de les réactiver...
		StartCoroutine (Disable (1.0f));

		bandeauDemmarage.SetActive (true);
		// active seulement le bandeau

		// tant que le luste n'est pas flashé on ne commence pas l'histoire
	
		lustreController = augmentationLustre.GetComponent<Lustre_Controller> ();

	}





	
	// Update is called once per frame
	void Update () 
	{

		
		StartCoroutine(Slider (delayForRead));
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
