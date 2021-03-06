﻿using UnityEngine;
using System.Collections;

/// <summary>
/// La classe LTCytoAttack hérite de la classe AgentAttack
/// et permet de redéfinir les méthodes d'attaque des lymphocytes T Cytotoxique.
/// </summary>
public class LTCytoAttack : AgentAttack {

	public float dashForce = 500f;

	TrailRenderer trail;

	void Start(){
		trail = GetComponent<TrailRenderer>();
	}

	/// <summary>
	/// Méthode d'attaque de l'agent.
	/// </summary>
	protected override AgentLife Attack ()
	{
		if(timer < timeBetweenAttacks || myMovement.targets.Count == 0 || myLife.currentLife == 0)
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
		
		timer = 0f;

		AgentLife enemyLife = closest.GetComponent<AgentLife>();
		if(enemyLife == null){
			return enemyLife;
		}

		Vector3 dir = enemyLife.transform.position - transform.position;

		trail.enabled = true;
		myMovement.agentRigidbody.AddForce(dir * dashForce);
	
		StartCoroutine(HideTrail());

		if(enemyLife.currentLife > 0)
		{
			enemyLife.TakeDamage (attackDamage);
		}
		
		return enemyLife;
	}

	/// <summary>
	/// Permet de cacher la trainée derrière l'agent.
	/// </summary>
	/// <returns>The trail.</returns>
	IEnumerator HideTrail(){
		yield return new WaitForSeconds(trail.time);
		trail.enabled = false;
	}
}
