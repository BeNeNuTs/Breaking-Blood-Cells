﻿using UnityEngine;
using System.Collections;

public class VirusMovement : AgentMovement {

	[HideInInspector]
	public GameObject target;

	public float timeBetweenDuplicate = 5f;
	float timeToDuplicate = 0f;

	AgentLife cellLife;

	public int nbVirusGenerated = 3;

	void OnEnable(){
		UpdateList(targets);
		for(int i = 0 ; i < targets.Count ; i++){
			if(Vector3.Distance(targets[i].transform.position, transform.position) > stoppingDistance){
				targets.RemoveAt(i);
			}
		}
	}

	protected override void Update ()
	{
		base.Update ();

		if(agent.state == VirusAgent.CONTROL){
			ToControl();
		}else if(agent.state == VirusAgent.DUPLICATE){
			Duplicate();
		}
	}

	void ToControl(){
		if(target != null){
			if(Vector3.Distance(target.transform.position, transform.position) < 1f){
				cellLife = target.GetComponent<AgentLife>();


				if(cellLife != null){
					cellLife.currentLife = cellLife.startingLife;
					cellLife.cellLife.color = Color.blue;
				}

				UpdateLifeCell();

				if(target.name.Contains("Cell"))
				{
					GameManager.gameManager.GetComponent<ObjectifManager>().updateGoal(7);
				}
				agent.state = VirusAgent.DUPLICATE;
				return;
			}
			
			Vector3 diff = target.transform.position - transform.position;
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			
			agentRigidbody.rotation = rot_z;
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}else{
			GetComponent<AgentLife>().canvas.GetComponent<Canvas>().enabled = true;
			GetComponent<CircleCollider2D>().enabled = true;
			GetComponent<BoxCollider2D>().enabled = true;

			agent.state = Agent.WIGGLE;
			return;
		}
	}

	void UpdateLifeCell(){
		float percentageLife = (float)cellLife.currentLife / (float)cellLife.startingLife;
		cellLife.cellLife.fillAmount = percentageLife;
	}

	void Duplicate(){
		if(target == null){
			GetComponent<AgentLife>().canvas.GetComponent<Canvas>().enabled = true;
			GetComponent<CircleCollider2D>().enabled = true;
			GetComponent<BoxCollider2D>().enabled = true;

			agent.state = Agent.WIGGLE;
			return;
		}


		agentRigidbody.velocity = Vector2.zero;

		timeToDuplicate += Time.deltaTime;

		if(timeToDuplicate > timeBetweenDuplicate && UnitManager.NB_VIRUS < UnitManager.MAX_VIRUS){
			timeToDuplicate = 0f;

		
			GameObject virus = Instantiate(gameObject, transform.position, Quaternion.identity) as GameObject;
			virus.GetComponent<AgentLife>().currentLife = virus.GetComponent<AgentLife>().startingLife;
			virus.GetComponent<AgentLife>().canvas.GetComponent<Canvas>().enabled = true;
			virus.GetComponent<BoxCollider2D>().enabled = true;
			virus.GetComponent<CircleCollider2D>().enabled = true;
			virus.GetComponent<Agent>().state = Agent.WIGGLE;
			UnitManager.NB_VIRUS++;

			TakeDamageToCellControled(cellLife.startingLife/nbVirusGenerated);
		}
	}

	public void TakeDamageToCellControled(float amount){
		if(cellLife == null){
			GetComponent<AgentLife>().Kill();
			return;
		}

		cellLife.currentLife -= amount;
		if(cellLife.currentLife <= 0){
			cellLife.Kill();
			GetComponent<AgentLife>().Kill();
			return;
		}
		
		UpdateLifeCell();
	}
	

}
