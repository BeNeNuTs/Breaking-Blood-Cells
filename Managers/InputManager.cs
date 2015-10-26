using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {


	public enum InputMode
	{
		Default,
		Controlling
	}

	public static InputMode mode = InputMode.Default;
	

	/*Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
         diff.Normalize();
 
         float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(0f, 0f, rot_z);*/


	// Update is called once per frame
	void Update () 
	{
		switch (mode) {
		case (InputMode.Default):
			if (Input.GetButtonDown ("Fire1")) {
				RaycastManager.RaycastControl ();
			}

			break;
		case (InputMode.Controlling) :
			if (Input.GetButtonDown ("Fire1")) {
				RaycastManager.RaycastControl ();
				PlayerController.DefineNewDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}

			break;
		}

	
	}
}
