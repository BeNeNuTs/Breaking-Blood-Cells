using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public static GameObject CellControlled;
	public static GameObject gameManager;

	public static bool isPaused = false;
	

	public GameObject macrophage;
	public GameObject bacteria;
	public GameObject LTAux;
	public GameObject LTCyto;
	public GameObject LB;
	public GameObject virus;
	

	//Variables d'écran Pause/Victoire/Défaite
	public static GameObject gameOverScreen;
	public static GameObject victoryScreen;
	public static GameObject pauseMenu;

	//Variables de limitations des agents
	public static bool canTakeResidu = false;
	
	public static bool canGenerateMacrophage = false;
	public static bool canGenerateLT = false;
	public static bool canGenerateLTCyto = false;
	public static bool canGenerateLB = false;
	
	public static bool canGenerateVirus = false;
	public static bool canGenerateBacteria = false;

	public static bool gameLost = false;

	public bool simulation = false;
	


	void Awake()
	{
		gameManager = gameObject;

	}

	void Start()
	{
		MacrophageAttack.bringResidues = false;
		MacrophageAttack.residuesDone = false;
		LTAuxMovement.residus = false;
		LTAuxMovement.typeResidus = Type.TypeResidus.NONE;
		gameOverScreen = GameObject.FindGameObjectWithTag ("GameOverScreen");
		gameOverScreen.SetActive (false);
		victoryScreen = GameObject.FindGameObjectWithTag ("VictoryScreen");
		victoryScreen.SetActive (false);
	}

	public static void Pause()
	{
		Time.timeScale = 0f;
		isPaused = true;

	}

	public static void Unpause()
	{
		Time.timeScale = 1f;
		isPaused = false;	
	}

	public static void gameOver()
	{

		foreach(GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
		{
			AgentLife life = cell.GetComponent<AgentLife>();
			if(life != null)
			{
				life.Kill();
			}
		}

		gameLost = true;
		gameOverScreen.SetActive (true);

	}

	public static void victory()
	{
		
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			AgentLife life = enemy.GetComponent<AgentLife>();
			if(life != null)
			{
				life.Kill();
			}
		}

		if (GameManager.gameManager.GetComponent<LevelBacteria01Manager> () != null)
			GameManager.gameManager.GetComponent<LevelBacteria01Manager> ().victoryLevelScreen.SetActive (true);


		//victoryScreen.SetActive (true);
	}

	public void backToMenu()
	{
		//BackToMenu
	}

	public void nextLevel()
	{
		//nextLevel
	}

	public void restart()
	{
		//Restart
	}




}
