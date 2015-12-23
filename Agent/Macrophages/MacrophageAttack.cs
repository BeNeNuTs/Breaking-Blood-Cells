using UnityEngine;
using System.Collections;

/// <summary>
/// La classe MacrophageAttack hérite de la classe AgentAttack
/// et permet de redéfinir les méthodes d'attaque des macrophages.
/// </summary>
public class MacrophageAttack : AgentAttack {

	public static bool bringResidues = false;
	public static bool residuesDone = false;


	public GameObject residuBacteriaSprite;
	public GameObject residuVirusSprite;
	public Type.TypeResidus typeResidus;
	

	void Start(){
		typeResidus = Type.TypeResidus.NONE;
	}

	/// <summary>
	/// Méthode d'attaque de l'agent.
	/// </summary>
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

	/// <summary>
	/// Permet de supprimer les résidus du macrophage.
	/// </summary>
	public void RemoveResidus(){
		residuBacteriaSprite.SetActive(false);
		residuVirusSprite.SetActive(false);
		typeResidus = Type.TypeResidus.NONE;
	}
}
