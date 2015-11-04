using UnityEngine;
using System.Collections;

public class MoveToPoint : MonoBehaviour {

	public Vector2 destination;
	
	public float speed;
	
	float progress;
	
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		//Debug.Log (destination);
			Vector2 myPosition =GetComponent<Rigidbody2D> ().position;
			if (Vector2.Distance (myPosition, destination) >= 1.0f) 
			{
				GetComponent<Rigidbody2D> ().velocity = (destination - myPosition).normalized * speed;
				//transform.forward = (destination - myPosition).normalized;
			} 
			else 
			{
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				if(gameObject != GameManager.CellControlled)
				{
					GetComponent<AgentMovement>().enabled = true; 
					enabled = false;
				}
				

				
			}

	}
	

	void OnDisable()
	{
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		//transform.forward = new Vector2(1,0);
	}

	//Nous mettons ceci dans une fonction, afin de le réutiliser au cas où l'on devrait faire bouger un ennemi
	public void DefineNewDestination (Vector2 newDestination)
	{
		enabled = true;
		destination = newDestination;
	}

}
