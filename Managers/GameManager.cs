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
	public GameObject victoryScreen;
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
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseScreen");
		pauseMenu.SetActive (false);
		/*victoryScreen = GameObject.FindGameObjectWithTag ("VictoryScreen");
		victoryScreen.SetActive (false);*/
	}

	void Update()
	{
		if (Input.GetButtonDown ("Pause")) 
		{
			if(!isPaused)
				Pause();
			else if(pauseMenu.activeSelf)//Pour vérifier qu'on est dans le menu pause car les panneaux mettent le jeu en pause
				Unpause();
		}
	}

	public static void PanelPause()
	{
		Time.timeScale = 0f;
		isPaused = true;

	}

	public static void PanelUnpause()
	{
		Time.timeScale = 1f;
		isPaused = false;	
	}

	public void Pause()
	{
		pauseMenu.SetActive (true);
		Time.timeScale = 0f;
		isPaused = true;
		
	}
	
	public void Unpause()
	{
		pauseMenu.SetActive (false);
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

		gameManager.GetComponent<GameManager>().victoryScreen.SetActive (true);
	}

	public void backToMenu()
	{
		ObjectifManager.idObjectif = 0;
		Time.timeScale = 1f;
		isPaused = false;
		Application.LoadLevel ("MainMenu");
	}

	public void loadLevel(int i)
	{
		ObjectifManager.idObjectif = 0;
		Time.timeScale = 1f;
		isPaused = false;
		Application.LoadLevel("Level"+i);
	}
	
	public void restart()
	{
		ObjectifManager.idObjectif = 0;
		Time.timeScale = 1f;
		isPaused = false;
		Application.LoadLevel (Application.loadedLevelName);
	}




}
