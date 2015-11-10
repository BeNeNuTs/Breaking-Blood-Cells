using UnityEngine;
using System.Collections;

public class BoundsManager : MonoBehaviour {

	public Vector3 normal;

	public float offset = -5f;

	void OnTriggerEnter2D(Collider2D other) {
		if((other.CompareTag("Cell") && other.GetType() == typeof(BoxCollider2D)) || (other.CompareTag("Enemy") && other.GetType() == typeof(BoxCollider2D))){

			Vector3 reflect = Vector3.Reflect(other.transform.right, normal);
			Vector3 direction = new Vector3(reflect.x, reflect.y, 0);

			Vector3 diff = (other.transform.position + direction) - other.transform.position;
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

			AgentMovement agentMovement = other.gameObject.GetComponent<AgentMovement>();
			agentMovement.agentRigidbody.rotation = rot_z;

			/*AgentMovement agentMovement = other.gameObject.GetComponent<AgentMovement>();
			if(normal.x == 0f){
				if(agentMovement.agentRigidbody.position.y >= transform.position.y + offset){
					agentMovement.agentRigidbody.position = new Vector2(agentMovement.agentRigidbody.position.x, transform.position.y + offset);
				}
			}else{
				if(agentMovement.agentRigidbody.position.x >= transform.position.x + offset){
					agentMovement.agentRigidbody.position = new Vector2(transform.position.x + offset, agentMovement.agentRigidbody.position.y);
				}
			}*/
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		/*if((other.CompareTag("Cell") && other.GetType() == typeof(BoxCollider2D)) || (other.CompareTag("Enemy") && other.GetType() == typeof(BoxCollider2D))){
			other.gameObject.GetComponent<Agent>().state = Agent.WIGGLE;
		}*/
	}
}
