using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Objectif : MonoBehaviour{

	[HideInInspector]
	public string description = "";
	[HideInInspector]
	public Dictionary<int, object> tableDesObjectifs = new Dictionary<int, object>();
	[HideInInspector]
	public String learning;

	// Vide l'objectif
	public void clear(){
		description = "";
		tableDesObjectifs.Clear ();
	}

	// Retourne vrai si l'objectif est rempli, faux sinon
	public Boolean isComplete()
	{ 
		return (tableDesObjectifs.Count > 0) ? false : true;
		/*for (int cptObjectif = 0; cptObjectif < tableDesObjectifs.Count; ++cptObjectif) 
		{
			if(progression[cptObjectif] < tableDesObjectifs[cptObjectif])
				return false;
		}

		return true;*/
	}
}
