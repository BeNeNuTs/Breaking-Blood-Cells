/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBacteriaManager : MonoBehaviour {

	public GameObject BoundsStep0;

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

	
	//Variables de spawn
	
	public AgentSpawn spawnMacrophage;
	public AgentSpawn spawnBacteria;
	public AgentSpawn spawnCyto;
	
	//Variables d'écran Pause/Victoire/Défaite
	public static GameObject gameOverScreen;
	public static GameObject victoryScreen;
	public static GameObject pauseMenu;
	
	public static bool gameLost = false;
	
	//Variables spécifiques
	public GameObject FirstCell;
	public GameObject FirstMacrophage;

	//Variables cinématiques
	public List<GameObject> cutscenes;
	public int idCutscene = 0;




	// Use this for initialization
	void Start () 
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
		
		UnitManager.MAX_MACROPHAGES = 5;
		UnitManager.MAX_LYMPHOCYTES_T = 4;
	
	}
	
	// Update is called once per frame
	void Update() 
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
		
		if (ObjectifManager.ObjectifId > 1 && BoundsStep0 != null) 
		{
			Destroy(BoundsStep0);
		}
		
		if (ObjectifManager.ObjectifId == 2 || ObjectifManager.ObjectifId == 4) 
		{
			spawnBacteria.enabled = true;
			spawnBacteria.spawnRate = 0.5f;
			spawnMacrophage.spawnRate = 10f;
			UnitManager.MAX_BACTERIES = 150;
			UnitManager.MAX_MACROPHAGES = 4;
			
		}
		
		if (ObjectifManager.ObjectifId == 3) 
		{
			spawnBacteria.spawnRate = 5;
			spawnMacrophage.spawnRate = 10;
			UnitManager.MAX_BACTERIES = 50;
		}
		
		if (ObjectifManager.ObjectifId == 5) 
		{
			GameManager.canTakeResidu = true;
		} 
		else 
		{
			GameManager.canTakeResidu = false;
		}
		
		if (ObjectifManager.ObjectifId == 6) 
		{
			spawnCyto.enabled = true;
			spawnCyto.spawnRate = 5;
			UnitManager.MAX_BACTERIES = 0;
			
		}
		
	}

	public  IEnumerator makeTransition(float timeFirstBlend, float timeSecondBlend)
	{
		if (ObjectifManager.cutscene) 
		{
			Debug.Log("cutscene");
			cutscenes[idCutscene].SetActive(true);
			idCutscene++;
			ObjectifManager.cutscene = false;
		}
		
		if (ObjectifManager.blend) 
		{
			StartCoroutine (CameraControl.BlendCameraTo (blendPosition [idBlend], blendZoom [idBlend], timeFirstBlend, false));
			yield return new WaitForSeconds (2*timeFirstBlend + 1.0f);
			idBlend++;
		}
		else
			yield return new WaitForSeconds (1.0f);
		
		
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

}*/
