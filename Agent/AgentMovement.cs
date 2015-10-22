using UnityEngine;
using System.Collections;

public class AgentMovement : MonoBehaviour {

	public float speed;
	public float wiggle;

	Rigidbody agentRigidbody;
	NavMeshAgent navMesh;

	Transform target;
	bool findEnemy = false;

	// Use this for initialization
	void Start () {
		navMesh = GetComponent<NavMeshAgent>();
		agentRigidbody = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Enemy") && !findEnemy){
			findEnemy = true;
			target = other.transform;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Enemy") && findEnemy){
			if(other.gameObject == target.gameObject){
				findEnemy = false;
				target = null;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(findEnemy){
			GoToEnemy ();
		}else{
			Wiggle ();
		}
	}

	void GoToEnemy(){
		navMesh.SetDestination(target.position);
	}

	void Wiggle(){
		// Faire avancer l'agent
		agentRigidbody.velocity = transform.forward * speed * Time.fixedDeltaTime;
	
		// Rotation de l'agent en Y entre -10 et +10
		Vector3 rotation = new Vector3(0,Random.Range(-wiggle,wiggle), 0) + agentRigidbody.rotation.eulerAngles;
		agentRigidbody.rotation = Quaternion.Euler(rotation);
	}
}
