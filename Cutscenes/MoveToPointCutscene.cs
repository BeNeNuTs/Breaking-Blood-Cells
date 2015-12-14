using UnityEngine;
using System.Collections;

public class MoveToPointCutscene : Cutscene {

	

	Vector2 initialPosition;
	public Vector2 destination;
	public float duration;
	float progress;
	
	bool arrived = false;
	public bool disapear = false;
	//160 25 0

	
	// Use this for initialization
	void Start () 
	{
		if(GetComponent<SpriteRenderer> () != null)
			GetComponent<SpriteRenderer> ().enabled = true;
		initialPosition = transform.position;
		//Debug.Log (initialPosition);
		progress = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (progress < 1.0f) 
		{
			progress += (Time.deltaTime / duration);
			transform.position = Vector2.Lerp (initialPosition, destination, progress);
		} 
		else  
		{
			GameManager.gameManager.GetComponent<ObjectifManager>().updateCutsceneGoal();
			if(disapear)
				gameObject.SetActive(false);
		}
	}
}
