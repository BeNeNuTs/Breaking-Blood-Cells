using UnityEngine;
using System.Collections;

/// <summary>
/// La classe AgentAttack définie les différentes méthodes permettant aux
/// agents d'attaquer.
/// </summary>
public class AgentAttack : MonoBehaviour {
	
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	protected Agent agent;
	protected AgentMovement myMovement;

	protected AgentLife myLife;
	protected float timer;

	public GameObject impactAttack; 
	
	void Awake ()
	{
		myLife = GetComponent<AgentLife>();
		myMovement = GetComponent<AgentMovement>();

		agent = GetComponent<Agent>();
	}

	/// <summary>
	/// Vérifie l'état de l'agent pour le faire attaquer.
	/// </summary>
	protected virtual void Update ()
	{
		timer += Time.deltaTime;

		if(agent.state == Agent.ATTACK){
			Attack();
		}
	}

	/// <summary>
	/// Vérifie si l'agent peut attaquer.
	/// </summary>
	/// <returns><c>true</c>, si le timer est vérifié, <c>false</c> sinon.</returns>
	public bool CheckTimer(){
		return (timer > timeBetweenAttacks);
	}
	
	/// <summary>
	/// Méthode d'attaque de l'agent.
	/// </summary>
	protected virtual AgentLife Attack ()
	{
		if(CheckTimer() || myMovement.targets.Count == 0 || myLife.currentLife == 0)
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
		if(enemyLife != null)
			if(enemyLife.currentLife > 0)
			{
				enemyLife.TakeDamage (attackDamage);
				if(impactAttack != null)
					Instantiate(impactAttack,closest.transform.position,Quaternion.identity);
				
			}

		return enemyLife;
	}
}
