using UnityEngine;
using System.Collections;

public class ResetRotation : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
	}
}
