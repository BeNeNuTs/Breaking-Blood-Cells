using UnityEngine;
using System.Collections;

public class AntibodyMovement : AgentMovement {

	[HideInInspector] public bool hasTarget = false;
	
	// Update is called once per frame
	protected override void Wiggle ()
	{
		if(targets.Count > 0){
			hasTarget = true;
			agent.state = Agent.GOTOENEMY;
			return;
		}

		hasTarget = false;
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
}
