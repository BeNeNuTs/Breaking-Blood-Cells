using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	

	static Vector2 destination;
	
	public float speed;

	float progress;


	// Update is called once per frame
	void FixedUpdate () 
	{
		if (GameManager.CellControlled != null) {
			Vector2 myPosition = GameManager.CellControlled.GetComponent<Rigidbody2D> ().transform.position;
			if (Vector2.Distance (myPosition, destination) >= 1.0f) {
				GameManager.CellControlled.GetComponent<Rigidbody2D> ().velocity = (destination - myPosition).normalized * speed;
			} else {
				GameManager.CellControlled.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
		}



	}
	
	
	//Nous mettons ceci dans une fonction, afin de le réutiliser au cas où l'on devrait faire bouger un ennemi
	public static void DefineNewDestination (Vector2 newDestination)
	{
		destination = newDestination;
	}


}
