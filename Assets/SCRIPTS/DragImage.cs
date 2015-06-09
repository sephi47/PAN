//#define DEBUG_SCRIPT
#define DEBUG_ANCHOR

using System.Collections;
using UnityEngine;

public class DragImage : MonoBehaviour
{
	private bool once = false;
	// dragging = true si l'utilisateur appuie sur l'image
	private bool dragging = false;

	//calcule la distance entre l'image et l'attache (l'ancre)
	private float distance;
	
	// distance entre l'image et le doigt
	private Vector3 v3Offset;

	//anchor
	// permet de déterminer la position sur laquelle l'image va rester coller
	public GameObject target;	

	// taille de l'image en 2D (permet de coller plus ou moins facilement)
	private float tailleImage2D;	

	// position de l'ancre
	private Vector3 anchorPosition;

	[SerializeField]
	// détermine la distance pour laquelle l'image sera attaché à l'ancre en pourcentage
	private float anchor = 20;

	// true si l'image est sur l'ancre
	private bool isOnTarget;
	
	public float Anchor
	{
		get { return this.anchor;}
		set { anchor = value;}
	}
	
	// permet de récupérer via un autre script si l'image est bien sur la cible
	public bool IsOnTarget
	{
		get { return this.isOnTarget;}
		set { isOnTarget = value;}
	}

	// Initialise les paramètres comme le IsOnTarget, la taille de l'image et la position de l'ancre
	void OnMouseDown()
	{
		isOnTarget = false;
		tailleImage2D = renderer.bounds.size.magnitude;
		anchorPosition = target.transform.position;
		dragging = true;
	}
	
	void OnMouseUp()
	{
		dragging = false;
		once = false;
	}
	
	void Update()
	{
		float anchor_product = 1.0f;
		if (dragging)
		{
			// si l'image est sur la target alors elle y reste
			if (isOnTarget == false)
			{
				
				// calcul la distance entre la camera et l'image -> permet de ne pas rendre l'image plus petite ou plus grande par rappot au livre
				distance = Vector3.Distance(transform.position,    Camera.main.transform.position);
				#if DEBUG_SCRIPT
			//		Debug.Log ("camera position : " + Camera.main.transform.position);
					Debug.Log ("image position before moving : " + transform.position);
					//Debug.Log ("distance : " + distance);
					//Debug.Log ("input mouse position : " + Input.mousePosition);
				#endif
				#if DEBUG_ANCHOR
					Debug.Log ("Taille image : " + tailleImage2D);
					Debug.Log ("anchorPosition : " + anchorPosition);
					Debug.Log ("image Position : " + transform.position);
					Debug.Log ("distance position : " + (anchorPosition - transform.position));
					Debug.Log ("magnitude image : " + (tailleImage2D));
					Debug.Log ("anchor * 2D : " + ((anchor/(100f*anchor_product)) * tailleImage2D));
					Debug.Log ("distance magnitude : " + (anchorPosition-transform.position).magnitude);
				#endif

				// récupère la position du doigt pour la transposer dans le jeu
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 rayPoint = ray.GetPoint(distance);

				#if DEBUG_SCRIPT
					Debug.Log ("raypoint : " + rayPoint);
				#endif

				// permet de garder la meme proportion entre l'image et le livre meme si on recule avec la caméra
				//__rayPoint.y = transform.position.y;

				/* parmet le calcul du V3offset */
				if (once == false)
				{
					v3Offset = transform.position - ray.GetPoint(distance); 
					once = true;
					v3Offset.y = 0;
				}
				#if DEBUG_SCRIPT
					Debug.Log ("v3Offset : " + v3Offset);
				#endif
				
				/* si l'image se rapproche de son ancre alors elle sera automatiquement coller a elle
				 * compare la distance entre les deux images (l'image et l'ancre)
				 * puis regarde si la distance est inférieur au paramètre ancre (paramètrable par le programmeur) * la taille de l'image
				 * ainsi si l'ancre est à 100% alors l'image s'attachera avant de toucher le bord de sa target
				 */

				if (((anchorPosition - transform.position).magnitude) < ((anchor / (100f * anchor_product)) * tailleImage2D)) {
					transform.position = anchorPosition;
					isOnTarget = true;
				} else {
					isOnTarget = false;
					Vector3 tempTrans =  rayPoint + v3Offset;
					tempTrans.y = anchorPosition.y;
					transform.position = tempTrans;
				}



				#if DEBUG_SCRIPT
				Debug.Log ("image position after moving : " + transform.position);
				#endif
			}
		}
	}
}