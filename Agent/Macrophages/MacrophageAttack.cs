﻿using UnityEngine;
using System.Collections;

public class MacrophageAttack : AgentAttack {

	public static bool bringResidus = false;

	public GameObject residusPrefab;
	public Type.TypeResidus typeResidus;

	GameObject residusGO;

	void Start(){
		bringResidus = false;
		typeResidus = Type.TypeResidus.NONE;
	}

	protected override AgentLife Attack(){
		AgentLife enemyLife = base.Attack();

		if(enemyLife == null){
			return enemyLife;
		}

		if(enemyLife.currentLife < 0 && !bringResidus){
			residusGO = Instantiate(residusPrefab, transform.position, Quaternion.identity) as GameObject;
			residusGO.transform.SetParent(transform);

			if(enemyLife.name.Contains("Bacteria")){
				typeResidus = Type.TypeResidus.BACTERIA;
			}else{
				typeResidus = Type.TypeResidus.VIRUS;
			}
				
			bringResidus = true;

			agent.state = MacrophageAgent.BRING_RESIDUS;
		}

		return enemyLife;
	}

	public void RemoveResidus(){
		Destroy(residusGO);
		typeResidus = Type.TypeResidus.NONE;
	}
}
