using UnityEngine;
using System.Collections;

public class AgentMovement : MonoBehaviour {

	public float speed;
	public float wiggle;
	public string tagEnemy;

	Rigidbody agentRigidbody;
	NavMeshAgent navMesh;

	[HideInInspector]
	public Transform target;
	bool findEnemy = false;


	// TEST
	public float turnSpeed = 90.0f;
	public float turbulence = 10.0f;

	// Use this for initialization
	void Start () {
		navMesh = GetComponent<NavMeshAgent>();
		agentRigidbody = GetComponent<Rigidbody>();
		target = null;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag(tagEnemy)){
			findEnemy = true;
			if(target != null){
				if(Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, target.position)){
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
	void FixedUpdate () {
		if(findEnemy){
			GoToEnemy ();
		}else{
			Wiggle ();
		}
	}

	void GoToEnemy(){
		if(target != null){
			navMesh.SetDestination(target.position);
		}else{
			target = null;
			findEnemy = false;
		}
	}

	void Wiggle(){

		// Faire avancer l'agent
		agentRigidbody.velocity = transform.forward * speed * Time.fixedDeltaTime;
	
		// Rotation de l'agent en Y entre -10 et +10
		Vector3 rotation = new Vector3(0,Random.Range(-wiggle,wiggle), 0) + agentRigidbody.rotation.eulerAngles;
		agentRigidbody.rotation = Quaternion.Euler(rotation);

		/*Vector3 direction = transform.position + Random.insideUnitSphere * turbulence;
		//direction.Normalize( );
		transform.rotation = Quaternion.RotateTowards( transform.rotation, Quaternion.LookRotation( direction ), turnSpeed * Time.fixedDeltaTime );
		transform.Translate( Vector3.forward * speed * Time.fixedDeltaTime );*/
	}
}
