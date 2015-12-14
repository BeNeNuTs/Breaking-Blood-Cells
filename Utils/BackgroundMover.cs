using UnityEngine;
using System.Collections;

public class BackgroundMover : MonoBehaviour {

	//Variables relatives au BackGround
	public GameObject Background;
	private GameObject currentBackground;
	private GameObject nextBackground;
	private float roomSizeX;
	public float scrollingSpeed = 40.0f;

	// Use this for initialization
	void Start () {

		//Gestion du background (pour un seul arrière plan, il faudra intégrer le parallaxe
		currentBackground = Instantiate (Background, new Vector2 (0, 0), Quaternion.identity) as GameObject;
		roomSizeX = currentBackground.GetComponent<Collider2D> ().bounds.size.x-0.1f;
		nextBackground = Instantiate (Background, new Vector2 (roomSizeX, Background.transform.position.y), Quaternion.identity) as GameObject;
	
	}
	
	// Use this for initialization
	void FixedUpdate() 
	{
		
		if (currentBackground  ==  null)
		{
			
			currentBackground = nextBackground;
			//On récupère la taille au cas ou on serait dans une pièce spéciale de taille différente
			roomSizeX = currentBackground.GetComponent<Collider2D> ().bounds.size.x;
			
			//on soustrait 1 de RoomSize car on constate la destruction de l'objet avec une frame de retard
			nextBackground = Instantiate (Background, new Vector2 (currentBackground.transform.position.x + roomSizeX, Background.transform.position.y), Quaternion.identity) as GameObject;
			
		}
		
		currentBackground.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1 * scrollingSpeed*Time.fixedDeltaTime, 0);
		nextBackground.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1 * scrollingSpeed*Time.fixedDeltaTime, 0);
		
	}

}
