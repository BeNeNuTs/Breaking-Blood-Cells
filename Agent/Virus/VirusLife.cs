using UnityEngine;
using System.Collections;

public class VirusLife : AgentLife {

	VirusMovement myMovement;

	void Start(){
		myMovement = GetComponent<VirusMovement>();
	}

	public override void TakeDamage (int amount, bool virus = false)
	{
		if(agent.state != VirusAgent.DUPLICATE){
			base.TakeDamage(amount);
		}else{
			myMovement.TakeDamageToCellControled(amount);
		}
	}
}
