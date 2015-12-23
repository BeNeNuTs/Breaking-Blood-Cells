using UnityEngine;
using System.Collections;

/// <summary>
/// La classe VirusLife hérite de la classe AgentLife
/// et permet de redéfinir les méthodes permettant de gérer la vie des virus.
/// </summary>
public class VirusLife : AgentLife {

	VirusMovement myMovement;

	void Start(){
		myMovement = GetComponent<VirusMovement>();
	}
	
	/// <summary>
	/// Inflige des dégats à l'agent.
	/// </summary>
	/// <param name="amount">Quantité de dégats reçus.</param>
	/// <param name="virus">Si <c>true</c> alors c'est un virus qui inflige des dégats.</param>
	public override void TakeDamage (int amount, bool virus = false)
	{
		if(agent.state != VirusAgent.DUPLICATE){
			base.TakeDamage(amount);
		}else{
			myMovement.TakeDamageToCellControled(amount);
		}
	}
}
