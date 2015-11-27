using UnityEngine;
using System.Collections;

public class ImpactAutoDestroy : MonoBehaviour {

	public AnimationClip ImpactAnim;
	
	
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, ImpactAnim.length);
	}
}
