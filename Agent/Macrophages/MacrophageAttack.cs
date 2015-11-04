using UnityEngine;
using System.Collections;

public class MacrophageAttack : AgentAttack {

	public enum TypeEnemy { BACTERIA, VIRUS }

	public GameObject residusPrefab;
	public bool residus;

	public TypeEnemy typeEnemy;

	GameObject residusGO;

	void Start(){
		residus = false;
	}

	protected override AgentLife Attack(){
		AgentLife enemyLife = base.Attack();
		if(enemyLife == null){
			return enemyLife;
		}

		if(enemyLife.currentLife < 0 && !residus){
			residusGO = Instantiate(residusPrefab, transform.position, Quaternion.identity) as GameObject;
			residusGO.transform.SetParent(transform);

			if(enemyLife.name.Contains("Bacteria")){
				typeEnemy = TypeEnemy.BACTERIA;
			}else{
				typeEnemy = TypeEnemy.VIRUS;
			}
				
			residus = true;
		}

		return enemyLife;
	}

	public void RemoveResidus(){
		Destroy(residusGO);
		residus = false;
	}
}
