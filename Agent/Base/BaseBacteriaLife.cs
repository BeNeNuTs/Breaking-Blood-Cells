using UnityEngine;
using System.Collections;

/// <summary>
/// La classe BaseBacteriaLife hérite de la classe AgentLife
/// et permet de gérer la vie des bases des bactéries.
/// </summary>
public class BaseBacteriaLife : AgentLife 
{
	public GameObject centralExplosion;
	public float scaleCentralExplosion;
	public GameObject damaged;

	/// <summary>
	/// Inflige des dégats à l'agent.
	/// </summary>
	/// <param name="amount">Quantité de dégats reçus.</param>
	/// <param name="virus">Si <c>true</c> alors c'est un virus qui inflige des dégats.</param>
	public override void TakeDamage (int amount, bool virus = false)
	{
		StartCoroutine(DoBlinks(damaged.GetComponent<SpriteRenderer>(), 1, 0.1f, false));
		base.TakeDamage (amount, virus);

	}

	/// <summary>
	/// Fais cligner la base des bactéries.
	/// </summary>
	/// <param name="render">Sprite de rendu.</param>
	/// <param name="numBlinks">Nombres de clignements.</param>
	/// <param name="blinkTime">Temps de clignement.</param>
	/// <param name="finalRenderer">Si <c>true</c> alors afficher le sprite final.</param>
	IEnumerator DoBlinks(SpriteRenderer render, int numBlinks, float blinkTime, bool finalRenderer = true)
	{
		for(int i = 0 ; i < numBlinks ; i++)
		{
			render.enabled = !render.enabled;
			yield return new WaitForSeconds(blinkTime);
		}
		
		render.enabled = finalRenderer;
	}

	/// <summary>
	/// Permet de faire mourir l'agent lorsqu'il n'a plus de vie.
	/// </summary>
	protected override void Death()
	{
		GameObject explosion = Instantiate (centralExplosion, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.localScale = explosion.transform.localScale * scaleCentralExplosion;
		GameManager.gameManager.GetComponent<ObjectifManager> ().updateGoal (13);
		base.Death ();
	}
}
