﻿using UnityEngine;
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

	// met à jour un objectif en fonction du second paramètre
	void updateGoal(int label, int type = 0){
		if (objectifCourant.tableDesObjectifs.ContainsKey (label)) {
			int tmp = (int) objectifCourant.tableDesObjectifs[label];
			--tmp;
			if(tmp != 0)
				objectifCourant.tableDesObjectifs[label] = tmp;
			else
				objectifCourant.tableDesObjectifs.Remove(label);
			
			//TODO : isComplete
		}
	}

	void updateGoal(int label, float type = 0){
		if (objectifCourant.tableDesObjectifs.ContainsKey (label)) {
			float tmp = (float) objectifCourant.tableDesObjectifs[label];
			tmp -= 1; // VALEUR A DEFINIR PLUS EN DETAIL
			if(tmp != 0.0f)
				objectifCourant.tableDesObjectifs[label] = tmp;
			else
				objectifCourant.tableDesObjectifs.Remove(label);
			
			//TODO : isComplete
		}
	}

	void updateGoal(int label, Vector2 pos){
		if (objectifCourant.tableDesObjectifs.ContainsKey (label)) {
			Vector2 obj = (Vector2)(objectifCourant.tableDesObjectifs[label]);
			if((pos.x > obj.x-5.0f || pos.x < obj.x+5.0f) && (pos.y > obj.y-5.0f || pos.y < obj.y+5.0f)){ // VARIABLE D'ECART A DEFINIR
				objectifCourant.tableDesObjectifs.Remove(label);
			}

			//TODO : isComplete
		}
	}

	// Charge l'objectif id du fichier XML au chemin path dans l'objectif courant
	void load(string path, int id){
		// Variables servant à récupérer l'information
		int tag = 0;
		Vector2 coords = new Vector2 ();
		Regex coordRegexX = new Regex (@"[0-9]+,[0-9]+;$");
		Match coordMatchX;
		Regex coordRegexY = new Regex (@";[0-9]+,[0-9]+$");
		Match coordMatchY;

		//Variable de test
		Boolean aTrouve = false;

		XmlTextReader myXmlTextReader = new XmlTextReader (path);
		while(myXmlTextReader.Read()){
			if(myXmlTextReader.IsStartElement()){
				if (int.Parse(myXmlTextReader.GetAttribute("id")) == id){
					objectifCourant.description = myXmlTextReader.GetAttribute("description");
					tag = int.Parse(myXmlTextReader.GetAttribute("tag"));
					if(tag == 0 || tag == 1 || tag == 3 || tag == 4 || tag == 6){ // Si on a ces tags, on a forcément un int dans value
						objectifCourant.tableDesObjectifs.Add(tag, (int)int.Parse(myXmlTextReader.GetAttribute("value")));
					} else if(tag == 2){ // Pour le tag 2, on récupère un vector2 dans value sous forme "float;float"
						coordMatchX = coordRegexX.Match(myXmlTextReader.GetAttribute("value"));
						coordMatchY = coordRegexY.Match(myXmlTextReader.GetAttribute("value"));
						coords.Set(float.Parse(coordMatchX.Value), float.Parse(coordMatchY.Value));
						objectifCourant.tableDesObjectifs.Add(tag, (Vector2)coords);
					} else if(tag == 5){ // Pour le tag 5 on a un float dans value
						objectifCourant.tableDesObjectifs.Add(tag, (float)float.Parse(myXmlTextReader.GetAttribute("value")));
					}

					aTrouve = true;
				}
			}
		}

		if (!aTrouve)
			Debug.Log ("N'a pas trouvé.\n");
	}
}