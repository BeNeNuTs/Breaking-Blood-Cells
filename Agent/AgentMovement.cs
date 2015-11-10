using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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


	// Use this for initialization
	void Awake () {
		agentRigidbody = GetComponent<Rigidbody2D>();
		targets = new List<GameObject>();
		agent = GetComponent<Agent>();
	}

	protected virtual void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && other.GetType() == typeof(BoxCollider2D)){
			if(!targets.Contains(other.gameObject)){
				targets.Add(other.gameObject);
			}
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && other.GetType() == typeof(BoxCollider2D)){
			targets.Remove(other.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		/*if(collision.contacts.Length > 0){
			Vector3 reflect = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
			Vector3 direction = new Vector3(reflect.x, 0, reflect.z);
			transform.LookAt(direction);
		}*/
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(agent.state == Agent.WIGGLE){
			Wiggle();
		}else if(agent.state == Agent.GOTOENEMY){
			GoToEnemy();
		}
	}

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

			//rotation = rot_z;
			//agentRigidbody.rotation = rotation;

			agentRigidbody.rotation = rot_z;
			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}else{
			agent.state = Agent.WIGGLE;
			return;
		}
	}

	/*public float ChangeRotation(float rot){
		rotation += rot;
		return rotation;
	}*/

	protected virtual void Wiggle(){
		if(targets.Count > 0){
			agent.state = Agent.GOTOENEMY;
			return;
		}
		// Faire avancer l'agent
		//ChangeRotation(Random.Range(-wiggle,wiggle));
		//agentRigidbody.rotation = rotation;
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	public void UpdateList(List<GameObject> list){
		for(int i = 0 ; i < list.Count ; i++){
			if(list[i] == null){
				list.RemoveAt(i);
			}
		}
	}

	public GameObject GetClosestTarget(List<GameObject> list){
		if(list.Count == 0){
			return null;
		}

		GameObject closest;
		closest = list[0];
		
		for(int i = 1 ; i < list.Count ; i++){
			if(Vector3.Distance(transform.position, list[i].transform.position) < Vector3.Distance(transform.position, closest.transform.position)){
				closest = list[i];
			}
		}

		return closest;
	}
}
