using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBacteria03Manager : MonoBehaviour {
	
	public GameObject victoryLevelScreen; 

	List<bool> ObjectifDone = new List<bool>();

	
	//Variables de spawn
	public AgentSpawn spawnMacrophage;
	public AgentSpawn spawnLTCyto;
	public List<AgentSpawn> spawnBacteria;
	
	public List<GameObject> bacteriaBases;

	//Variables spécifiques au niveau
	public GameObject FirstLTCyto;


	void Awake()
	{

	}

	// Use this for initialization
	void Start () 
	{
		UnitManager.CountCells ();
		//Debug.Log (ObjectifManager.nbObjectifs);
		//Pour chaque objectif
		for (int i = 0; i < ObjectifManager.nbObjectifs; i++) 
		{
			//Debug.Log(i);
			ObjectifDone.Add(false);
		}

		UnitManager.MAX_BACTERIES = 100;
		UnitManager.MAX_MACROPHAGES = 10; //10
		UnitManager.MAX_LYMPHOCYTES_T = 0;

		spawnMacrophage.enabled = true;
		spawnMacrophage.spawnRate = 20;
		spawnLTCyto.enabled = false;
		spawnLTCyto.spawnRate = 5;

		spawnBacteria [0].enabled = true;
		spawnBacteria [1].enabled = true;
		spawnBacteria [2].enabled = true;

	}
	 

	// Update is called once per frame
	void Update () 
	{

		
		if (UnitManager.NB_CELLS == 0) 
		{
			Debug.Log("All Cells dead");
			GameManager.gameOver();
		}

		if (ObjectifManager.ObjectifId == 8 && !ObjectifDone [8]) 
		{	
			UnitManager.MAX_LYMPHOCYTES_T = 10;
			spawnLTCyto.enabled = true;

			ObjectifDone[8] = true; 
		}

		if (ObjectifManager.ObjectifId == 11 && !ObjectifDone [11]) 
		{	
			foreach(GameObject meteor in bacteriaBases)
			{
				foreach(BoxCollider2D box in meteor.GetComponents<BoxCollider2D>())
					box.enabled = true;
			}

			foreach(AgentSpawn spawn in spawnBacteria)
			{
				spawn.spawnRate = 3;
			}

			ObjectifDone[11] = true; 
		}
	
	}



}
