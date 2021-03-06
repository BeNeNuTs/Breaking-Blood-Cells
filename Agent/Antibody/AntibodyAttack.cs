﻿using UnityEngine;
using System.Collections;

/// <summary>
/// La classe AntibodyAttack hérite de la classe AgentAttack
/// et redéfinie les méthodes d'attaque des anticorps.
/// </summary>
public class AntibodyAttack : AgentAttack {

	public float timeBeforeDestroy = 5f;
	float timeDestroy = 0f;

	public float timeAfterFreeze = 10f;
	float timeDestroyFreeze = 0f;

	bool freezeEnemy = false;

	AntibodyMovement antibodyMovement;
	SpriteRenderer sprite;

	AgentLife enemyLife;

	void Start(){
		antibodyMovement = GetComponent<AntibodyMovement>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	/// <summary>
	/// Met à jour la position et la couleur d'un anticorps.
	/// </summary>
	protected override void Update ()
	{
		base.Update ();

		if(agent.state == AntibodyAgent.CALL_MACROPHAGE){
			CallMacrophages();
		}

		timeDestroy += Time.deltaTime;
		if(!freezeEnemy && !antibodyMovement.hasTarget){
			FadeSpriteRenderer();
		}else{
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
		}

		if(timeDestroy > timeBeforeDestroy && !freezeEnemy && !antibodyMovement.hasTarget){
			Destroy(gameObject);
		}

		if(freezeEnemy && antibodyMovement.hasTarget){
			timeDestroyFreeze += Time.deltaTime;
			if(timeDestroyFreeze > timeAfterFreeze){
				UnfreezeEnemy();
			}
		}
	}

	/// <summary>
	/// Méthode d'attaque de l'agent.
	/// </summary>
	protected override AgentLife Attack ()
	{
		if(timer < timeBetweenAttacks || myMovement.targets.Count == 0)
		{
			agent.state = Agent.WIGGLE;
			return null;
		}
		
		myMovement.UpdateList(myMovement.targets);
		GameObject closest = myMovement.GetClosestTarget(myMovement.targets);
		
		if(closest == null){
			agent.state = Agent.WIGGLE;
			return null;
		}

		if(Vector3.Distance(closest.transform.position, transform.position) > myMovement.stoppingDistance){
			agent.state = Agent.WIGGLE;
			return null;
		}

		AgentLife agentLife = closest.GetComponent<AgentLife>();
		
		if(agentLife.currentLife > 0 && agentLife.GetComponent<AgentAttack>().enabled && agentLife.GetComponent<AgentMovement>().enabled)
		{
			myMovement.agentRigidbody.velocity = Vector2.zero;
			timer = 0f;

			freezeEnemy = true;
			enemyLife = agentLife;

			FreezeEnemy();

			agent.state = AntibodyAgent.CALL_MACROPHAGE;
		}
		
		return enemyLife;
	}

	/// <summary>
	/// Permet de geler un virus.
	/// </summary>
	void FreezeEnemy(){
		AgentMovement enemyMovement = enemyLife.GetComponent<AgentMovement>();
		enemyMovement.agentRigidbody.velocity = Vector2.zero;
		enemyMovement.enabled = false;
		enemyLife.GetComponent<AgentAttack>().enabled = false;
	}

	/// <summary>
	/// Permet de dégeler un virus.
	/// </summary>
	void UnfreezeEnemy(){
		if(enemyLife == null){
			Destroy(gameObject);
			return;
		}

		AgentMovement enemyMovement = enemyLife.GetComponent<AgentMovement>();
		enemyMovement.enabled = true;
		enemyLife.GetComponent<AgentAttack>().enabled = true;

		enemyLife = null;

		freezeEnemy = false;
		antibodyMovement.hasTarget = false;

		Destroy(gameObject);
	}

	/// <summary>
	/// Rend transparent un anticorps.
	/// </summary>
	void FadeSpriteRenderer(){
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1 - (timeDestroy / timeBeforeDestroy));
	}

	void CallMacrophages(){
		antibodyMovement.UpdateList(antibodyMovement.targets);

		if(timeDestroy > timeBeforeDestroy && antibodyMovement.targets.Count == 0){
			Destroy(gameObject);
		}

		//CALL MACROPHAGES
		/*GameObject[] cell = GameObject.FindGameObjectsWithTag("Cell");
		foreach(GameObject macro in cell){
			if(macro.name.Contains("Macrophage")){
				macro.GetComponent<MacrophageMovement>().GoToAntibody(transform.position);
			}
		}*/
	}
}
