using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgentLife : MonoBehaviour {

	public float startingLife;
	public float currentLife;

	public GameObject canvasPrefab;
	public float offsetZ;

	private GameObject canvas;
	private Image cellLife;
	bool isDead;

	// Use this for initialization
	void Awake () {
		currentLife = startingLife;
		canvas = Instantiate(canvasPrefab, posCanvas, Quaternion.Euler(new Vector3(-90,0,0))) as GameObject;
		cellLife = canvas.GetComponentInChildren<Image>();
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {

		// Pour pouvoir tester si les méthodes fonctionnent
		if(Input.GetKeyDown(KeyCode.A)){
			TakeDamage(10);
		}else if(Input.GetKeyDown(KeyCode.D)){
			Duplicate();
		}else if(Input.GetKeyDown(KeyCode.L)){
			if(currentLife + 10 < startingLife)
				currentLife += 10;
			else
				currentLife = startingLife;

			UpdateLifeImage();
		}

		UpdatePositionCanvas();
	}

	/** Permet de dupliquer l'agent s'il possède toute sa vie et en divant sa vie par 2 */
	void Duplicate(){
		if(currentLife == startingLife){
			GameObject cellInstance = Instantiate(this.gameObject, transform.position, Quaternion.identity) as GameObject;
			AgentLife cellInstanceLife = cellInstance.GetComponent<AgentLife>();
			cellInstanceLife.currentLife = startingLife / 2f;
			cellInstanceLife.UpdateLifeImage();

			currentLife = startingLife / 2f;
			UpdateLifeImage();
		}
	}

	/** Inflige des dégats à l'agent */
	public void TakeDamage (int amount)
	{
		if(isDead)
			return;
		
		StopCoroutine("IsHit");
		StartCoroutine("IsHit");
		
		currentLife -= amount;
		
		ShowDamage(amount);
		
		if(currentLife <= 0)
		{
			Death ();
			return;
		}
		
		UpdateLifeImage();
	}

	/** Met à jour la barre de vie de l'agent */
	void UpdateLifeImage(){
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

	void Death ()
	{
		isDead = true;
		Destroy(canvas);
		Destroy(this.gameObject);
	}

	Vector3 posCanvas { 
		get{
			return new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetZ);
		}
	}

	/** Fonction à faire pour afficher un effet, un son lorsque l'agent subit des dégats */
	void ShowDamage(int amount){
		/*GameObject hitInstance = Instantiate(hitDamage, Vector3.zero, Quaternion.identity) as GameObject;
		hitInstance.transform.SetParent(canvas);
		hitInstance.transform.localPosition = Vector3.zero;
		hitInstance.transform.localEulerAngles = Vector3.zero;

		hitInstance.transform.localScale = Vector3.one;
		
		hitInstance.GetComponent<Text>().text = "-"+amount;
		
		Destroy(hitInstance, 1f);*/
	}

	/** Fonction à faire pour afficher un effet, un son lorsque l'agent subit des dégats */
	IEnumerator IsHit() {
		/*Color newColor = new Color(10,0,0,0);
		myRenderer.materials[0].SetColor("_RimColor", newColor);
		
		float time = 1f;
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			newColor = Color.Lerp(newColor, rimColor, elapsedTime / time);
			myRenderer.materials[0].SetColor("_RimColor",  newColor);
			elapsedTime += Time.deltaTime;
			yield return null;
		}*/
		yield return null;
	}
}
