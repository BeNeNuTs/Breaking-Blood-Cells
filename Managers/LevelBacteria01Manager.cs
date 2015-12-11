using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBacteria01Manager : MonoBehaviour {

	public GameObject BoundsStep0;
	
	public GameObject victoryLevelScreen; 

	//Variables de controles de caméra

	public float sizeCameraStepInitial = 120;
	public Vector3 positionCameraStepInitial = new Vector3 (0, 0, -10);

	public float sizeCameraKillOneBacteria = 50;
	public Vector3 positionCameraKillOneBacteria  = new Vector3 (105, 0, -10);

	public float sizeCameraBaseBacteriaArrival = 70;
	public Vector3 positionCameraBaseBacteriaArrival  = new Vector3 (105, 0, -10);

	public float sizeCameraBaseMacroArrival = 50;
	public Vector3 positionCameraBaseMacroArrival = new Vector3 (-135, -75, -10);

	public float sizeCameraStepBattle = 120;
	public Vector3 positionCameraStepBattle = new Vector3 (0, 0, -10);


	List<bool> ObjectifDone = new List<bool>();


	List<float> times = new List<float> ();
	List<Vector3> positions = new List<Vector3> ();
	List<float> zooms = new List<float> ();
	List<GameObject> objectToActive = new List<GameObject> ();
	
	//Variables de spawn
	public AgentSpawn spawnMacrophage;
	public AgentSpawn spawnBacteria;
	
	//Variables spécifiques
	public GameObject FirstCell;
	public GameObject FirstMacrophage;
	
	//Variables cinématiques
	public List<GameObject> cutscenes;
	public int idCutscene = 0;

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < 20; i++) 
		{
			ObjectifDone.Add(false);
		}

		UnitManager.MAX_BACTERIES = 20;
		UnitManager.MAX_MACROPHAGES = 2;
		UnitManager.MAX_LYMPHOCYTES_T = 0;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ObjectifManager.cutscene) 
		{
			cutscenes[idCutscene++].SetActive(true);
			ObjectifManager.cutscene = false;
		}

		
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


		if (ObjectifManager.ObjectifId == 0 && !ObjectifDone [0]) 
		{
			times.Add(1.0f);
			zooms.Add(sizeCameraKillOneBacteria);
			positions.Add(positionCameraKillOneBacteria);
			objectToActive.Add(GameObject.Find(ObjectifManager.learning));

			StartCoroutine(multipleBlends(times,positions,zooms,objectToActive,1.0f, false));

			ObjectifDone [0] = true;
		}

		if (ObjectifManager.ObjectifId == 2 && !ObjectifDone [2]) 
		{
			Destroy(BoundsStep0);

			times.Add(0.5f);
			zooms.Add(sizeCameraBaseBacteriaArrival);
			positions.Add(positionCameraBaseBacteriaArrival);
			objectToActive.Add(GameObject.Find(ObjectifManager.learning));
			
			StartCoroutine(multipleBlends(times,positions,zooms,objectToActive,0.5f, false));
			
			ObjectifDone [2] = true;
		} 

		if (ObjectifManager.ObjectifId == 4 && !ObjectifDone [4]) 
		{	
			times.Add(0.3f);
			zooms.Add(sizeCameraBaseMacroArrival);
			positions.Add(positionCameraBaseMacroArrival);
			objectToActive.Add(GameObject.Find(ObjectifManager.learning));
			objectToActive.Add(null);

			times.Add(1.0f);
			zooms.Add(sizeCameraStepBattle);
			positions.Add(positionCameraStepBattle);
			
			StartCoroutine(multipleBlends(times,positions,zooms,objectToActive,0.5f,true));
			
			ObjectifDone [4] = true;
		} 

		if (ObjectifManager.ObjectifId == 5 && !ObjectifDone [5]) 
		{	
			UnitManager.MAX_BACTERIES = 20;
			UnitManager.MAX_MACROPHAGES = 2;
			
			ObjectifDone [5] = true;
		} 

		if (ObjectifManager.ObjectifId == 6 && !ObjectifDone [6]) 
		{	
			UnitManager.MAX_BACTERIES = 30;
			UnitManager.MAX_MACROPHAGES = 3;
			
			ObjectifDone [6] = true;
		}

		if (ObjectifManager.ObjectifId == 7 && !ObjectifDone [7]) 
		{	
			UnitManager.MAX_BACTERIES = 50;
			UnitManager.MAX_MACROPHAGES = 5;
			
			ObjectifDone [7] = true;
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
