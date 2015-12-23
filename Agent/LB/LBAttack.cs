using UnityEngine;
using System.Collections;

/// <summary>
/// La classe LBAttack hérite de la classe AgentAttack
/// et permet de redéfinir les méthodes d'attaques pour les lymphocytes B.
/// </summary>
public class LBAttack : AgentAttack {

	public GameObject antibodiesPrefab;
	public int nb_antibodies = 10;
	public float angle_between_antibodies = 36f;

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
		if(myMovement.targets.Count == 0){
			agent.state = Agent.WIGGLE;
			return null;
		}
		
		timer = 0f;
		
		GenerateAntibodies();
		
		return null;
	}

	/// <summary>
	/// Permet à l'agent de générer des anticorps.
	/// </summary>
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
