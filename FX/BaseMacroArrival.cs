using UnityEngine;
using System.Collections;

public class BaseMacroArrival : MonoBehaviour {

	

	Vector2 initialPosition;
	public Vector2 destination;
	public float duration;
	float progress;
	
	bool arrived = false;
	//160 25 0

	
	// Use this for initialization
	void Start () 
	{
		initialPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (progress < 1.0f) 
		{
			progress += (Time.deltaTime / duration);
			transform.position = Vector2.Lerp (initialPosition, destination, progress);
		} 
		else  
		{
			GameManager.gameManager.GetComponent<ObjectifManager>().updateGoal(true);
		}
	}
}
