using UnityEngine;
using System.Collections;

public class ClickToControl : MonoBehaviour {

	public bool playerControl = false;
	public GameObject characterToControl;

	public void ControlCell()
	{

		Debug.Log ("Click");

		if (GameManager.CellControlled != null) 
		{
			//GameManager.CellControlled.GetComponent<PlayerControl> ().playerControl = false;
			GameManager.CellControlled.GetComponent<AgentMovement> ().enabled = true;
		}

		GameManager.CellControlled = characterToControl;
		PlayerController.DefineNewDestination (Vector2.zero);//GameManager.CellControlled.GetComponent<Rigidbody2D>().position);
		characterToControl.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		characterToControl.GetComponent<AgentMovement> ().enabled = false;

		InputManager.mode = InputManager.InputMode.Controlling;


	}



}
