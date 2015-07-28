using UnityEngine;
using System.Collections;
using Vuforia;

public class boucheController : MonoBehaviour {

	public GameObject bouche1;
	public GameObject bouche2;
	public GameObject blablabla;
	public Cougars_TrackableEventHandler boucheTrackable;
	private bool once;


	private GameObject t1;
	private GameObject t2;
	private GameObject t3;
	private GameObject t4;

	// Use this for initialization
	void Start () 
	{
		once = false;
	}

	IEnumerator ActivateBouche()
	{
		t1 = Instantiate (bouche1) as GameObject ;
		t1.transform.parent = transform.parent;
		yield return new WaitForSeconds (2.0f);
		t2 = Instantiate (bouche2) as GameObject ;
		t2.transform.parent = transform.parent;
		yield return new WaitForSeconds (2.0f);
		t3 = Instantiate (bouche1) as GameObject ;
		t3.transform.parent = transform.parent;
		yield return new WaitForSeconds (2.0f);
		t4 = Instantiate (bouche2) as GameObject ;
		t4.transform.parent = transform.parent;
		yield return new WaitForSeconds (2.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (boucheTrackable.IsDetected)
		{
			if (!once)
			{
				StartCoroutine (ActivateBouche());
				blablabla.SetActive(true);
				once = true;
			}
		}
		else
		{
			once = false;
			blablabla.SetActive(false);
			Destroy(t1);
			Destroy(t2);
			Destroy(t3);
			Destroy(t4);


		}	
	}
}
