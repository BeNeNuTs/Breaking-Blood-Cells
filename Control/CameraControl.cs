using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}


public class CameraControl : MonoBehaviour {

	public float minFieldOfView = 30;
	public float maxFieldOfView = 70;
	public float sensitivity = 20;

	public float moveSensitivity = 20;

	public Boundary maxZoomBoundary;//Boundary sur le zoom max
	public Boundary minZoomBoundary;//Boundary sur le zoom min
	public Boundary CurrentBoundary;


	public static bool zoomEnabled = false;
	public static bool moveEnabled = false;

	public float zoom = 0;


	// Use this for initialization
	void Start () 
	{
	
	}

	public static IEnumerator BlendCameraTo(Vector3 position, float size, float duration, bool enableControl)
	{
		zoomEnabled = false;
		moveEnabled = false;

		float progression = 0;
		float initialSize = Camera.main.orthographicSize;
		Vector3 initialPosition = Camera.main.transform.position;
		while (progression <= 1.0f) 
		{
			yield return new WaitForSeconds(Time.deltaTime);
			progression += (Time.deltaTime / duration);
			Camera.main.transform.position = Vector3.Lerp(initialPosition,position,progression);
			Camera.main.orthographicSize = initialSize + (size - initialSize)*progression;
		}

		zoomEnabled = enableControl;
		moveEnabled = enableControl;

	}

	// Update is called once per frame
	void Update () 
	{

		//ZOOM
		if (zoomEnabled) 
		{
			float size = Camera.main.orthographicSize;
			size -= Input.GetAxis ("Mouse ScrollWheel") * sensitivity;
			size = Mathf.Clamp (size, minFieldOfView, maxFieldOfView);

			Camera.main.orthographicSize = size;
		}

		//Déplacement
		if (moveEnabled) 
		{
			float moveX = Input.GetAxisRaw ("Horizontal");
			float moveY = Input.GetAxisRaw ("Vertical");
		
		
			transform.position += new Vector3 (moveX * moveSensitivity, moveY * moveSensitivity, 0) * Time.fixedDeltaTime;

			//Calcul du clamp
			zoom = (float)(Camera.main.orthographicSize - maxFieldOfView)/(maxFieldOfView-minFieldOfView)*-1;

			CurrentBoundary.xMin = minZoomBoundary.xMin + (maxZoomBoundary.xMin - minZoomBoundary.xMin)*zoom; 
			CurrentBoundary.xMax = minZoomBoundary.xMax + (maxZoomBoundary.xMax - minZoomBoundary.xMax)*zoom; 

			CurrentBoundary.yMin = minZoomBoundary.yMin + (maxZoomBoundary.yMin - minZoomBoundary.yMin)*zoom; 
			CurrentBoundary.yMax = minZoomBoundary.yMax + (maxZoomBoundary.yMax - minZoomBoundary.yMax)*zoom; 


			transform.position =  new Vector3
				(
					Mathf.Clamp (transform.position.x, CurrentBoundary.xMin, CurrentBoundary.xMax),
					Mathf.Clamp (transform.position.y, CurrentBoundary.yMin, CurrentBoundary.yMax),
					transform.position.z
			);
		}
	
	}
}
