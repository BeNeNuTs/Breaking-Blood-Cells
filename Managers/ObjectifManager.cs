using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

public class ObjectifManager : MonoBehaviour {
	
	Objectif currentObjective = new Objectif();
	Objectif initialObjective = new Objectif();

	[HideInInspector]
	public enum tag {KILLBACTERIA = 0, KILLVIRUS = 1, GOTO = 2, ANALYZEBACTERIA = 3, ANALYZEVIRUS = 4, SURVIVE = 5, CREATEANTIBODIES = 6};

	public GameObject ObjectifName;
	public GameObject Progression;

	public String xmlPath;

	private static int idObjectif = 0;

	private const float ECART_POSITION = 5.0f;

	public static int ObjectifId;


	void Start(){
		//Debug.Log (File.Exists(Application.dataPath+"\\Xml\\xmlTest.xml"));
		xmlPath = Application.dataPath + "\\Xml\\xmlTest.xml";
		load (Application.dataPath + "\\Xml\\xmlTest.xml", 0);
		/*foreach(int i in objectifCourant.tableDesObjectifs.Keys){
			Debug.Log (i + " -> " + objectifCourant.tableDesObjectifs[i]);
		}*/
	}

	void OnLevelWasLoaded(int level){
		xmlPath = Application.dataPath+"\\Xml\\xmlTest.xml";
	}

	void Update()
	{

		if (currentObjective.tableDesObjectifs.Count == 0) 
		{
			ObjectifName.GetComponent<Text> ().text = "MISSION COMPLETE !";
			Progression.GetComponent<Text>().text = "";
			return;
		}

		//Debug.Log (currentObjective.description);

		ObjectifName.GetComponent<Text> ().text = currentObjective.description;
		foreach(int i in currentObjective.tableDesObjectifs.Keys)
		{

			Debug.Log (i + " -> " + currentObjective.tableDesObjectifs[i]);

			if(i == 0 || i == 1 || i == 3 || i == 4 || i == 6)
				Progression.GetComponent<Text>().text = currentObjective.tableDesObjectifs[i].ToString()+ " / " + initialObjective.tableDesObjectifs[i].ToString();
			else if(i == 5)
			   Progression.GetComponent<Text>().text = RoundValue((float)currentObjective.tableDesObjectifs[i],1).ToString();


			//Il est important de mettre cette instruction à la fin sinon on peut clear l'objectif et demander l'affichage ensuite
			//Ce qui provoque une exception
			if(i == 5)
				updateGoal (5,Time.deltaTime);


		}



	}

	// Objectifs qui se décrémentent par entier (tuer bactérie, etc...)
	public void updateGoal(int label){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			int tmp = (int) currentObjective.tableDesObjectifs[label];
			++tmp;
			if(tmp >= 0)
				currentObjective.tableDesObjectifs[label] = tmp;
			
			if(isCurrentObjectiveComplete()){
				initialObjective.clear();
				currentObjective.clear();
				++idObjectif;
				load (xmlPath, idObjectif);
			}
		}
	}


	// Objectifs qui se décrémentent par float (time, etc...)
	void updateGoal(int label, float time){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			float tmp = (float) currentObjective.tableDesObjectifs[label];
			tmp -= time; // VALEUR A DEFINIR PLUS EN DETAIL
			currentObjective.tableDesObjectifs[label] = tmp;

			
			if(isCurrentObjectiveComplete()){
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

			if(isCurrentObjectiveComplete()){
				currentObjective.clear();
				++idObjectif;
				load (xmlPath, idObjectif);
			}
		}
	}

	bool isCurrentObjectiveComplete()
	{
		foreach (int i in currentObjective.tableDesObjectifs.Keys) 
		{
			if((i == 0 || i == 1 || i == 3 || i == 4 || i == 6) && (int)currentObjective.tableDesObjectifs[i] < (int)initialObjective.tableDesObjectifs[i])
				return false;
			else if(i == 5 && (float)currentObjective.tableDesObjectifs[i] > 0.0f)
				return false;
			else if(i == 2)
				return false;

		}
		return true;
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
					ObjectifId = id;
					initialObjective.description = myXmlTextReader.GetAttribute("description");
					currentObjective.description = myXmlTextReader.GetAttribute("description");

					initialObjective.learning = myXmlTextReader.GetAttribute("learning");
					currentObjective.learning = myXmlTextReader.GetAttribute("learning");


					tag = int.Parse(myXmlTextReader.GetAttribute("tag"));
					if(tag == 0 || tag == 1 || tag == 3 || tag == 4 || tag == 6)
					{ // Si on a ces tags, on a forcément un int dans value
						initialObjective.tableDesObjectifs.Add(tag, (int)int.Parse(myXmlTextReader.GetAttribute("value")));
						currentObjective.tableDesObjectifs.Add(tag, 0);
						break;
					} 
					else if(tag == 2)
					{ // Pour le tag 2, on récupère un vector2 dans value sous forme "float;float"
						coordMatchX = coordRegexX.Match(myXmlTextReader.GetAttribute("value"));
						coordMatchY = coordRegexY.Match(myXmlTextReader.GetAttribute("value"));
						coords.Set(float.Parse(coordMatchX.Value), float.Parse(coordMatchY.Value));
						initialObjective.tableDesObjectifs.Add(tag, (Vector2)coords);
						currentObjective.tableDesObjectifs.Add(tag, (Vector2)coords);
						break;
					} 
					else if(tag == 5)
					{ // Pour le tag 5 on a un float dans value
						initialObjective.tableDesObjectifs.Add(tag, (float)float.Parse(myXmlTextReader.GetAttribute("value")));
						currentObjective.tableDesObjectifs.Add(tag, (float)float.Parse(myXmlTextReader.GetAttribute("value")));
						break;
					}


				}
			}
		}

		if (aTrouve) 
		{
			//Activer le panneau
			GameObject panel = GameObject.Find(initialObjective.learning);
			if(panel != null)
			{
				panel.GetComponent<PanelController>().isPanelActive = true;
			}
			else
			{
				Debug.Log("Panneau inexistant : " + initialObjective.learning);
			}
		}

		if (!aTrouve)
			Debug.Log ("N'a pas trouvé.\n");
	}

	/** Arrondi un float avec <precision> chiffre après la virgule */
	public static float RoundValue(float num, float precision)
	{
		return Mathf.Floor(num * precision + 0.5f) / precision;
	}

}
