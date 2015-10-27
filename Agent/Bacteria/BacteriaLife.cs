using UnityEngine;
using System.Collections;

public class BacteriaLife : AgentLife {

	public GameObject ondePrefab;

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
			Duplicate();
		}
	}

	/** Permet de dupliquer l'agent s'il possède toute sa vie et en divant sa vie par 2 */
	void Duplicate(){
		if(UnitManager.NB_BACTERIES < UnitManager.MAX_BACTERIES){
			if(agent.state == Agent.WIGGLE || agent.state == BacteriaAgent.FLEE){
				if(currentLife == startingLife){
					GameObject cellInstance = Instantiate(this.gameObject, transform.position, Quaternion.identity) as GameObject;
					AgentLife cellInstanceLife = cellInstance.GetComponent<AgentLife>();
					cellInstanceLife.currentLife = startingLife / 2f;
					cellInstanceLife.UpdateLifeImage();
					
					currentLife = startingLife / 2f;
					UpdateLifeImage();

					Instantiate(ondePrefab, transform.position, Quaternion.identity);
					
					UnitManager.NB_BACTERIES++;
				}
			}
		}
	}
}
