using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelVirus01Manager : MonoBehaviour {
	
	public GameObject victoryLevelScreen; 

	List<bool> ObjectifDone = new List<bool>();

	public GameObject boundsStep0;

	
	//Variables de spawn
	public AgentSpawn spawnMacrophage;
	public AgentSpawn spawnLB;
	public AgentSpawn spawnVirus;
	
	public GameObject virusBase;


	void Awake()
	{

	}

	// Use this for initialization
	void Start () 
	{

		GameManager.gameManager.GetComponent<GameManager> ().initLevel ();

		Time.timeScale = 1f;

		UnitManager.CountCells ();
		//Debug.Log (ObjectifManager.nbObjectifs);
		//Pour chaque objectif
		for (int i = 0; i < 100; i++) 
		{
			//Debug.Log(i);
			ObjectifDone.Add(false);
		}

		UnitManager.MAX_VIRUS = 50;
		UnitManager.MAX_MACROPHAGES = 10; //10
		UnitManager.MAX_LYMPHOCYTES_T = 0;
		UnitManager.MAX_LYMPHOCYTES_B = 10;

		spawnMacrophage.enabled = true;
		spawnMacrophage.spawnRate = 5;
		spawnLB.enabled = false;
		spawnLB.spawnRate = 5;

		spawnVirus.enabled = false;
		spawnVirus.spawnRate = 1;

		GameManager.canTakeResidu = false;


	}
	 

	// Update is called once per frame
	void Update () 
	{

		
		if (UnitManager.NB_CELLS == 0) 
		{
			Debug.Log("All Cells dead");
			GameManager.gameOver();
		}

		if (ObjectifManager.ObjectifId == 6 && !ObjectifDone [6]) 
		{	
			spawnVirus.enabled = true;;
			
			ObjectifDone[6] = true; 
		}

		if (ObjectifManager.ObjectifId == 7 && !ObjectifDone [7]) 
		{	
			Destroy(boundsStep0);
			
			ObjectifDone[7] = true; 
		}


		if (ObjectifManager.ObjectifId == 11 && !ObjectifDone [11]) 
		{	
			GameManager.canTakeResidu = true;
			
			ObjectifDone[11] = true; 
		}

		if (ObjectifManager.ObjectifId == 12 && !ObjectifDone [12]) 
		{	
			GameManager.canTakeResidu = false;
			
			ObjectifDone [12] = true;
		}

		if (ObjectifManager.ObjectifId == 20 && !ObjectifDone [20]) 
		{	
			UnitManager.MAX_LYMPHOCYTES_T = 10;
			spawnLB.enabled = true;

			ObjectifDone[20] = true; 
		}


	
	}



}
