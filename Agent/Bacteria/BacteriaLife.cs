using UnityEngine;
using System.Collections;

/// <summary>
/// La classe BacteriaLife hérite de la classe AgentLife
/// et permet de gérer la vie des bactéries.
/// </summary>
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
	
	/// <summary>
	/// Vérifie si la bactérie peut se dupliquer.
	/// </summary>
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
	

	/// <summary>
	/// Permet de dupliquer la bactérie si elle possède toute sa vie en divisant sa vie par deux.
	/// </summary>
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
