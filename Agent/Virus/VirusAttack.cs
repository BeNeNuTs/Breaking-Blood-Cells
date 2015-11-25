using UnityEngine;
using System.Collections;

public class VirusAttack : AgentAttack {

	protected override AgentLife Attack ()
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
		if(enemyLife != null)
			if(enemyLife.currentLife > 0)
			{
				enemyLife.TakeDamage (attackDamage, true);
				if(enemyLife.currentLife <= 0){
					GetComponent<VirusMovement>().target = enemyLife.gameObject;

					AgentMovement enemyMovement = enemyLife.gameObject.GetComponent<AgentMovement>();
					if(enemyMovement != null){
						enemyMovement.agentRigidbody.velocity = Vector2.zero;
						enemyMovement.enabled = false;
					}
					AgentAttack enemyAttack = enemyLife.gameObject.GetComponent<AgentAttack>();
					if(enemyAttack != null){
						enemyAttack.enabled = false;
					}

					BoxCollider2D boxCollider = enemyLife.gameObject.GetComponent<BoxCollider2D>();
					if(boxCollider != null){
						boxCollider.enabled = false;
					}

					CircleCollider2D circleCollider = enemyLife.gameObject.GetComponent<CircleCollider2D>();
					if(circleCollider != null){
						circleCollider.enabled = false;
					}

					GetComponent<CircleCollider2D>().enabled = false;
					GetComponent<BoxCollider2D>().enabled = false;
					myLife.canvas.GetComponent<Canvas>().enabled = false;

					StartCoroutine(FadeBlack(enemyLife.gameObject.GetComponentInChildren<SpriteRenderer>()));

					agent.state = VirusAgent.CONTROL;
				}
			}
			
		return enemyLife;
	}

	IEnumerator FadeBlack(SpriteRenderer sprite){
		Color oldColor = sprite.color;
		
		Color c;
		float time = 1f;
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			if(sprite == null){
				yield break;
			}

			c = Color.Lerp(oldColor, new Color(0.2f,0.2f,0.2f), elapsedTime / time);
			sprite.color = c;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
