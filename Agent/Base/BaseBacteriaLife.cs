using UnityEngine;
using System.Collections;

public class BaseBacteriaLife : AgentLife 
{
	public GameObject centralExplosion;
	public float scaleCentralExplosion;
	public GameObject damaged;

	public override void TakeDamage (int amount, bool virus = false)
	{
		StartCoroutine(DoBlinks(damaged.GetComponent<SpriteRenderer>(), 1, 0.1f, false));
		base.TakeDamage (amount, virus);

	}

	IEnumerator DoBlinks(SpriteRenderer render, int numBlinks, float blinkTime, bool finalRenderer = true)
	{
		for(int i = 0 ; i < numBlinks ; i++)
		{
			render.enabled = !render.enabled;
			yield return new WaitForSeconds(blinkTime);
		}
		
		render.enabled = finalRenderer;
	}

	protected override void Death()
	{
		GameObject explosion = Instantiate (centralExplosion, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.localScale = explosion.transform.localScale * scaleCentralExplosion;
		GameManager.gameManager.GetComponent<ObjectifManager> ().updateGoal (13);
		base.Death ();
	}
}
