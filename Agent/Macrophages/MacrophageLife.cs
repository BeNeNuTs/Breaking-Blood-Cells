using UnityEngine;
using System.Collections;

public class MacrophageLife : AgentLife {


	protected override void Death ()
	{
		if(agent.state == MacrophageAgent.BRING_RESIDUS){
			MacrophageAttack.bringResidues = false;
		}
		
		base.Death ();
	}
}
