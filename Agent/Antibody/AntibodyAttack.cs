using UnityEngine;
using System.Collections;

public class AntibodyAttack : AgentAttack {

	public float timeBeforeDestroy = 5f;

	float timeDestroy = 0f;
	bool freezeEnemy = false;

	AntibodyMovement antibodyMovement;
	SpriteRenderer sprite;

	void Start(){
		antibodyMovement = GetComponent<AntibodyMovement>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	protected override void Update ()
	{
		base.Update ();

		timeDestroy += Time.deltaTime;
		if(!freezeEnemy && !antibodyMovement.hasTarget){
			FadeSpriteRenderer();
		}else{
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
		}

		if(timeDestroy > timeBeforeDestroy && !freezeEnemy && !antibodyMovement.hasTarget){
			Destroy(gameObject);
		}
	}

	protected override AgentLife Attack ()
	{
		if(timer < timeBetweenAttacks || myMovement.targets.Count == 0)
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
			freezeEnemy = true;
			AgentMovement enemyMovement = enemyLife.GetComponent<AgentMovement>();
			enemyMovement.agentRigidbody.velocity = Vector2.zero;
			enemyMovement.enabled = false;
			enemyLife.GetComponent<AgentAttack>().enabled = false;

			agent.state = AntibodyAgent.CALL_MACROPHAGE;
		}
		
		return enemyLife;
	}

	void FadeSpriteRenderer(){
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1 - (timeDestroy / timeBeforeDestroy));
	}
}
