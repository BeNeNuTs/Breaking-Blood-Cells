using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBacteria02Manager : MonoBehaviour {
	
	public GameObject victoryLevelScreen; 

	List<bool> ObjectifDone = new List<bool>();

	
	//Variables de spawn
	public AgentSpawn spawnMacrophage;
	public List<AgentSpawn> spawnBacteria;

	//Variables spécifiques au niveau
	public GameObject FirstLTAux;
	public GameObject ChefMacro;



	// Use this for initialization
	void Start () 
	{

		GameManager.gameManager.GetComponent<GameManager> ().initLevel ();

		Time.timeScale = 1f;

		UnitManager.CountCells ();
		Debug.Log (ObjectifManager.nbObjectifs);
		//Pour chaque objectif
		for (int i = 0; i < 100; i++) 
		{
			ObjectifDone.Add(false);
		}

		UnitManager.MAX_BACTERIES = 40;
		UnitManager.MAX_MACROPHAGES = 15; //10
		UnitManager.MAX_LYMPHOCYTES_T = 0;

		spawnMacrophage.spawnRate = 20;
		spawnBacteria [0].enabled = true;
		spawnBacteria [1].enabled = false;
		spawnBacteria [2].enabled = false;

	}
	 

	// Update is called once per frame
	void Update () 
	{

		if (UnitManager.NB_CELLS == 0) 
		{
			Debug.Log("All Cells dead");
			GameManager.gameOver();
		}


		if (ObjectifManager.ObjectifId == 3 && !ObjectifDone [3]) 
		{	
			UnitManager.MAX_BACTERIES = 50;
			UnitManager.MAX_MACROPHAGES = 5;

			foreach(AgentSpawn bacteriaSpawn in spawnBacteria)
			{
				bacteriaSpawn.spawnRate = 3;
			}
			
			ObjectifDone [3] = true;
		}

		if (ObjectifManager.ObjectifId == 7 && !ObjectifDone [7]) 
		{	
			UnitManager.MAX_BACTERIES = 100;
			UnitManager.MAX_MACROPHAGES = 8;
			spawnBacteria [1].enabled = true;
			
			foreach(AgentSpawn bacteriaSpawn in spawnBacteria)
			{
				bacteriaSpawn.spawnRate = 1;
			}
			
			ObjectifDone [7] = true;
		}

		if (ObjectifManager.ObjectifId == 10 && !ObjectifDone [10]) 
		{	
			spawnBacteria [2].enabled = true;
			ObjectifDone [10] = true;
		}

		if (ObjectifManager.ObjectifId == 15 && !ObjectifDone [15]) 
		{	
			GameManager.canTakeResidu = true;

			UnitManager.MAX_MACROPHAGES = 10;
			UnitManager.MAX_BACTERIES = 70;

			foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				AgentAttack attack = enemy.GetComponent<AgentAttack>();
				if(attack != null)
				{
					attack.attackDamage /= 2; 
				}
			}

			
			foreach(AgentSpawn bacteriaSpawn in spawnBacteria)
			{
				bacteriaSpawn.spawnRate = 5;
			}
			
			ObjectifDone [15] = true;
		}

		if (ObjectifManager.ObjectifId == 16 && !ObjectifDone [16]) 
		{	
			GameManager.canTakeResidu = false;
			
			ObjectifDone [16] = true;
		}
	
	}



}
