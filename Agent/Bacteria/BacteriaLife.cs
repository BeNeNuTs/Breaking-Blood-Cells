using UnityEngine;
using System.Collections;

public class BacteriaLife : AgentLife {

	float timer = 0f;
	float timeMinToWait = 2f;
	float timeMaxToWait = 5f;
	float timeToWait;
	float percentageGainLife = 0.1f;

	void Start(){
		timeToWait = Random.Range(timeMinToWait, timeMaxToWait);
	}

	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		timer += Time.deltaTime;
		if(timer > timeToWait){
			timer = 0f;
			timeToWait = Random.Range(timeMinToWait, timeMaxToWait);
			AddLife(Mathf.FloorToInt(startingLife * percentageGainLife));

			//Try to duplicate
			if(UnitManager.NB_BACTERIES < UnitManager.MAX_BACTERIES){
				if(agent.state == Agent.WIGGLE || agent.state == BacteriaAgent.FLEE){
					Duplicate();
				}
			}
		}
	}

	protected override void Death(){
		UnitManager.NB_BACTERIES--;
		base.Death();
	}
}
