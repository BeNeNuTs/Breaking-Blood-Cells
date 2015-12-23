using UnityEngine;
using System.Collections;

/// <summary>
/// La classe LTAuxMovement hérite de la classe AgentMovement
/// et permet de redéfinir les méthodes de mouvements des lymphocytes T Auxiliaire.
/// </summary>
public class LTAuxMovement : AgentMovement {

	public static bool backToBase = false;
	
	public static bool residus;
	public static Type.TypeResidus typeResidus;
	
	public float timeToAnalize = 3f;
	
	public GameObject residuBacteriaSprite;
	public GameObject residuVirusSprite;
	float time;

	UnitGenerator unitGenerator;
	
	void Start(){

		time = 0f;

		unitGenerator = GameObject.FindGameObjectWithTag("Base").GetComponent<UnitGenerator>();
	}

	/// <summary>
	/// Permet de ce diriger vers un macrophage lorsque celui-ci est détecté
	/// dans le percept du lymphocyte T Auxiliaire et qu'il apporte un résidu.
	/// </summary>
	/// <param name="other">Other.</param>
	protected void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Cell") && other.GetType() == typeof(BoxCollider2D)){
			if(other.name.Contains("Macrophage") && MacrophageAttack.bringResidues){
				if(other.GetComponent<Agent>().state == MacrophageAgent.BRING_RESIDUS){

					Vector3 diff = other.transform.position - transform.position;
					float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
					agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
				}
			}
		}
	}

	/// <summary>
	/// Vérifie l'état de l'agent.
	/// </summary>
	protected override void Update () {
		if((agent.state == LTAuxAgent.BACK_TO_BASE || backToBase) && agent.state != LTAuxAgent.ANALYZE){
			BackToBase();
		}else if(agent.state == LTAuxAgent.ANALYZE){
			Analize();
		}else if(agent.state == Agent.WIGGLE){
			Wiggle();
		}
	}

	/// <summary>
	/// Permet de faire avancer l'agent.
	/// </summary>
	protected override void Wiggle(){
		// Faire avancer l'agent
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	/// <summary>
	/// Permet à l'agent de retourner à la base pour analise.
	/// </summary>
	void BackToBase(){
		if(Vector3.Distance(unitGenerator.transform.position, transform.position) < stoppingDistance){
			//ANALYZE RESIDUS
			agent.state = LTAuxAgent.ANALYZE;
			return;
		}

		Vector3 diff = unitGenerator.transform.position - transform.position;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime * 2;
	}

	/// <summary>
	/// Permet à l'agent d'analiser un résidu.
	/// </summary>
	void Analize(){
		agentRigidbody.velocity = Vector2.zero;

		time += Time.deltaTime;
		
		if(time > timeToAnalize){

			GameManager.gameManager.GetComponent<ObjectifManager>().updateGoal(3);
			if(typeResidus == Type.TypeResidus.BACTERIA){
				Debug.Log("GENERER CYTOTOXIQUE");
				residuBacteriaSprite.SetActive(false);
				if(GameManager.gameManager.GetComponent<GameManager>().simulation)
				{
					GameManager.gameManager.GetComponent<LevelAgentManager>().LTCytoSpawn.enabled = true;
					GameManager.gameManager.GetComponent<LevelAgentManager>().takeResidu = false;
				}
				//else
					//GameManager.gameManager.GetComponent<LevelBacteriaManager>().spawnCyto.enabled = true;

				GameManager.canTakeResidu = false;

				Instantiate(GameManager.gameManager.GetComponent<GameManager>().LTCyto,transform.position,Quaternion.identity);
				UnitManager.NB_LYMPHOCYTES_T++;

			}else if(typeResidus == Type.TypeResidus.VIRUS){
				Debug.Log("GENERER LB");
				residuVirusSprite.SetActive(false);

				if(GameManager.gameManager.GetComponent<GameManager>().simulation)
				{
					GameManager.gameManager.GetComponent<LevelAgentManager>().LBSpawn.enabled = true;
					GameManager.gameManager.GetComponent<LevelAgentManager>().takeResidu = false;
				}
				//else
					//GameManager.gameManager.GetComponent<LevelVirusManager>().spawnLB.enabled = true;

				//unitGenerator.Generate(Type.TypeUnit.LB);
			}
			
			time = 0f;

			backToBase = true;
	
			GetComponent<AgentLife>().Kill();

		}
	}

	/// <summary>
	/// Permet à l'agent de récupérer un résidu donné par un macrophage.
	/// </summary>
	/// <returns><c>true</c>, si le résidu à été récupéré, <c>false</c> sinon.</returns>
	/// <param name="_typeResidus">_type residus.</param>
	public bool TakeResidus(Type.TypeResidus _typeResidus){
		if(residus || _typeResidus == Type.TypeResidus.NONE){
			return false;
		}

		GameManager.gameManager.GetComponent<ObjectifManager> ().updateGoal (9);
		

		typeResidus = _typeResidus;
		if(typeResidus == Type.TypeResidus.BACTERIA){
			residuBacteriaSprite.SetActive(true);
		}else if(typeResidus == Type.TypeResidus.VIRUS){
			residuVirusSprite.SetActive(true);
		}

		if (GameManager.gameManager.GetComponent<GameManager> ().simulation) {
			residus = true;
			agent.state = LTAuxAgent.BACK_TO_BASE;
		}

		return true;
	}

}
