using UnityEngine;
using System.Collections;

public class OndeController : MonoBehaviour {

	public float speed = 5f;
	public int amountDamage = 10;

	float scale = 0f;
	Color color;
	SpriteRenderer sprite;
	const float maxScale = 1f;

	void Start(){
		sprite = GetComponent<SpriteRenderer>();
		color = sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(scale > maxScale){
			Destroy(gameObject, 5f);
		}else{
			transform.localScale = new Vector3(scale, scale, scale);
			sprite.color = color;

			scale += speed * Time.deltaTime;
			color = new Color(color.r, color.g, color.b, color.a - speed * Time.deltaTime);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Cell") && other.GetType() == typeof(BoxCollider2D) && color.a > 0.05f){
			AgentLife agentLife = other.GetComponent<AgentLife>();
			agentLife.TakeDamage(amountDamage);
			StopCoroutine(agentLife.IsHit(new Color(color.r, color.g, color.b)));
			StartCoroutine(agentLife.IsHit(new Color(color.r, color.g, color.b)));
		}
	}
}
