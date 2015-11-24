using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameObject CellControlled;
	public static GameObject Selection;
	public static GameObject gameManager;

	public static bool isPaused = false;

	// Use this for initialization
	void Start () 
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
	void Update () {
	
	}
}
