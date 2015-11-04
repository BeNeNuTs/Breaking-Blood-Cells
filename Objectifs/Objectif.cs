using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Objectif : MonoBehaviour{

	[HideInInspector]
	public string description = "";
	[HideInInspector]
	public Dictionary<int, object> tableDesObjectifs = new Dictionary<int, object>();

	// Vide l'objectif
	public void clear(){
		description = "";
		tableDesObjectifs.Clear ();
	}

	// Retourne vrai si l'objectif est rempli, faux sinon
	Boolean isComplete(){ return (tableDesObjectifs.Count > 0) ? false : true; }
}
