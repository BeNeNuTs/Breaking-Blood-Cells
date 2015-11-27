using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BacteriaMovement : AgentMovement {

	[HideInInspector]
	public List<GameObject> bacterias;

	const uint NB_BACTERIAS_TO_ATTACK = 3;

	protected override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D(other);

		if(other.CompareTag(gameObject.tag) && other.GetType() == typeof(BoxCollider2D)){
			if(!bacterias.Contains(other.gameObject)){
				bacterias.Add(other.gameObject);
			}
		}
	}
	
	protected override void OnTriggerExit2D(Collider2D other){
		base.OnTriggerExit2D(other);

		if(other.CompareTag(gameObject.tag) && other.GetType() == typeof(BoxCollider2D)){
			if(Vector3.Distance(transform.position, other.transform.position) > GetComponent<CircleCollider2D>().radius){
				bacterias.Remove(other.gameObject);
			}
		}
	}

	protected override void Update () {
		if(agent.state == BacteriaAgent.FLEE){
			Flee ();
		}else{
			base.Update();
		}
	}

	protected override void GoToEnemy(){
		UpdateList(bacterias);
		UpdateList(targets);

		if(targets.Count > 0){
			GameObject closest = GetClosestTarget(targets);
			if(closest == null){
				return;
			}

			if(closest.name.Contains("Cell")){
				base.GoToEnemy();
			}else if(bacterias.Count < NB_BACTERIAS_TO_ATTACK){
				agent.state = BacteriaAgent.FLEE;
				return;
			}
		}else{
			agent.state = Agent.WIGGLE;
		}
	}

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
