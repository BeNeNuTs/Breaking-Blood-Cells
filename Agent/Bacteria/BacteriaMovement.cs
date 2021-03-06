﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// La classe BacteriaMovement hérite de la classe AgentMovement
/// et permet de déplacer une bactérie.
/// </summary>
public class BacteriaMovement : AgentMovement {

	[HideInInspector]
	public List<GameObject> bacterias;

	const uint NB_BACTERIAS_TO_ATTACK = 3;

	/// <summary>
	/// Vérifie ce qui entre dans le percepts de l'agent.
	/// </summary>
	/// <param name="other">Other.</param>
	protected override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D(other);

		/*if (other.CompareTag ("LTAux") && other.GetType () == typeof(BoxCollider2D)) {
			if (!targets.Contains (other.gameObject)) {
				targets.Add (other.gameObject);
			}
		}*/

		if(other.CompareTag(gameObject.tag) && other.GetType() == typeof(BoxCollider2D)){
			if(!bacterias.Contains(other.gameObject)){
				bacterias.Add(other.gameObject);
			}
		}
	}

	/// <summary>
	/// Vérifie ce qui sort dans le percepts de l'agent.
	/// </summary>
	/// <param name="other">Other.</param>
	protected override void OnTriggerExit2D(Collider2D other){
		base.OnTriggerExit2D(other);

		if(other.CompareTag(gameObject.tag) && other.GetType() == typeof(BoxCollider2D)){
			if(Vector3.Distance(transform.position, other.transform.position) > GetComponent<CircleCollider2D>().radius){
				bacterias.Remove(other.gameObject);
			}
		}
	}

	/// <summary>
	/// Vérifie l'état de l'agent.
	/// </summary>
	protected override void Update () {
		if(agent.state == BacteriaAgent.FLEE){
			Flee ();
		}else{
			base.Update();
		}
	}

	/// <summary>
	/// Déplacer l'agent vers l'ennemi le plus proche
	/// si les bactéries sont en surnombre.
	/// </summary>
	protected override void GoToEnemy(){
		UpdateList(bacterias);
		UpdateList(targets);

		if(targets.Count > 0){
			GameObject closest = GetClosestTarget(targets);
			if(closest == null){
				return;
			}

			if(closest.name.Contains("Cell"))
			{
				base.GoToEnemy();
			}
			else if(bacterias.Count < NB_BACTERIAS_TO_ATTACK){
				agent.state = BacteriaAgent.FLEE;
				return;
			}
			else
			{
				base.GoToEnemy();
				return;
			}

		}else{
			agent.state = Agent.WIGGLE;
		}
	}

	/// <summary>
	/// Permet à la bactérie de fuir face à l'ennemi si
	/// elles sont en sousnombre.
	/// </summary>
	void Flee(){
		UpdateList(targets);

		if(bacterias.Count >= NB_BACTERIAS_TO_ATTACK && targets.Count > 0){
			agent.state = BacteriaAgent.ATTACK;
			return;
		}
		
		if(targets.Count > 0){
			GameObject closest = GetClosestTarget(targets);
			if(closest == null){
				agent.state = BacteriaAgent.WIGGLE;
				return;
			}
			
			Vector3 diff = closest.transform.position - transform.position;
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

			agentRigidbody.rotation = rot_z + 180f;
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}else{
			agent.state = BacteriaAgent.WIGGLE;
			return;
		}
	}
}
