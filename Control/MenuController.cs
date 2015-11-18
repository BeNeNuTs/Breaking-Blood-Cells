using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour{
	public void quit(){
		if (Application.isEditor)
			Debug.Break ();
		else
			Application.Quit ();
	}

	public void loadPlayLevel(string sceneName){
		Application.LoadLevel (sceneName);
	}

	public void toReception(){
		GameObject.Find ("ReceptionMenu").SetActive (true);
		GameObject.Find ("PlayMenu").SetActive (false);
		GameObject.Find ("ControlMenu").SetActive (false);
	}

	public void toControls(){
		GameObject.Find ("ReceptionMenu").SetActive (false);
		GameObject.Find ("PlayMenu").SetActive (false);
		GameObject.Find ("ControlMenu").SetActive (true);
	}

	public void toPlay(){
		GameObject.Find ("ReceptionMenu").SetActive (false);
		GameObject.Find ("PlayMenu").SetActive (true);
		GameObject.Find ("ControlMenu").SetActive (false);
	}
}
