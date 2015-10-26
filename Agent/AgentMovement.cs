using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentMovement : MonoBehaviour {

	public float speed;
	public float wiggle;
	public float stoppingDistance;
	public string tagEnemy;

	Rigidbody2D agentRigidbody;

	//[HideInInspector]
	public List<GameObject> targets;

	// Use this for initialization
	void Start () {
		agentRigidbody = GetComponent<Rigidbody2D>();
		targets = new List<GameObject>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && !other.isTrigger){
			if(!targets.Contains(other.gameObject)){
				targets.Add(other.gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag(tagEnemy) && !other.isTrigger){
			targets.Remove(other.gameObject);
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
		if(targets.Count > 0){
			GoToEnemy ();
		}else{
			Wiggle ();
		}
	}

	void GoToEnemy(){
		UpdateList();

		if(targets.Count > 0){
			GameObject closest = GetClosestTarget();
			if(closest == null){
				return;
			}

			Vector3 diff = closest.transform.position - transform.position;
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

			agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
		}
	}

	void Wiggle(){
		// Faire avancer l'agent
		agentRigidbody.rotation += Random.Range(-wiggle,wiggle);
		agentRigidbody.velocity = new Vector2(transform.right.x, transform.right.y) * speed * Time.deltaTime;
	}

	public void UpdateList(){
		for(int i = 0 ; i < targets.Count ; i++){
			if(targets[i] == null){
				targets.RemoveAt(i);
			}
		}
	}

	public GameObject GetClosestTarget(){
		if(targets.Count == 0){
			return null;
		}

		GameObject closest;
		closest = targets[0];
		
		for(int i = 1 ; i < targets.Count ; i++){
			if(Vector3.Distance(transform.position, targets[i].transform.position) < Vector3.Distance(transform.position, closest.transform.position)){
				closest = targets[i];
			}
		}

		return closest;
	}
}
