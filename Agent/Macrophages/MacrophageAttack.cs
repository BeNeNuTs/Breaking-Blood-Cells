using UnityEngine;
using System.Collections;

public class MacrophageAttack : AgentAttack {

	protected override AgentLife Attack(){
		AgentLife enemyLife = base.Attack();
		if(enemyLife == null){
			return enemyLife;
		}

		if(enemyLife.currentLife < 0){
			Debug.LogWarning("RESIDUS !");
		}

		return enemyLife;
	}
}
