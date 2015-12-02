using UnityEngine;
using System.Collections;

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
	public static float sizeCameraStep0 = 50;
	public static Vector3 positionCameraStep0 = new Vector3 (105, 0, -1);

	public static float sizeCameraStep1 = 120;
	public static Vector3 positionCameraStep1 = new Vector3 (0, 0, -1);

	public bool blendToStep1 = false;

	//Variables de limitations des agents
	public static bool canPlayerHandleResidu = false;
	public static bool canIAHandleResidu = false;

	public static bool canGenerateMacrophage = false;
	public static bool canGenerateLT = false;
	public static bool canGenerateLTCyto = false;
	public static bool canGenerateLB = false;

	public static bool canGenerateVirus = false;
	public static bool canGenerateBacteria = false;



	
	void Awake()
	{
		gameManager = gameObject;
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
		if(ObjectifManager.ObjectifId == 1 && !blendToStep1)
		{
			blendToStep1 = true;
			StartCoroutine(CameraControl.BlendCameraTo(positionCameraStep1, sizeCameraStep1, 1.0f,true));
		}
	
	}
}
