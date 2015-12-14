using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBacteria01Manager : MonoBehaviour {

	public GameObject BoundsStep0;
	
	public GameObject victoryLevelScreen; 
	
	List<bool> ObjectifDone = new List<bool>();

	//Variables de spawn
	public AgentSpawn spawnMacrophage;
	public AgentSpawn spawnBacteria;
	
	//Variables spécifiques
	public GameObject FirstCell;
	public GameObject FirstMacrophage;
	public GameObject SecondMacrophage;
	

	// Use this for initialization
	void Start () 
	{

		UnitManager.CountCells ();

		for (int i = 0; i < ObjectifManager.nbObjectifs; i++) 
		{
			ObjectifDone.Add(false);
		}

		spawnMacrophage.enabled = false;
		spawnBacteria.enabled = false;

		UnitManager.MAX_BACTERIES = 20;
		UnitManager.MAX_MACROPHAGES = 2;
		UnitManager.MAX_LYMPHOCYTES_T = 0;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (UnitManager.NB_CELLS == 0) 
		{
			Debug.Log("All Cells dead");
			GameManager.gameOver();
		}
		
		if (ObjectifManager.ObjectifId == 0 && (FirstCell == null || FirstMacrophage == null)) 
		{
			
			Debug.Log("Game over on first objective");
			GameManager.gameOver();
		}


		if (ObjectifManager.ObjectifId == 7 && !ObjectifDone [7]) 
		{
			spawnBacteria.enabled = true;
			ObjectifDone [7] = true;
		}

		//Fin de la première partie
		if (ObjectifManager.ObjectifId == 8 && !ObjectifDone [8]) 
		{
			Destroy(BoundsStep0);
			ObjectifDone [8] = true;
		} 

		if (ObjectifManager.ObjectifId == 12 && !ObjectifDone [12]) 
		{	
			spawnMacrophage.enabled = true;
			SecondMacrophage.GetComponent<AgentMovement>().enabled = true;
			UnitManager.MAX_BACTERIES = 20;
			UnitManager.MAX_MACROPHAGES = 2;
			
			ObjectifDone [12] = true;
		} 

		if (ObjectifManager.ObjectifId == 13 && !ObjectifDone [13]) 
		{	
			UnitManager.MAX_BACTERIES = 30;
			UnitManager.MAX_MACROPHAGES = 3;
			
			ObjectifDone [13] = true;
		}

		if (ObjectifManager.ObjectifId == 14 && !ObjectifDone [14]) 
		{	
			UnitManager.MAX_BACTERIES = 50;
			UnitManager.MAX_MACROPHAGES = 5;
			
			ObjectifDone [14] = true;
		}

	
	}

}
