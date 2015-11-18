using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

public class ObjectifManager : MonoBehaviour {
	
	Objectif currentObjective = new Objectif();
	[HideInInspector]
	public enum tag {KILLBACTERIA = 0, KILLVIRUS = 1, GOTO = 2, ANALYZEBACTERIA = 3, ANALYZEVIRUS = 4, SURVIVE = 5, CREATEANTIBODIES = 6};
	
	public String xmlPath;

	private static int idObjectif = 0;
	private const float ECART_POSITION = 5.0f;

/*	void Start(){
		//Debug.Log (File.Exists(Application.dataPath+"\\Xml\\xmlTest.xml"));
		load (Application.dataPath + "\\Xml\\xmlTest.xml", 0);
		foreach(int i in objectifCourant.tableDesObjectifs.Keys){
			Debug.Log (i + " -> " + objectifCourant.tableDesObjectifs[i]);
		}
	}*/

	void OnLevelWasLoaded(int level){
		xmlPath = Application.dataPath+"\\Xml\\"+level+".xml";
	}

	// Met à jour un objectif en fonction du second paramètre

	// Objectifs qui se décrémentent par entier (tuer bactérie, etc...)
	void updateGoal(int label, int type = 0){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			int tmp = (int) currentObjective.tableDesObjectifs[label];
			--tmp;
			if(tmp != 0)
				currentObjective.tableDesObjectifs[label] = tmp;
			else
				currentObjective.tableDesObjectifs.Remove(label);
			
			if(currentObjective.isComplete()){
				currentObjective.clear();
				++idObjectif;
				load (xmlPath, idObjectif);
			}
		}
	}

	// Objectifs qui se décrémentent par float (time, etc...)
	void updateGoal(int label, float type = 0){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			float tmp = (float) currentObjective.tableDesObjectifs[label];
			tmp -= 1; // VALEUR A DEFINIR PLUS EN DETAIL
			if(tmp != 0.0f)
				currentObjective.tableDesObjectifs[label] = tmp;
			else
				currentObjective.tableDesObjectifs.Remove(label);
			
			if(currentObjective.isComplete()){
				currentObjective.clear();
				++idObjectif;
				load (xmlPath, idObjectif);
			}
		}
	}

	// Objectifs qui se décrémentent par position (vecteurs etc...)
	void updateGoal(int label, Vector2 pos){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			Vector2 obj = (Vector2)(currentObjective.tableDesObjectifs[label]);
			if((pos.x > obj.x-ECART_POSITION || pos.x < obj.x+ECART_POSITION) && (pos.y > obj.y-ECART_POSITION || pos.y < obj.y+ECART_POSITION)){ // VARIABLE D'ECART A DEFINIR
				currentObjective.tableDesObjectifs.Remove(label);
			}

			if(currentObjective.isComplete()){
				currentObjective.clear();
				++idObjectif;
				load (xmlPath, idObjectif);
			}
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
			if(myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "objectif"){
				if (int.Parse(myXmlTextReader.GetAttribute("id")) == id){
					aTrouve = true;
					currentObjective.description = myXmlTextReader.GetAttribute("description");
					tag = int.Parse(myXmlTextReader.GetAttribute("tag"));
					if(tag == 0 || tag == 1 || tag == 3 || tag == 4 || tag == 6){ // Si on a ces tags, on a forcément un int dans value
						currentObjective.tableDesObjectifs.Add(tag, (int)int.Parse(myXmlTextReader.GetAttribute("value")));
						break;
					} else if(tag == 2){ // Pour le tag 2, on récupère un vector2 dans value sous forme "float;float"
						coordMatchX = coordRegexX.Match(myXmlTextReader.GetAttribute("value"));
						coordMatchY = coordRegexY.Match(myXmlTextReader.GetAttribute("value"));
						coords.Set(float.Parse(coordMatchX.Value), float.Parse(coordMatchY.Value));
						currentObjective.tableDesObjectifs.Add(tag, (Vector2)coords);
						break;
					} else if(tag == 5){ // Pour le tag 5 on a un float dans value
						currentObjective.tableDesObjectifs.Add(tag, (float)float.Parse(myXmlTextReader.GetAttribute("value")));
						break;
					}
				}
			}
		}
		if (!aTrouve)
			Debug.Log ("N'a pas trouvé.\n");
	}
}
