using UnityEngine;
using System.Collections;

public class AgentMovement : MonoBehaviour {

	public float speed;
	public float wiggle;
	public float stoppingDistance;
	public string tagEnemy;

	Rigidbody2D agentRigidbody;
	bool findEnnemy = false;


	[HideInInspector]
	public Transform target;

	// Use this for initialization
	void Start () {
		agentRigidbody = GetComponent<Rigidbody2D>();
		target = null;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(tagEnemy)){
			findEnnemy = true;
			if(target != null){
				if(Vector2.Distance(transform.position, other.transform.position) < Vector2.Distance(transform.position, target.position)){
					target = other.transform;
				}
			}else{
				target = other.transform;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && findEnnemy){
			if(other.gameObject == target.gameObject){
				findEnnemy = false;
				target = null;
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		/*if(collision.contacts.Length > 0){
			Vector3 reflect = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
			Vector3 direction = new Vector3(reflect.x, 0, reflect.z);
			transform.LookAt(direction);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		if(findEnnemy){
			GoToEnemy ();
		}else{
			Wiggle ();
		}
	}

	void GoToEnnemy(){
		if(target != null){
			Vector3 diff = target.transform.position - transform.position;
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}else{
			target = null;
			findEnnemy = false;
		}
	}

	void Wiggle(){
		// Faire avancer l'agent
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}
}
