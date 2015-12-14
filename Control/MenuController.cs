using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject credits;

	public GameObject menu;
	public GameObject levels;
	public GameObject characters;

	public void Play(){
		Debug.Log("PLAY");
		iTween.MoveTo(menu, iTween.Hash("x", -800, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
		iTween.MoveTo(levels, iTween.Hash("x", 0, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
	}

	public void Characters(){
		iTween.MoveTo(menu, iTween.Hash("x", 800, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
		iTween.MoveTo(characters, iTween.Hash("x", 0, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
	}

	public void Quit(){
		if(Application.isEditor){
			Debug.Break();
		}else{
			Application.Quit();
		}
	}

	public void SwitchCredits(){
		credits.SetActive(!credits.activeSelf);
	}

	public void BackToMenu(){
		iTween.MoveTo(menu, iTween.Hash("x", 0, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));

		iTween.MoveTo(levels, iTween.Hash("x", 800, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
		iTween.MoveTo(characters, iTween.Hash("x", -800, "time", 1f, "easetype", iTween.EaseType.easeOutBack, "islocal", true));
	}

	public void LoadLevel(int i){
		Application.LoadLevel("Level"+i);
	}
}
