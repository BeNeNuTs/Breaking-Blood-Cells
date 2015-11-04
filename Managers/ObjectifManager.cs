using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class ObjectifManager : MonoBehaviour {
	
	Objectif objectifCourant = new Objectif();
	[HideInInspector]
	public enum tag {KILLBACTERIA = 0, KILLVIRUS = 1, GOTO = 2, ANALYZEBACTERIA = 3, ANALYZEVIRUS = 4, SURVIVE = 5, CREATEANTIBODIES = 6};

	// Use this for initialization
	void Start () {

	}

	// Décrémente le label d'un objectif et le supprime s'il est complet
	// Si le label n'est pas présent dans la table, la fonction ne fait rien
	void updateGoal(int label){
		if(objectifCourant.tableDesObjectifs.ContainsKey(label)){
			int tmp = 0;
			switch(label){
			case 0:
				tmp = (int) objectifCourant.tableDesObjectifs[label];
				--tmp;
				if(tmp != 0)
					objectifCourant.tableDesObjectifs[label] = tmp;
				else
					objectifCourant.tableDesObjectifs.Remove(label);

				//TODO : isComplete
				break;
			case 1:
				tmp = (int) objectifCourant.tableDesObjectifs[label];
				--tmp;
				if(tmp != 0)
					objectifCourant.tableDesObjectifs[label] = tmp;
				else
					objectifCourant.tableDesObjectifs.Remove(label);

				//TODO : isComplete
				break;
			case 2:
				//TODO : isComplete
				break;
			case 3:
				//TODO : isComplete
				break;
			case 4:
				//TODO : isComplete
				break;
			case 5:
				//TODO : isComplete
				break;
			case 6:
				//TODO : isComplete
				break;
			default:
				break;
			}
		}
	}

	// Charge un fichier XML dans l'objectif courant
	void load(string path, int id){
		XmlTextReader myXmlTextReader;
	}
}
