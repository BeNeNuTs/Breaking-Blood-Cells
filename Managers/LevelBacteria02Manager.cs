using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBacteria02Manager : MonoBehaviour {
	
	public GameObject victoryLevelScreen; 

	List<bool> ObjectifDone = new List<bool>();

	//blends
	//Blend LT


	List<float> times = new List<float> ();
	List<Vector3> positions = new List<Vector3> ();
	List<float> zooms = new List<float> ();
	List<GameObject> objectToActive = new List<GameObject> ();
	
	//Variables de spawn
	public AgentSpawn spawnMacrophage;
	public List<AgentSpawn> spawnBacteria;

	public GameObject LTAuxZero;

	//Variables cinématiques
	public List<GameObject> cutscenes;
	public int idCutscene = 0;

	// Use this for initialization
	void Start () 
	{
		//Pour chaque objectif
		for (int i = 0; i < 20; i++) 
		{
			ObjectifDone.Add(false);
		}

		UnitManager.MAX_BACTERIES = 40;
		UnitManager.MAX_MACROPHAGES = 15; //10
		UnitManager.MAX_LYMPHOCYTES_T = 0;

		spawnMacrophage.spawnRate = 5; //20

	}
	 

	// Update is called once per frame
	void Update () 
	{
		if (ObjectifManager.cutscene) 
		{
			Debug.Log("Cutscenes");
			cutscenes[idCutscene++].SetActive(true);
			ObjectifManager.cutscene = false;
		}

		GameObject learning = GameObject.Find (ObjectifManager.learning);

		if (learning != null) 
		{
			if(learning.GetComponent<PanelController>()!= null)
				learning.GetComponent<PanelController> ().isPanelActive = true;
		} 

		if (ObjectifManager.ObjectifId == 2 && !ObjectifDone [2]) 
		{	
			UnitManager.MAX_BACTERIES = 100;
			UnitManager.MAX_MACROPHAGES = 10;

			foreach(AgentSpawn bacteriaSpawn in spawnBacteria)
			{
				bacteriaSpawn.spawnRate = 1;
			}
			
			ObjectifDone [2] = true;
		}

		if (ObjectifManager.ObjectifId == 3 && !ObjectifDone [3]) 
		{	
			UnitManager.MAX_MACROPHAGES = 20;
			UnitManager.MAX_BACTERIES = 70;
			foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				AgentAttack attack = enemy.GetComponent<AgentAttack>();
				if(attack != null)
				{
					attack.attackDamage /= 2; 
				}
			}

			//Blend on LT


			foreach(AgentSpawn bacteriaSpawn in spawnBacteria)
			{
				bacteriaSpawn.spawnRate = 5;
			}
			
			ObjectifDone [3] = true;
		}

	
	}



	public  IEnumerator multipleBlends(List<float> timeBlends, List<Vector3> positions, List<float> zooms, List<GameObject> toActive, float delay, bool EnableControl)
	{
		CameraControl.moveEnabled = false;
		CameraControl.zoomEnabled = false;

		yield return new WaitForSeconds (delay);
		for(int i = 0 ; i < timeBlends.Count; i++) 
		{
			StartCoroutine (CameraControl.BlendCameraTo (positions [i], zooms[i], timeBlends[i], false));
			yield return new WaitForSeconds (2*timeBlends[i]+1.0f);

			if (toActive[i] != null) 
			{
				if(toActive[i].GetComponent<PanelController>()!= null)
					toActive[i].GetComponent<PanelController> ().isPanelActive = true;
			} 

			yield return new WaitForSeconds (1.0f);
		}

		timeBlends.Clear ();
		positions.Clear ();
		zooms.Clear ();
		toActive.Clear ();

		CameraControl.moveEnabled = EnableControl;
		CameraControl.zoomEnabled = EnableControl;


	}
}
