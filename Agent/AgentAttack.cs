using UnityEngine;
using System.Collections;

public class AgentAttack : MonoBehaviour {
	
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	protected Agent agent;
	protected AgentMovement myMovement;

	protected AgentLife myLife;
	protected float timer;
	
	void Awake ()
	{
		myLife = GetComponent<AgentLife>();
		myMovement = GetComponent<AgentMovement>();

		agent = GetComponent<Agent>();
	}
	
	protected virtual void Update ()
	{
		timer += Time.deltaTime;

		if(agent.state == Agent.ATTACK){
			Attack();
		}
	}

	public bool CheckTimer(){
		return (timer > timeBetweenAttacks);
	}
	
	
	protected virtual AgentLife Attack ()
	{
		if(timer < timeBetweenAttacks || myMovement.targets.Count == 0 || myLife.currentLife == 0)
		{
			agent.state = Agent.WIGGLE;
			return null;
		}

		myMovement.UpdateList(myMovement.targets);
		GameObject closest = myMovement.GetClosestTarget(myMovement.targets);

		if(closest == null){
			agent.state = Agent.WIGGLE;
			return null;
		}

		myMovement.agentRigidbody.velocity = Vector2.zero;

		timer = 0f;

		AgentLife enemyLife = closest.GetComponent<AgentLife>();

		if(enemyLife.currentLife > 0)
		{
			enemyLife.TakeDamage (attackDamage);
		}

		return enemyLife;
	}
}
