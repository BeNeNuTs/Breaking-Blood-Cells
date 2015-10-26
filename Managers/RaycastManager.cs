using UnityEngine;
using System.Collections;

public class RaycastManager : MonoBehaviour {
	
	void Update () {

		/*if (Input.GetMouseButtonDown (0)) 
		{
			Debug.Log("HI");
			//Ray2D ray = Camera.main.ScreenPointToRay(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit) {
				Debug.Log ("Name = " + hit.collider.name);
				Debug.Log ("Tag = " + hit.collider.tag);
				Debug.Log ("Hit Point = " + hit.point);
				Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
				Debug.Log ("--------------");
			}
		}*/
	}

	public static void RaycastControl()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000, LayerMask.GetMask("ClickLayer"));
		if (hit) 
		{
			hit.collider.gameObject.GetComponent<ClickToControl>().ControlCell();
		}
	}

	public static void RaycastMovement()
	{

	}

}