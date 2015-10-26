using UnityEngine;
using System.Collections;

public class AgentAttack : MonoBehaviour {
	
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	
	AgentLife myLife;
	AgentMovement myMovement;

	Agent agent;

	float timer;
	
	void Awake ()
	{
		myLife = GetComponent<AgentLife>();
		myMovement = GetComponent<AgentMovement>();

		agent = GetComponent<Agent>();

	}
	
	void Update ()
	{
		timer += Time.deltaTime;

		if(agent.state == Agent.ATTACK){
			Attack();
		}
	}
	
	
	void Attack ()
	{
		if(timer < timeBetweenAttacks || myMovement.targets.Count == 0 || myLife.currentLife == 0)
		{
			agent.state = Agent.WIGGLE;
			return;
		}

		myMovement.UpdateList();
		GameObject closest = myMovement.GetClosestTarget();

		if(closest == null){
			agent.state = Agent.WIGGLE;
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
