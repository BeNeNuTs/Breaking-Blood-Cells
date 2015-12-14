using UnityEngine;
using System.Collections;

public class MeteorArrival : Cutscene {

	public GameObject meteor;
	public GameObject rocks;
	public GameObject explosion;

	public Sprite meteorVirus;
	public Sprite meteorBacteria;

	Vector2 initialPosition;
	public Vector2 destination;
	public float duration;
	float progress;

	bool arrived = false;
	//160 25 0

	public GameObject macrophageToKill;

	// Use this for initialization
	void Start () 
	{
		initialPosition = transform.position;
		if (GameManager.gameManager.GetComponent<GameManager> ().simulation) 
		{
			if(GameManager.gameManager.GetComponent<LevelAgentManager>().bacteria)
			{
				meteor.GetComponent<SpriteRenderer>().sprite = meteorBacteria;
			}else
			{
				meteor.GetComponent<SpriteRenderer>().sprite = meteorVirus;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (progress < 1.0f) {
		
			progress += (Time.deltaTime / duration);


			transform.position = Vector2.Lerp (initialPosition, destination, progress);
			if(progress > 0.95f)
			{
				Instantiate (explosion, rocks.transform.position, Quaternion.identity);
			}
		} 
		else if(!arrived) 
		{
			arrived = true;
			StartCoroutine(impact());
		}
	}



	IEnumerator impact()
	{
		Debug.Log ("MeteorImpact");
		yield return new WaitForSeconds (0.2f);
		if(macrophageToKill != null && macrophageToKill.activeSelf != false)
			macrophageToKill.GetComponent<AgentLife> ().Kill ();

		rocks.SetActive (true);
		yield return new WaitForSeconds (3.0f);
		GameManager.gameManager.GetComponent<ObjectifManager>().updateCutsceneGoal();

	}
}
