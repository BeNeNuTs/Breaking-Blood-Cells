using UnityEngine;
using System.Collections;

/// <summary>
/// La classe AntibodyMovement hérite de la classe AgentMovement
/// et permet de déplacer les anticorps.
/// </summary>
public class AntibodyMovement : AgentMovement {

	[HideInInspector] public bool hasTarget = false;

	/// <summary>
	/// Vérifie ce qui entre dans le percepts de l'agent.
	/// </summary>
	protected override void OnTriggerEnter2D(Collider2D other){
		if(other.name.Contains("Virus")){
			base.OnTriggerEnter2D(other);
		}
	}
	
	/// <summary>
	/// Permet de faire avancer l'agent.
	/// </summary>
	protected override void Wiggle ()
	{
		if(targets.Count > 0){
			UpdateList(targets);
			
			if(targets.Count > 0){
				GameObject closest = GetClosestTarget(targets);
				if(closest == null){
					GoForward();
					return;
				}

				if(!closest.GetComponent<AgentMovement>().enabled){
					GoForward();
					return;
				}

				hasTarget = true;
				agent.state = Agent.GOTOENEMY;
				return;
			}

		}

		GoForward();
	}

	/// <summary>
	/// Avancer.
	/// </summary>
	void GoForward(){
		hasTarget = false;
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
}
