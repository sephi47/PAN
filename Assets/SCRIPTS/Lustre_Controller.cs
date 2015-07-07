using UnityEngine;
using System.Collections;

public class Lustre_Controller : MonoBehaviour {

	/*public GameObject lustreCasse;
	public GameObject lustre;
	public GameObject pan;
	private Renderer lustreCasseRenderer;
	private Renderer lustreRenderer;
	private Renderer panRenderer;*/
	public AudioSource son;
	//public float speed;
	//private bool end;
	//float alphaChannel;


	// Use this for initialization
	/*void Start () 
	{
		lustreCasseRenderer = lustreCasse.GetComponent<Renderer> ();
		lustreRenderer = lustre.GetComponent<Renderer> ();
		panRenderer = pan.GetComponent<Renderer> ();

		
		alphaChannel = 0.0f;
		lustreCasseRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, alphaChannel);
		panRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
	}*/

	/*IEnumerator RendererPAN (float delayEnable, float delayDisable)
	{
		
		yield return new WaitForSeconds(delayEnable);
		panRenderer.material.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		pan.transform.position += Random.insideUnitSphere * 10 ;
		yield return new WaitForSeconds(delayDisable);
		panRenderer.enabled = false;
	}


	IEnumerator RendererLustreCasse (float delayEnable, float delayDisable)
	{
		lustreCasseRenderer.material.color = new Color(1.0f,1.0f,1.0f,alphaChannel);
		Debug.Log (lustreCasseRenderer.material.color);
		yield return new WaitForSeconds(delayEnable);
		if (!end) 
		{
			alphaChannel += Time.deltaTime;
			if (alphaChannel >= 1)
				alphaChannel = 1;
		}

		yield return new WaitForSeconds(delayDisable);
		end = true;
		alphaChannel -= Time.deltaTime;
		if (alphaChannel <= 0)
			alphaChannel = 0;
		//lustreCasseRenderer.material.color = new Color(1.0f,1.0f,1.0f,alphaChannel);
	}*/
	// Update is called once per frame
	void FixedUpdate () 
	{

		// si l'image augmenté est visible alors on joue le son
		if (renderer.isVisible) 
		{
			
		//	StartCoroutine (RendererLustreCasse (3.0f, 17.0f));
		//	StartCoroutine (RendererPAN(1.2f, 1.0f));
			//si la voix off n'est pas en train de jouer, on la lance, sinon c'est qu'elle est déjà en cours!
			if (!son.isPlaying) 
			{
			//	end = false;
				Handheld.Vibrate ();
				son.Play ();			
			}
		}
	}
}
