using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(DragImage))]
public class DragEditor : Editor 
{

	//SerializedProperty TempActiveDepth;
	public override void OnInspectorGUI()
	{
		DragImage myDragUI = (DragImage)target;

		myDragUI.target = (GameObject)EditorGUILayout.ObjectField("Target Game Object",myDragUI.target, typeof(GameObject) );
		myDragUI.Anchor = EditorGUILayout.Slider ("Anchor Distance in %", myDragUI.Anchor, 0.0f, 100.0f);
		EditorGUILayout.LabelField ("Is image on target?", myDragUI.IsOnTarget.ToString ());
		//myDragUI.ActiveDepth = EditorGUILayout.Toggle ("Active Depth when drag", myDragUI.ActiveDepth);
		EditorGUILayout.HelpBox(
			"Ce script permet de bouger la position de l'image sur les axes x et z (en 2D) \n" +
 			"Il permet également d'attacher une image à une ancre et de lever un drapeau  \n" +
			"les données d'entrées sont : \n " +
			"Game objet de l'ancre \n " +
			"la tolérance de l'ancre en % de la taille de l'image -> 100% signifie que l'image sera aimanté dès qu'elle touchera un de ses bords \n " +
			"50 % signifie quelle sera attaché quand un de ses bord touchera le milieu de l'ancre... \n", MessageType.Info);
	}
}

