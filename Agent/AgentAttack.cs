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
		
		if(timer >= timeBetweenAttacks && myMovement.target != null && myLife.currentLife > 0)
		{
			if(Vector2.Distance(myMovement.target.position, transform.position) < myMovement.stoppingDistance){
				Attack (); 
			}
		}
	}
	
	
	void Attack ()
	{
		timer = 0f;

		if(myMovement.target == null)
			return;

		AgentLife enemyLife = myMovement.target.GetComponent<AgentLife>();

		if(enemyLife.currentLife > 0)
		{
			enemyLife.TakeDamage (attackDamage);
		}
	}
}
