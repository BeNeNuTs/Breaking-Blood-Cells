using UnityEngine;
using System.Collections;

public class AgentMovement : MonoBehaviour {

	public float speed;
	public float wiggle;
	public float stoppingDistance;
	public string tagEnemy;

	Rigidbody2D agentRigidbody;
	bool findEnemy = false;


	[HideInInspector]
	public Transform target;

	// Use this for initialization
	void Start () {
		agentRigidbody = GetComponent<Rigidbody2D>();
		target = null;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag(tagEnemy)){
			findEnemy = true;
			if(target != null){
				if(Vector2.Distance(transform.position, other.transform.position) < Vector2.Distance(transform.position, target.position)){
					target = other.transform;
				}
			}else{
				target = other.transform;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag(tagEnemy) && findEnemy){
			if(other.gameObject == target.gameObject){
				findEnemy = false;
				target = null;
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.contacts.Length > 0){
			Vector3 reflect = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
			Vector3 direction = new Vector3(reflect.x, 0, reflect.z);
			transform.LookAt(direction);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(findEnemy){
			GoToEnemy ();
		}else{
			Wiggle ();
		}
	}

	void GoToEnemy(){
		if(target != null){
			//agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}else{
			target = null;
			findEnemy = false;
		}
	}

	void Wiggle(){
		// Faire avancer l'agent
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
	}
}
