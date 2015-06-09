//#define DEBUG_SCRIPT
//#define DEBUG_ANCHOR

using System.Collections;
using UnityEngine;

/* Ce script permet de bouger une image gràce au toucher ou à la souris.
 * Il permet également de magnétiser cette image à une cible
 * La cible est une gameObject
 * on peut ajuster la magnétisation en fonction du recouvrement de l'image par rapport à la target.
 * */
public class DragImage : MonoBehaviour
{
	/****************************************************/
	/*	 variables publics								*/
	/****************************************************/
	/* Dans l'inspector */




	// OBjet de la cible sur laquelle l'image va pouvoir s'attacher
	public GameObject target;

	// Permet de définir le taux de recouvrement pour laquelle l'image se colle (en pourcentage)
	// 0% -> l'image ne se colle pas
	// 50 % -> l'image se colle quand elle recouvre 50% de sa cible
	// 100% -> L'image se colle quand elle commence a recouvrir la cible
	// par défaut 20%
	[SerializeField]
	private float anchor = 20;
	
		
	public float Anchor
	{
		get { return this.anchor;}
		set { anchor = value;}
	}
	
	// true si l'image est sur l'ancre
	private bool isOnTarget;
	
	// permet de récupérer via un autre script si l'image est bien sur la cible
	public bool IsOnTarget
	{
		get { return this.isOnTarget;}
		set { isOnTarget = value;}
	}

	/****************************************************/
	/*	 variables privées								*/
	/****************************************************/


	private bool once = false;


	// dragging = true si l'utilisateur appuie sur l'image
	private bool dragging = false;


	//calcule la distance entre l'image et l'attache (l'ancre)
	private float distance;
	
	// distance entre l'image et le doigt
	private Vector3 v3Offset;

	// taille de l'image en 2D (permet de coller plus ou moins facilement)
	private float sizeImageTarget2D;	

	// position de la target
	private Vector3 targetPosition;




	// Initialise les paramètres comme le IsOnTarget, la taille de l'image et la position de l'ancre
	void OnMouseDown()
	{
		isOnTarget = false;
		dragging = true;
	}
	
	void OnMouseUp()
	{
		dragging = false;
		once = false;
	}


	/* permet de voir la target en debug mode */
	void OnDrawGizmos ()
	{
		if (renderer.isVisible == true) 
		{
			Vector3 sizeImageTarget = renderer.bounds.size;
			Gizmos.DrawLine (targetPosition - (sizeImageTarget / 2), targetPosition + (sizeImageTarget / 2) - new Vector3 (0, 0, sizeImageTarget.z));
			Gizmos.DrawLine (targetPosition - (sizeImageTarget / 2), targetPosition + (sizeImageTarget / 2) - new Vector3 (sizeImageTarget.x, 0, 0));
			Gizmos.DrawLine (targetPosition + (sizeImageTarget / 2), targetPosition - (sizeImageTarget / 2) + new Vector3 (0, 0, sizeImageTarget.z));
			Gizmos.DrawLine (targetPosition + (sizeImageTarget / 2), targetPosition - (sizeImageTarget / 2) + new Vector3 (sizeImageTarget.x, 0, 0));
		}
	}


	/* Permet de savoir le ratio entre entremelement des deux images
	 *  Ratio de 1 indique que les deux images se superposent parfaitement
	 * Ratio de 0 indique que les deux images ne se touche meme pas.
	 * P1 représente l'image Target et P2 l'image augmentée
	 * Retourne Ratio de 0 à 1
	 */


	float CalculRatio(Vector3 P1, Vector3 P2)
	{
		/* sizeImageTarget représente la taille de l'image dans toutes les dimensions et le 2D ne tiens compte que du x et z */
		Vector3 sizeImageTarget = renderer.bounds.size;
		float ratio = 0.0f;

		// on enlève la taille de la profondeur
		sizeImageTarget2D = sizeImageTarget.x * sizeImageTarget.z;

		// P1 est l'image toujouts à droite.
		// Si l'image de gauche passe à droite alors on inverse les P1 et P2
		if (P2.x < P1.x) 
		{
			Vector3 save = P1;
			P1 = P2;
			P2 = save;
		}

		// 
		if (P2.z < P1.z) 
		{
			P2.z += 2 * (P1.z - P2.z); 
		}
		
		if (P2.z < (P1.z + sizeImageTarget.z) &&
			P2.x < (P1.x + sizeImageTarget.x)) 
		{
			Vector3 C = P1 + sizeImageTarget;
			Vector3 D3 = C - P2;
			float A3 = D3.x * D3.z;
			ratio = A3 / sizeImageTarget2D;
		}		
		Debug.Log ("Ratio : " + ratio);
		return ratio;
	}







	void DragTheImage()
	{
		// calcul la distance entre la camera et l'image -> permet de ne pas rendre l'image plus petite ou plus grande par rappot au livre
		distance = Vector3.Distance(transform.position,    Camera.main.transform.position);
		
		
		// récupère la position du doigt pour la transposer dans le jeu
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 rayPoint = ray.GetPoint(distance);
		
		
		/* parmet le calcul du V3offset */
		if (once == false)
		{
			v3Offset = transform.position - ray.GetPoint(distance); 
			once = true;
			v3Offset.y = 0;
		}

		/* bouge l'image */
		Vector3 tempTrans =  rayPoint + v3Offset;
		tempTrans.y = targetPosition.y;
		transform.position = tempTrans;
	}


	/* si l'image se rapproche de son ancre alors elle sera automatiquement coller a elle
		 * compare la distance entre les deux images (l'image et l'ancre)
		 * puis regarde si la distance est inférieur au paramètre ancre (paramètrable par le programmeur) * la taille de l'image
		 * ainsi si l'ancre est à 100% alors l'image s'attachera avant de toucher le bord de sa target
		 */

	void MagnetTheImage(float ratio)
	{
		if (ratio > 1 - (Anchor / 100.0f)) 
		{
			transform.position = targetPosition;
			isOnTarget = true;
		} else 
		{
			isOnTarget = false;
		}
	}



	void Update()
	{
		/* position de l'ancre */
		targetPosition = target.transform.position;
	
		/* calcul qui permet de déterminer le ratio de remplissage de l'image */
		float ratio = CalculRatio (targetPosition, transform.position);


		if (dragging)
			DragTheImage();
		else		
			MagnetTheImage(ratio);
		

	}
}