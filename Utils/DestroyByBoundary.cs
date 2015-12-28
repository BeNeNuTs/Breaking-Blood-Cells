using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "MenuBackground") 
		{
			Destroy(collider.gameObject);
		} 
		
	}
}
