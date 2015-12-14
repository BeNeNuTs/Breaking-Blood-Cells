using UnityEngine;
using System.Collections;

public class MovePanel : MonoBehaviour {

	public Vector3 finalPosition;
	public float time;

	Vector3 initPosition;
	bool open;

	void Start(){
		initPosition = transform.localPosition;
		open = false;

		Debug.Log(initPosition);
	}

	public void SwitchPanel(){
		StopAllTweens();
		if(open){
			iTween.MoveTo(gameObject, iTween.Hash("position", initPosition, "time", time, "islocal", true, "easetype", iTween.EaseType.easeOutBack));
		}else{
			iTween.MoveTo(gameObject, iTween.Hash("position", finalPosition, "time", time, "islocal", true, "easetype", iTween.EaseType.easeOutBack));
		}
		open = !open;
	}

	void StopAllTweens(){
		iTween.Stop();
	}

}
