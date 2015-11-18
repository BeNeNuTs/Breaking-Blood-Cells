using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour{

	public GameObject playMenu, controlMenu, receptionMenu;

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
		receptionMenu.SetActive (true);
		playMenu.SetActive (false);
		controlMenu.SetActive (false);
	}

	public void toControls(){
		receptionMenu.SetActive (false);
		playMenu.SetActive (false);
		controlMenu.SetActive (true);
	}

	public void toPlay(){
		playMenu.SetActive (true);
		receptionMenu.SetActive (false);
		controlMenu.SetActive (false);
	}
}
