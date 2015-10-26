using UnityEngine;
using System.Collections;

public class AgentAttack : MonoBehaviour {
	
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	
	AgentLife myLife;
	AgentMovement myMovement;

	float timer;
	
	void Awake ()
	{
		myLife = GetComponent<AgentLife>();
		myMovement = GetComponent<AgentMovement>();
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && myMovement.targets.Count > 0 && myLife.currentLife > 0)
		{
			myMovement.UpdateList();
			GameObject closest = myMovement.GetClosestTarget();
			Attack (closest); 
		}
	}
	
	
	void Attack (GameObject closest)
	{
		if(Vector3.Distance(closest.transform.position, transform.position) > myMovement.stoppingDistance){
			return;
		}

		if(closest == null){
			return;
		}

		timer = 0f;

		AgentLife enemyLife = closest.GetComponent<AgentLife>();

		if(enemyLife.currentLife > 0)
		{
			enemyLife.TakeDamage (attackDamage);
		}
	}
}
