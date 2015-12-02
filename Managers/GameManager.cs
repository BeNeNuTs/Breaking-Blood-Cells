using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public static GameObject CellControlled;
	public static GameObject gameManager;

	public static bool isPaused = false;

	public GameObject BoundsStep0;

	public GameObject macrophage;
	public GameObject bacteria;
	public GameObject LTAux;
	public GameObject LTCyto;
	public GameObject LB;
	public GameObject virus;

	public enum StepLevel
	{
		Step0, //Tuto 1 macro 1 bactérie.
		Step1 //Jeu normal
	}

	//Variables de controles de caméra
	public float sizeCameraStep0 = 50;
	public Vector3 positionCameraStep0 = new Vector3 (105, 0, -10);

	public float sizeCameraStepDefault = 120;
	public Vector3 positionCameraStepDefault = new Vector3 (0, 0, -10);

	
	public float sizeCameraStepAnalyse = 50;
	public Vector3 positionCameraStepAnalyse = new Vector3 (-140, -75, -10);

	public bool blendToStep1 = false;

	List<float> blendZoom;
	List<Vector3> blendPosition;

	int idBlend = 0;

	//Variables de limitations des agents
	public static bool canTakeResidu = false;

	public static bool canGenerateMacrophage = false;
	public static bool canGenerateLT = false;
	public static bool canGenerateLTCyto = false;
	public static bool canGenerateLB = false;

	public static bool canGenerateVirus = false;
	public static bool canGenerateBacteria = false;

	//Variables de spawn

	public GameObject spawnMacrophage;
	public GameObject spawnBacteria;
	public GameObject spawnCyto;



	
	void Awake()
	{
		gameManager = gameObject;
	}

	void Start()
	{
		blendZoom = new List<float> ();
		blendPosition = new List<Vector3> ();
	


		blendZoom.Add (sizeCameraStep0);
		blendPosition.Add (positionCameraStep0);
		blendZoom.Add (sizeCameraStepDefault);
		blendPosition.Add (positionCameraStepDefault);

		
		blendZoom.Add (sizeCameraStepAnalyse);
		blendPosition.Add (positionCameraStepAnalyse);
		blendZoom.Add (sizeCameraStepDefault);
		blendPosition.Add (positionCameraStepDefault);
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

	// Update is called once per frame
	void Update () 
	{
		if (ObjectifManager.ObjectifId == 1 || ObjectifManager.ObjectifId == 3) 
		{
			spawnBacteria.GetComponent<AgentSpawn>().enabled = true;
			spawnBacteria.GetComponent<AgentSpawn>().spawnRate = 1;
			spawnMacrophage.GetComponent<AgentSpawn>().spawnRate = 15;
			UnitManager.MAX_BACTERIES = 100;
		}

		if (ObjectifManager.ObjectifId == 2) 
		{
			spawnBacteria.GetComponent<AgentSpawn>().spawnRate = 5;
			spawnMacrophage.GetComponent<AgentSpawn>().spawnRate = 5;
			UnitManager.MAX_BACTERIES = 50;
		}

		if (ObjectifManager.ObjectifId == 4) 
		{
			canTakeResidu = true;
		} 
		else 
		{
			canTakeResidu = false;
		}

		if (ObjectifManager.ObjectifId == 5) 
		{
			spawnCyto.GetComponent<AgentSpawn>().enabled = true;
			spawnCyto.GetComponent<AgentSpawn>().spawnRate = 5;
			UnitManager.MAX_BACTERIES = 1;

		}
	
	}

	public  IEnumerator makeTransition(float timeFirstBlend, float timeSecondBlend)
	{
		if (ObjectifManager.blend) 
		{
			StartCoroutine (CameraControl.BlendCameraTo (blendPosition [idBlend], blendZoom [idBlend], timeFirstBlend, false));
			yield return new WaitForSeconds (2*timeFirstBlend);
			idBlend++;
		}



		//Activer le panneau
		GameObject panel = GameObject.Find (ObjectifManager.learning);
		if (panel != null) 
		{
			if(panel.GetComponent<PanelController>()!= null)
				panel.GetComponent<PanelController> ().isPanelActive = true;
		} else {
			Debug.Log ("Panneau inexistant : " + ObjectifManager.learning);
		}

		yield return new WaitForSeconds (0.5f);

		if (ObjectifManager.blend) 
		{
			StartCoroutine (CameraControl.BlendCameraTo (blendPosition [idBlend], blendZoom [idBlend], timeSecondBlend, true));
			idBlend++;
		}
		
	}
}
