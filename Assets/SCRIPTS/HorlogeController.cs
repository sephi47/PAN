using UnityEngine;
using System.Collections;
using Vuforia;

public class HorlogeController : MonoBehaviour 
{
	// charge les sprites des horloges
	public Sprite[] horloges; 
	public GameObject horloge1;
	public GameObject horloge2;
	/* flag = 1 si scene 1
	 *  = 2 si scene 2...
	 * */
	private int flag;

	// permet de ne rien faire pendant l'animation de l'horloge
	private bool inAnimation;





	// tous les flags
	// flag de 19h
	public DefaultTrackableEventHandler aveugleScene1Trackable;
	public BAM_TrackableEventHandler cougarsScene1Trackable;
	public TargetBarTrackableEventHandler mecBourreScene1Trackable;
	public DefaultTrackableEventHandler lustreScene1Trackable;
	public RASTA_TrackableEventHandler mecBourreScene2Trackable;
	public Cougars_TrackableEventHandler cougarsScene2Trackable;



	// flag de 19h15
	public DefaultTrackableEventHandler aveugleScene2Trackable;

	// flag de 19h30


	// flag de 19h45


	// flag de 20h PAN!


	// Use this for initialization
	void Start () 
	{
		inAnimation = false;
		// charge les sprites dans horloges
		horloges = Resources.LoadAll<Sprite> ("Horloge");
		horloge1.GetComponent<UnityEngine.UI.Image> ().sprite = horloges[0];
		horloge2.SetActive(false);
		flag = 1;



	
	}

	/* défini quel horloge est a enlever et quel doit etre montré */

	void HorlogeChoice (Sprite spr)
	{
		// si l'horloge 1 est null alors il faut faire apparaitre la 1 sinon c'est la deux
		if (!horloge1.activeSelf && horloge2.activeSelf) 
		{
			Fade (horloge2, horloge1, spr);
		} 
		else if (!horloge2.activeSelf && horloge1.activeSelf) 
		{
			Fade (horloge1, horloge2, spr);
		} 
		else
		{
			Debug.Log ("_____ WARNING ERROR _______");
			Fade (horloge2, horloge1, spr);
		}


	}

	IEnumerator FadeIn(GameObject horloge)
	{
		inAnimation = true;
		for (float f = 0f; f <= 1f; f += 0.1f) 
		{
			horloge.GetComponent<UnityEngine.UI.Image> ().color = new Vector4(1.0f, 1.0f, 1.0f, f);
			//horloge.GetComponent<UnityEngine.UI.Image> ().color = Color.Lerp (oldColor, alphaColor, Time.time);
			yield return new WaitForSeconds(0.1f);
		}
		// force la couleur -> la coroutine ne donne pas un resultat juste...
		horloge.GetComponent<UnityEngine.UI.Image> ().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
		inAnimation = false;

	}

	IEnumerator FadeOut(GameObject horloge)
	{

		for (float f = 1f; f >= 0f; f -= 0.1f) 
		{
			horloge.GetComponent<UnityEngine.UI.Image> ().color = new Vector4(1.0f, 1.0f, 1.0f, f);
			yield return new WaitForSeconds(0.1f);
		}
		// desactiver l'ancienne horloge
		// force la couleur -> la coroutine ne donne pas un resultat juste...
		horloge.GetComponent<UnityEngine.UI.Image> ().color = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
		horloge.SetActive(false);

	}
	

	// effet de fade out fade in avec l'ancienne horloge et la nouvelle
	void Fade(GameObject oldHorloge, GameObject newHorloge, Sprite spr)
	{
		Color oldColor;
		// associer le sprite à la nouvelle horloge
		newHorloge.GetComponent<UnityEngine.UI.Image> ().sprite = spr;
		// mettre la couleur du newHorloge à 0
		oldColor = newHorloge.GetComponent<UnityEngine.UI.Image> ().color;
		newHorloge.GetComponent<UnityEngine.UI.Image> ().color = new Vector4 (oldColor.r, oldColor.g, oldColor.b, 0.0f);
		// activer la newhorloge
		newHorloge.SetActive(true);
		// faire un fade out de l'ancienne horloge
		StartCoroutine (FadeOut (oldHorloge));
		// faire un fade in de la nouvelle
		StartCoroutine (FadeIn (newHorloge));

	}
	
	// Update is called once per frame
	void Update () 
	{

		/* si on est sur le premier quart d'heure alors je charge l'horloge 01
		 * et ainsi de suite pour les autres horloges
		 * Il faut faire des flags dans chaque images target que je lève et baisse en fonction de l'image qu'on voit
		 * s'il n'y a pas de flag alors je garde l'ancienne horloge
		 * faire un effet de fade pour chaque changement d'horloge
		 * */
		/* scene 1 => 7h00 ==> horleges[0] */
		if (inAnimation == false) 
		{
			if ((aveugleScene1Trackable.IsDetected || lustreScene1Trackable.IsDetected || mecBourreScene1Trackable.IsDetected || cougarsScene1Trackable.IsDetected) && flag != 1) 
			{
				flag = 1;
				HorlogeChoice (horloges [0]);
			}
			else if ((aveugleScene2Trackable.IsDetected || cougarsScene2Trackable.IsDetected || mecBourreScene2Trackable.IsDetected) && flag != 2) 
			{
				flag = 2;
				HorlogeChoice (horloges [1]);
			} 
		}



	//	GetComponent<Image> ().sprite = Horloge01;
	
	}
}
