using UnityEngine;
using System.Collections;

public class AntibodyMovement : AgentMovement {

	[HideInInspector] public bool hasTarget = false;

	protected override void OnTriggerEnter2D(Collider2D other){
		if(other.name.Contains("Virus")){
			base.OnTriggerEnter2D(other);
		}
	}
	
	// Update is called once per frame
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

	void GoForward(){
		hasTarget = false;
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
}
