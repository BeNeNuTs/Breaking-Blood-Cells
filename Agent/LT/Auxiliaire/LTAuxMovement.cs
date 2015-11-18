using UnityEngine;
using System.Collections;

public class LTAuxMovement : AgentMovement {
	
	public static bool residus;
	public static Type.TypeResidus typeResidus;
	
	public float timeToAnalize = 3f;
	
	GameObject residusGO;
	float time;

	UnitGenerator unitGenerator;
	
	void Start(){
		typeResidus = Type.TypeResidus.NONE;
		residus = false;
		
		time = 0f;

		unitGenerator = GameObject.FindGameObjectWithTag("Base").GetComponent<UnitGenerator>();
	}

	protected override void OnTriggerEnter2D(Collider2D other){
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
	
	protected override void Update () {
		if(agent.state == LTAuxAgent.BACK_TO_BASE){
			BackToBase();
		}else if(agent.state == LTAuxAgent.ANALYZE){
			Analize();
		}else if(agent.state == Agent.WIGGLE){
			Wiggle();
		}
	}

	protected override void Wiggle(){
		// Faire avancer l'agent
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	void BackToBase(){
		if(Vector3.Distance(unitGenerator.transform.position, transform.position) < stoppingDistance){
			//ANALYZE RESIDUS
			agent.state = LTAuxAgent.ANALYZE;
			return;
		}

		Vector3 diff = unitGenerator.transform.position - transform.position;
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
	
	void Analize(){
		agentRigidbody.velocity = Vector2.zero;
		
		//AJOUT D'ANIM D'ANALISE
		
		time += Time.deltaTime;
		
		if(time > timeToAnalize){
			if(typeResidus == Type.TypeResidus.BACTERIA){
				Debug.Log("GENERER CYTOTOXIQUE");
				unitGenerator.Generate(Type.TypeUnit.LT_CYTOTOXIQUE);
			}else if(typeResidus == Type.TypeResidus.VIRUS){
				Debug.Log("GENERER LB");
				unitGenerator.Generate(Type.TypeUnit.LB);
			}
			
			time = 0f;

			Destroy(gameObject);
		}
	}
	
	public bool TakeResidus(Type.TypeResidus _typeResidus){
		if(residus || _typeResidus == Type.TypeResidus.NONE){
			return false;
		}
		
		residus = true;
		typeResidus = _typeResidus;
		
		agent.state = LTAuxAgent.BACK_TO_BASE;
		return true;
	}
}
