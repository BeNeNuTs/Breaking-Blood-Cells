using UnityEngine;
using System.Collections;

public class MacrophageAttack : AgentAttack {

	public static bool bringResidues = false;
	public static bool residuesDone = false;

	public GameObject residusPrefab;
	public Type.TypeResidus typeResidus;

	GameObject residusGO;

	void Start(){
		bringResidues = false;
		residuesDone = false;

		typeResidus = Type.TypeResidus.NONE;
	}

	protected override AgentLife Attack(){
		Debug.Log ("Alataque");
		AgentLife enemyLife = base.Attack();

		if(enemyLife == null){
			Debug.Log("NUL");
			return enemyLife;
		}

		if(enemyLife.currentLife <= 0 && !bringResidues && !residuesDone){

			residusGO = Instantiate(residusPrefab, transform.position, Quaternion.identity) as GameObject;
			residusGO.transform.SetParent(transform);

			if(enemyLife.name.Contains("Bacteria")){
				typeResidus = Type.TypeResidus.BACTERIA;
			}else{
				typeResidus = Type.TypeResidus.VIRUS;
			}

			bringResidues = true;
			agent.state = MacrophageAgent.BRING_RESIDUS;
		}

		return enemyLife;
	}

	public void RemoveResidus(){
		Destroy(residusGO);
		typeResidus = Type.TypeResidus.NONE;
	}
}
