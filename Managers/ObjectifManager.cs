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
	public enum tag {KILLBACTERIA = 0, KILLVIRUS = 1, GOTO = 2, ANALYZEBACTERIA = 3, ANALYZEVIRUS = 4, SURVIVE = 5, 
		CREATEANTIBODIES = 6, LOSECELLS = 7, CUTSCENE = 8, BRINGRESIDU = 9, BLEND=10, LEARNING = 11, LOSELTCYTO = 12, DESTROYBASE = 13,
	WAIT = 14};

	public GameObject ObjectifName;
	public GameObject Progression;

	public String xmlPath;

	public static int idObjectif = 0;

	private const float ECART_POSITION = 5.0f;

	public static int ObjectifId;

	public static int nbObjectifs;
	
	bool simulation;

	void Start(){
		simulation = GameManager.gameManager.GetComponent<GameManager> ().simulation;
		if (simulation) 
		{
			return;
		}
		//Debug.Log (File.Exists(Application.dataPath+"\\Xml\\xmlTest.xml"));
		//xmlPath = Application.dataPath + "\\Xml\\xmlTest.xml";
		xmlPath = Application.dataPath+xmlPath;
		load (xmlPath, 0);
		/*foreach(int i in objectifCourant.tableDesObjectifs.Keys){
			Debug.Log (i + " -> " + objectifCourant.tableDesObjectifs[i]);
		}*/

	
	}

	void OnLevelWasLoaded(int level){

	}

	void Update()
	{
		if (simulation) 
		{
			return;
		}

		//Debug.Log (currentObjective.description);

		ObjectifName.GetComponent<Text> ().text = currentObjective.description;
		foreach(int i in currentObjective.tableDesObjectifs.Keys)
		{

			//Debug.Log (i + " -> " + currentObjective.tableDesObjectifs[i]);

			if(i == 0 || i == 1 || i == 3 || i == 4 || i == 6 || i == 9 || i == 13)
				Progression.GetComponent<Text>().text = currentObjective.tableDesObjectifs[i].ToString()+ " / " + initialObjective.tableDesObjectifs[i].ToString();
			else if(i == 7 || i == 12)
			{
				ObjectifName.GetComponent<Text> ().text = currentObjective.description;
				Progression.GetComponent<Text>().text = "";
			}
			else if(i == 8 || i == 10 || i == 11 || i == 14)
			{
				ObjectifName.GetComponent<Text> ().text = "";
				Progression.GetComponent<Text>().text = "";
			}
			/*else if(i == 7)
				Progression.GetComponent<Text>().text = "";*/

		

		}

		
		if (currentObjective.tableDesObjectifs.Count == 0) 
		{
			ObjectifName.GetComponent<Text> ().text = "";
			Progression.GetComponent<Text>().text = "";
			idObjectif = 0;
			GameManager.victory();
			return;
		}



	}

	// Objectifs qui se décrémentent par entier (tuer bactérie, etc...)
	public void updateGoal(int label){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			int tmp = (int) currentObjective.tableDesObjectifs[label];
			++tmp;
			if(tmp >= 0)
				currentObjective.tableDesObjectifs[label] = tmp;
			
			if(isCurrentObjectiveComplete() && !GameManager.gameLost)
			{
				loadNextObjectif();
			}
		}
	}

	// Objectifs cutscenes, les cutscenes appellent cette fonction lorsqu'elles sont terminées
	public void updateCutsceneGoal(){
		if (currentObjective.tableDesObjectifs.ContainsKey (8)) {
			int tmp = (int)currentObjective.tableDesObjectifs [8];
			++tmp;
			if (tmp >= 0)
				currentObjective.tableDesObjectifs [8] = tmp;

			
			if (isCurrentObjectiveComplete () && !GameManager.gameLost) {
				loadNextObjectif ();
			}
		}
	}

	// Objectifs de blend
	public void endBlendObjectif()
	{
		if (currentObjective.tableDesObjectifs.ContainsKey (10)) 
		{
			loadNextObjectif();
		}
	}

	// Objectifs de learning
	public void endLearningObjectif()
	{
		if (currentObjective.tableDesObjectifs.ContainsKey (11)) 
		{
			loadNextObjectif();
		}
	}

	public IEnumerator waitObjectif (float time)
	{
		yield return new WaitForSeconds (time);
		loadNextObjectif ();
	}


	// Objectifs qui se décrémentent par float (time, etc...)
	void updateGoal(int label, float time){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			float tmp = (float) currentObjective.tableDesObjectifs[label];
			tmp -= time; // VALEUR A DEFINIR PLUS EN DETAIL
			currentObjective.tableDesObjectifs[label] = tmp;

			
			if(isCurrentObjectiveComplete() && !GameManager.gameLost)
			{
				loadNextObjectif();
			}
		}
	}

	IEnumerator TimeObjectif()
	{
		float time = (float) currentObjective.tableDesObjectifs[5];
		while (!isCurrentObjectiveComplete()) 
		{
			yield return new WaitForSeconds(1.0f);
			time -= 1.0f;
			currentObjective.tableDesObjectifs[5] = time;
			Progression.GetComponent<Text>().text = RoundValue((float)currentObjective.tableDesObjectifs[5],1).ToString();
		}

		if (!GameManager.gameLost) 
		{
			loadNextObjectif();
		}

	}

	// Objectifs qui se décrémentent par position (vecteurs etc...)
	void updateGoal(int label, Vector2 pos){
		if (currentObjective.tableDesObjectifs.ContainsKey (label)) {
			Vector2 obj = (Vector2)(currentObjective.tableDesObjectifs[label]);
			if((pos.x > obj.x-ECART_POSITION || pos.x < obj.x+ECART_POSITION) && (pos.y > obj.y-ECART_POSITION || pos.y < obj.y+ECART_POSITION)){ // VARIABLE D'ECART A DEFINIR
				currentObjective.tableDesObjectifs.Remove(label);
			}

			if(isCurrentObjectiveComplete() && !GameManager.gameLost){
				loadNextObjectif();
			}
		}
	}

	void loadNextObjectif()
	{
		initialObjective.clear();
		currentObjective.clear();
		++idObjectif;
		load (xmlPath, idObjectif);
	}

	bool isCurrentObjectiveComplete()
	{
		foreach (int i in currentObjective.tableDesObjectifs.Keys) 
		{
			if((i == 0 || i == 1 || i == 3 || i == 4 || i == 6 || i == 7 || i == 8 || i==9 || i == 13) && (int)currentObjective.tableDesObjectifs[i] < (int)initialObjective.tableDesObjectifs[i])
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
			if(myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "objectifs")
			{
				Debug.Log(nbObjectifs);
				nbObjectifs = int.Parse(myXmlTextReader.GetAttribute("number"));
				Debug.Log(nbObjectifs);
			}
			if(myXmlTextReader.IsStartElement() && myXmlTextReader.Name == "objectif"){
				if (int.Parse(myXmlTextReader.GetAttribute("id")) == id){

					aTrouve = true;
					ObjectifId = id;
					initialObjective.description = myXmlTextReader.GetAttribute("description");
					currentObjective.description = myXmlTextReader.GetAttribute("description");



					tag = int.Parse(myXmlTextReader.GetAttribute("tag"));

					//Si le tag est 10, il s'agit d'un blend de caméra
					if(tag == 14)
					{
						StartCoroutine(waitObjectif(float.Parse(myXmlTextReader.GetAttribute("time"))));
						currentObjective.tableDesObjectifs.Add(tag, -1);
						return;
					}
					else if(tag == 10)
					{
						Debug.Log("Blend");
						currentObjective.tableDesObjectifs.Add(tag, -1);

						float x = float.Parse(myXmlTextReader.GetAttribute("x"));
						float y = float.Parse(myXmlTextReader.GetAttribute("y"));
						float z = float.Parse(myXmlTextReader.GetAttribute("z"));

						float duration = float.Parse(myXmlTextReader.GetAttribute("duration"));
						float size = float.Parse(myXmlTextReader.GetAttribute("size"));

						bool enableControlAfter =  bool.Parse(myXmlTextReader.GetAttribute("enableControlAfter"));

						StartCoroutine(CameraControl.BlendCameraTo(new Vector3(x,y,z),size,duration,enableControlAfter));

					}
					else if(tag == 11) // Si c'est 11, il s'agit d'un affichage de panneau
					{
						currentObjective.tableDesObjectifs.Add(tag, -1);

						string learning = myXmlTextReader.GetAttribute("learning");
						GameObject panelLearning = GameObject.Find(learning);
						if (panelLearning != null) 
						{
							if(panelLearning.GetComponent<PanelController>()!= null)
								panelLearning.GetComponent<PanelController> ().isPanelActive = true;
						}
						else
						{
							Debug.Log("Panneau Learning " + learning +" non trouvé, penser à activer l'objet !");
							loadNextObjectif();
							return;
						}
					}
					else if(tag == 8)
					{
						initialObjective.tableDesObjectifs.Add(tag, (int)int.Parse(myXmlTextReader.GetAttribute("value")));
						currentObjective.tableDesObjectifs.Add(tag, 0);

						string cutsceneName = myXmlTextReader.GetAttribute("cutsceneName");
						GameObject cutscene = GameObject.Find(cutsceneName);
						if(cutscene != null)
						{
							Cutscene scriptCutscene = cutscene.GetComponent<Cutscene>();
							if(scriptCutscene != null)
								cutscene.GetComponent<Cutscene>().enabled = true;
							else
							{
								Debug.Log("Aucun script cutscene n'est attaché à l'objet");
								loadNextObjectif();
								return;
							}
						}
						else
						{
							Debug.Log("Cutscene " + cutsceneName +" non trouvé, penser à activer l'objet !");
							loadNextObjectif();
							return;
						}


					}
					else if(tag == 0 || tag == 1 || tag == 3 || tag == 4 || tag == 6 || tag == 7 || tag == 9 || tag == 12 || tag == 13)
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
						StartCoroutine(TimeObjectif());
						break;
					}
				}
			}
		}

		if (aTrouve) {
			//StartCoroutine(GameManager.gameManager.GetComponent<LevelBacteriaManager>().makeTransition (1, 1));
		} else {
			Debug.Log ("N'a pas trouvé.\n");
		}
	}

	/** Arrondi un float avec <precision> chiffre après la virgule */
	public static float RoundValue(float num, float precision)
	{
		return Mathf.Floor(num * precision + 0.5f) / precision;
	}

}
