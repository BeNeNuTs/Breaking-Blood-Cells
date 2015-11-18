using UnityEngine;
using System.Collections;

public class LBAttack : AgentAttack {

	public GameObject antibodiesPrefab;
	public int nb_antibodies = 10;
	public float angle_between_antibodies = 36f;

	protected override AgentLife Attack ()
	{
		if(timer < timeBetweenAttacks || myMovement.targets.Count == 0 || myLife.currentLife == 0)
		{
			agent.state = Agent.WIGGLE;
			return null;
		}
		
		myMovement.UpdateList(myMovement.targets);
		if(myMovement.targets.Count == 0){
			agent.state = Agent.WIGGLE;
			return null;
		}
		
		timer = 0f;
		
		GenerateAntibodies();
		
		return null;
	}

	void GenerateAntibodies(){
		for(int i = 0 ; i < nb_antibodies ; i++){
			// Make sure our antibodies spread out in an even pattern.
			float angle = i * angle_between_antibodies - ((angle_between_antibodies / 2) * (nb_antibodies - 1));
			//Quaternion rot = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
			Quaternion rot = Quaternion.Euler(new Vector3(0,0,angle));
			
			//Instanciate and initialize antibodies
			Instantiate(antibodiesPrefab, transform.position, rot);
		}
	}
}
