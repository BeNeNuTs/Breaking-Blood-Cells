using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgentLife : MonoBehaviour {

	public float startingLife;
	public float currentLife;

	public GameObject canvasPrefab;
	public float offsetY;

	protected Agent agent;

	GameObject canvas;
	Image cellLife;
	bool isDead;

	// Use this for initialization
	void Awake () {
		currentLife = startingLife;
		canvas = Instantiate(canvasPrefab, posCanvas, Quaternion.identity) as GameObject;
		cellLife = canvas.GetComponentInChildren<Image>();
		agent = GetComponent<Agent>();
		isDead = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		// Pour pouvoir tester si les méthodes fonctionnent
		if(Input.GetKeyDown(KeyCode.A)){
			if(agent.state == MacrophageAgent.BRING_RESIDUS && name.Contains("Macrophage")){
				TakeDamage(10);
			}
		}else if(Input.GetKeyDown(KeyCode.L)){
			AddLife(10);
		}

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

	/** Inflige des dégats à l'agent */
	public void TakeDamage (int amount)
	{
		if(isDead)
			return;
		
		currentLife -= amount;
		
		if(currentLife <= 0)
		{
			Death ();
			return;
		}
		
		UpdateLifeImage();
	}

	/** Met à jour la barre de vie de l'agent */
	public void UpdateLifeImage(){
		if(cellLife.enabled == false){
			cellLife.enabled = true;
		}

		float percentageLife = (float)currentLife / (float)startingLife;
		cellLife.fillAmount = percentageLife;
		cellLife.color = Color.Lerp(Color.red, Color.green, percentageLife);
	}

	void UpdatePositionCanvas(){
		canvas.transform.position = posCanvas;
	}

	protected virtual void Death ()
	{
		UnitManager.DeathCell(name);

		isDead = true;

		Destroy(canvas);
		Destroy(this.gameObject);

	}

	Vector3 posCanvas { 
		get{
			return new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
		}
	}

	/** Fonction à faire pour afficher un effet, un son lorsque l'agent subit des dégats */
	public IEnumerator IsHit(Color newColor) {

		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
		sprite.color = newColor;

		Color c;
		float time = 1f;
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			c = Color.Lerp(newColor, Color.white, elapsedTime / time);
			sprite.color = c;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
