using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	

	public enum InputMode
	{
		Default,
		Controlling,
		Attacking
	}

	public static InputMode mode = InputMode.Default;

	public static GameObject selection;
	public static GameObject playerTarget;

	/*Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
         diff.Normalize();
 
         float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(0f, 0f, rot_z);*/


	void Start()
	{
		selection = GameObject.FindGameObjectWithTag ("Selection");
	}

	void Update () 
	{
		
	


		if (Input.GetButtonDown ("Fire1")) {
			RaycastManager.RaycastControl ();
		}

		if (GameManager.CellControlled == null) 
		{
			mode = InputMode.Default;
			return;
		}

		if (GameManager.CellControlled.GetComponent<AgentLife> ().isInfected) 
		{
			DeselectAll();
			return;
		}


		if (Input.GetButtonDown ("Fire2")) {
			playerTarget = RaycastManager.RaycastAttackEnemy ();
			GameManager.CellControlled.GetComponent<AgentMovement> ().enabled = false;
			GameManager.CellControlled.GetComponent<MoveToPoint> ().DefineNewDestination (Camera.main.ScreenToWorldPoint (Input.mousePosition));

			if (playerTarget != null) {
				mode = InputMode.Attacking;
			}
			else
			{
				mode = InputMode.Controlling;
			}
			
		}


		switch (mode) {
		
		case (InputMode.Attacking):


			if(playerTarget == null)
			{
				//GameManager.CellControlled.GetComponent<AgentMovement> ().enabled = true;
				mode = InputMode.Controlling;
				break;
			}

			GameManager.CellControlled.GetComponent<MoveToPoint> ().DefineNewDestination (playerTarget.GetComponent<Rigidbody2D> ().position);

			if (Vector2.Distance (GameManager.CellControlled.GetComponent<Rigidbody2D> ().position, playerTarget.GetComponent<Rigidbody2D> ().position) < 10) {
				GameManager.CellControlled.GetComponent<AgentMovement> ().enabled = true;
				GameManager.CellControlled.GetComponent<MoveToPoint> ().enabled = false;


			}
		                    
			break;
		
		
		//case (InputMode.Attacking)
		}
	}

	public static void DeselectAll()
	{
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Selection"))
		{
			go.GetComponent<SpriteRenderer>().enabled = false;
		}

		if (GameManager.CellControlled != null) {
			GameManager.CellControlled.GetComponent<AgentMovement> ().enabled = true; 
			GameManager.CellControlled = null;
		}
	}

	

}
