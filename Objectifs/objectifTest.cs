using UnityEngine;
using System.Collections;

public class objectifTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("processTest");
	}

	IEnumerator processTest(){
		yield return new WaitForSeconds(1);
	}
}
