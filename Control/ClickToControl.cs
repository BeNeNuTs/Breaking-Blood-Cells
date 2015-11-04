using UnityEngine;
using System.Collections;

public class ClickToControl : MonoBehaviour {

	public bool playerControl = false;
	public GameObject characterToControl;
	public GameObject characterSprite;
	public GameObject selectedSprite;

	public void ControlCell()
	{

		Debug.Log ("Click");

	
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Selection"))
		{
			go.GetComponent<SpriteRenderer>().enabled = false;
		}


		GameManager.CellControlled = characterToControl;

		selectedSprite.GetComponent<SpriteRenderer> ().enabled = true;

		//On désactive le mouvement de l'agent
		characterToControl.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		characterToControl.GetComponent<AgentMovement> ().enabled = false;
		//characterToControl.GetComponent<MoveToPoint> ().enabled = true;
		characterToControl.GetComponent<MoveToPoint> ().DefineNewDestination (characterToControl.GetComponent<Rigidbody2D>().position);

		InputManager.mode = InputManager.InputMode.Controlling;


	}

	void OnDestroy()
	{
		GameManager.CellControlled = null;
	}



}
