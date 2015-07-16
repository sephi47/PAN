using UnityEngine;
using System.Collections;

public class Lustre_Controller : MonoBehaviour {

	// pan et pan renderer correspond à la bulle PAN qui s'afficechera pendant le coup de feu
	public GameObject pan;
	private Renderer panRenderer;

	// corespond au son du narrateur
	public AudioSource son;



	
	// passe à true si on scan le lustre et reste à true ensuite. au démarrage = false jusqu'au flashage
	private bool isFlashed;

	// permet de récupérer via un autre script si l'image est bien sur la cible
	public bool IsFlashed
	{
		get { return this.isFlashed;}
		set { isFlashed = value;}
	}

	void Init()
	{

		// alpha du pan = transparent
		panRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	}
	// Use this for initialization
	void Start () 
	{
		
		this.isFlashed = false;
		panRenderer = pan.GetComponent<Renderer> ();
		Init ();



	}

	IEnumerator RendererPAN (float delayEnable)
	{
		
		yield return new WaitForSeconds(delayEnable);
		panRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		pan.transform.position += Random.insideUnitSphere * 10 ;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{

		// si l'image augmenté est visible et que on est pas entrain de jouer l'animation alors on  play
		if (renderer.isVisible) 
		{
			// leve le drapeaux pour dire que l'animation a été joué au moins une fois
			this.isFlashed = true;


		

			//si la voix off n'est pas en train de jouer, on la lance, sinon c'est qu'elle est déjà en cours!
			if (!son.isPlaying) 
			{
				// coroutine pour afficher PAN au moment du coup de feu
				StartCoroutine (RendererPAN(1.2f));
				Handheld.Vibrate ();
				son.Play ();			
			}

		}
		else
		{
			if (!son.isPlaying) 
			{
				Init ();
			}
		}
	}
}
