using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

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
			if(label == 0 || label == 1 || label == 3 || label == 4 || label == 6){
				tmp = (int) objectifCourant.tableDesObjectifs[label];
				--tmp;
				if(tmp != 0)
					objectifCourant.tableDesObjectifs[label] = tmp;
				else
					objectifCourant.tableDesObjectifs.Remove(label);
				
				//TODO : isComplete
			} else if(label == 2){
				
			}
		}
	}

	// Charge l'objectif id du fichier XML au chemin path dans l'objectif courant
	void load(string path, int id){
		int tag = 0;
		objectifCourant.clear ();
		XmlTextReader myXmlTextReader = new XmlTextReader (path);
		while(myXmlTextReader.Read()){
			if(myXmlTextReader.IsStartElement()){
				if (int.Parse(myXmlTextReader.GetAttribute("id")) == id){
					objectifCourant.description = myXmlTextReader.GetAttribute("description");
					tag = int.Parse(myXmlTextReader.GetAttribute("tag"));
					if(tag == 0 || tag == 1 || tag == 3 || tag == 4 || tag == 6){
						objectifCourant.tableDesObjectifs.Add(tag, (int)int.Parse(myXmlTextReader.GetAttribute("value")));
					} else if(tag == 2){

					}
				}
			}
		}
	}
}
