using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// La classe AgentLife permet de gérer la vie de l'agent.
/// </summary>
public class AgentLife : MonoBehaviour {

	public float startingLife;
	public float currentLife;

	public GameObject canvasPrefab;
	public float offsetY;

	protected Agent agent;
	protected bool isDead;



	[HideInInspector]
	public GameObject canvas;
	[HideInInspector]
	public Image cellLife;

	public bool isInfected = false;

	public GameObject DeathExplosion;
	public float scaleExplosion = 1f;


	// Use this for initialization
	void Awake () {
		currentLife = startingLife;
		canvas = Instantiate(canvasPrefab, posCanvas, Quaternion.identity) as GameObject;
		cellLife = canvas.GetComponentInChildren<Image>();
		agent = GetComponent<Agent>();
		isDead = false;
	}
	
	/// <summary>
	/// Met à jour la position du canvas affichant la vie de l'agent.
	/// </summary>
	protected virtual void Update () {

		UpdatePositionCanvas();
	}

	public void AddLife(int amount){
		if(currentLife == startingLife){
			return;
		}
		
		if(currentLife + amount < startingLife){
			currentLife += amount;
		}else{
			currentLife = startingLife;
		}

		UpdateLifeImage();
	}

	/// <summary>
	/// Inflige des dégats à l'agent.
	/// </summary>
	/// <param name="amount">Quantité de dégats reçus.</param>
	/// <param name="virus">Si <c>true</c> alors c'est un virus qui inflige des dégats.</param>
	public virtual void TakeDamage (int amount, bool virus = false)
	{
		if(isDead)
			return;
		
		currentLife -= amount;

		if(currentLife <= 0)
		{
			if(!virus){
				Death ();
			}else{
				isDead = true;
			}
			return;
		}
		
		UpdateLifeImage();
	}

	/// <summary>
	/// Met à jour la barre de vie de l'agent.
	/// </summary>
	public void UpdateLifeImage(){
		if(cellLife.enabled == false){
			cellLife.enabled = true;
		}

		float percentageLife = (float)currentLife / (float)startingLife;
		cellLife.fillAmount = percentageLife;
		cellLife.color = Color.Lerp(Color.red, Color.green, percentageLife);
	}

	/// <summary>
	/// Met à jour la position du canvas.
	/// </summary>
	void UpdatePositionCanvas(){
		canvas.transform.position = posCanvas;
	}

	/// <summary>
	/// Permet de faire mourir l'agent lorsqu'il n'a plus de vie.
	/// </summary>
	protected virtual void Death ()
	{
		UnitManager.DeathCell(name);

		GameObject explosion = Instantiate (DeathExplosion, transform.position, Quaternion.identity) as GameObject;
		explosion.transform.localScale = explosion.transform.localScale * scaleExplosion;

		isDead = true;

		Destroy(canvas);
		Destroy(gameObject);
	}

	/// <summary>
	/// Permet de détruire l'agent.
	/// </summary>
	public void Kill ()
	{
		Death();

	}

	/// <summary>
	/// Retourne la position du canvas.
	/// </summary>
	/// <value>La position du canvas.</value>
	Vector3 posCanvas { 
		get{
			return new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
		}
	}

	/// <summary>
	/// Affiche un effet lorsque l'agent subit des dégats.
	/// </summary>
	/// <param name="newColor">La couleur de l'effet.</param>
	public IEnumerator IsHit(Color newColor) {

		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
		sprite.color = newColor;

		Color c;
		float time = 1f;
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			if(sprite != null)
			{
				c = Color.Lerp(newColor, Color.white, elapsedTime / time);
				sprite.color = c;
				elapsedTime += Time.deltaTime;
			}
			yield return null;
		}
	}
}
