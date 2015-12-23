using UnityEngine;
using System.Collections;

/// <summary>
/// La classe MacrophageLife hérite de la classe AgentLife
/// et permet de redéfinir les méthodes permettant de gérer la vie des macrophages.
/// </summary>
public class MacrophageLife : AgentLife {

	/// <summary>
	/// Permet de faire mourir l'agent lorsqu'il n'a plus de vie.
	/// </summary>
	protected override void Death ()
	{
		if(agent.state == MacrophageAgent.BRING_RESIDUS){
			MacrophageAttack.bringResidues = false;
		}
		
		base.Death ();
	}
}
