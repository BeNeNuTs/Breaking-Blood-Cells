using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// La classe AgentMovement permet de déplacer l'agent.
/// </summary>
public class AgentMovement : MonoBehaviour {

	public float speed;
	public float wiggle;
	public float stoppingDistance;
	public string tagEnemy;

	protected Agent agent;
	protected float rotation;

	[HideInInspector]
	public List<GameObject> targets;
	[HideInInspector]
	public Rigidbody2D agentRigidbody;

	public Boundary moveBoundaries;


	/// <summary>
	/// Initialise la classe.
	/// </summary>
	void Awake () {
		agentRigidbody = GetComponent<Rigidbody2D>();
		targets = new List<GameObject>();
		agent = GetComponent<Agent>();
		moveBoundaries.xMin = -225;
		moveBoundaries.xMax = 225;
		moveBoundaries.yMin = -125;
		moveBoundaries.yMax = 120;
	}

	/// <summary>
	/// Vérifie ce qui entre dans le percepts de l'agent.
	/// </summary>
	protected virtual void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && other.GetType() == typeof(BoxCollider2D)){
			if(!targets.Contains(other.gameObject)){
				targets.Add(other.gameObject);
			}
		}
	}

	/// <summary>
	/// Vérifie ce qui sort dans le percepts de l'agent.
	/// </summary>
	protected virtual void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && other.GetType() == typeof(BoxCollider2D)){
			if(Vector3.Distance(transform.position, other.transform.position) >= GetComponent<CircleCollider2D>().radius){
				targets.Remove(other.gameObject);
			}
		}
	}
	
	/// <summary>
	/// Vérifie l'état de l'agent et clamp sa position.
	/// </summary>
	protected virtual void Update () {


		if(agent.state == Agent.WIGGLE){
			Wiggle();
		}else if(agent.state == Agent.GOTOENEMY){
			GoToEnemy();
		}

		
		transform.position =  new Vector3
			(
				Mathf.Clamp (transform.position.x, moveBoundaries.xMin, moveBoundaries.xMax),
				Mathf.Clamp (transform.position.y, moveBoundaries.yMin, moveBoundaries.yMax),
				transform.position.z
				);


	}

	/// <summary>
	/// Déplacer l'agent vers l'ennemi le plus proche.
	/// </summary>
	protected virtual void GoToEnemy(){

		UpdateList(targets);

		if(targets.Count > 0){
			GameObject closest = GetClosestTarget(targets);
			if(closest == null){
				return;
			}

			if(Vector3.Distance(closest.transform.position, transform.position) < stoppingDistance){
			
				agent.state = Agent.ATTACK;
				return;
			}

			Vector3 diff = closest.transform.position - transform.position;
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

			agentRigidbody.rotation = rot_z;
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}else{
			agent.state = Agent.WIGGLE;
			return;
		}
	}

	/// <summary>
	/// Permet de faire avancer l'agent.
	/// </summary>
	protected virtual void Wiggle(){
		if(targets.Count > 0){
			agent.state = Agent.GOTOENEMY;
			return;
		}
		// Faire avancer l'agent
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	/// <summary>
	/// Met à jour la liste passée en paramètre.
	/// </summary>
	/// <param name="list">Liste à vérifier.</param>
	public void UpdateList(List<GameObject> list){
		for(int i = 0 ; i < list.Count ; i++)
		{

			if(list[i] == null){
				list.RemoveAt(i);
			}else if(!list[i].GetComponent<BoxCollider2D>().enabled){
				list.RemoveAt(i);
			}
			else if(Vector3.Distance(transform.position, list[i].transform.position) > GetComponent<CircleCollider2D>().radius)
			{
				list.RemoveAt(i);	
			}
		
		}
	}

	/// <summary>
	/// Retourne l'ennemi le plus proche dans la liste passée en paramètre.
	/// </summary>
	/// <returns>La cible la plus proche.</returns>
	/// <param name="list">Liste d'ennemis.</param>
	public GameObject GetClosestTarget(List<GameObject> list){
		if(list.Count == 0){
			return null;
		}

		GameObject closest;
		closest = list[0];
		
		for(int i = 1 ; i < list.Count ; i++){
			if(closest == null || list[i] == null)
				continue;

			if(Vector3.Distance(transform.position, list[i].transform.position) < Vector3.Distance(transform.position, closest.transform.position)){
				closest = list[i];
			}
		}

		return closest;
	}
}
