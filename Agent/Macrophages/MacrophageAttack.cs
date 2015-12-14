using UnityEngine;
using System.Collections;

public class MacrophageAttack : AgentAttack {

	public static bool bringResidues = false;
	public static bool residuesDone = false;


	public GameObject residuBacteriaSprite;
	public GameObject residuVirusSprite;
	public Type.TypeResidus typeResidus;
	

	void Start(){
		typeResidus = Type.TypeResidus.NONE;
	}

	protected override AgentLife Attack(){
		AgentLife enemyLife = base.Attack();

		if(enemyLife == null){
			return enemyLife;
		}

		if(enemyLife.currentLife <= 0 && !bringResidues /*&& !residuesDone*/ && GameManager.canTakeResidu && Random.Range(0f,1f) > 0.5f){


			if(enemyLife.name.Contains("Bacteria")){
				Debug.Log("TakeResiduBacteria");
				typeResidus = Type.TypeResidus.BACTERIA;
				residuBacteriaSprite.SetActive(true);
			}else{
				typeResidus = Type.TypeResidus.VIRUS;
				residuVirusSprite.SetActive(true);
			}

			if (GameManager.gameManager.GetComponent<GameManager> ().simulation) 
				bringResidues = true;

			agent.state = MacrophageAgent.BRING_RESIDUS;
		}

		return enemyLife;
	}

	public void RemoveResidus(){
		Debug.Log("RemoveResiduBacteria");
		residuBacteriaSprite.SetActive(false);
		residuVirusSprite.SetActive(false);
		typeResidus = Type.TypeResidus.NONE;
	}
}
