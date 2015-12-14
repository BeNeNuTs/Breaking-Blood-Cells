using UnityEngine;
using System.Collections;

public class AnimationCutscene : Cutscene {
	

	public AnimationClip anim;

	// Use this for initialization
	void Start () 
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<Animator> ().SetBool ("PlayTransfo", true);
		StartCoroutine (endObjectif ());
	}

	IEnumerator endObjectif()
	{
		yield return new WaitForSeconds(anim.length);
		GameManager.gameManager.GetComponent<ObjectifManager>().updateCutsceneGoal();

	}

	// Update is called once per frame
	void Update () {
	
	}
}
