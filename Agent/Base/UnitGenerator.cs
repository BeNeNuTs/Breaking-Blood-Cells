using UnityEngine;
using System.Collections;

public class UnitGenerator : MonoBehaviour {

	public Type.TypeUnit typeUnitToGenerate;
	public float timeBetweenUnit = 10f;

	public GameObject LT_Cytotoxique;
	public GameObject LB;

	float timer;

	void Start(){
		typeUnitToGenerate = Type.TypeUnit.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		if(typeUnitToGenerate != Type.TypeUnit.NONE){
			timer += Time.deltaTime;

			if(timer > timeBetweenUnit){
				timer = 0f;
				Generate();
			}
		}
	}

	void Generate(){
		if(typeUnitToGenerate == Type.TypeUnit.LT_CYTOTOXIQUE){
			Instantiate(LT_Cytotoxique, transform.position, Quaternion.identity);
		}else if(typeUnitToGenerate == Type.TypeUnit.LB){
			Instantiate(LB, transform.position, Quaternion.identity);
		}
	}

	public void Generate(Type.TypeUnit type){
		if(type == Type.TypeUnit.LT_CYTOTOXIQUE){
			Instantiate(LT_Cytotoxique, transform.position, Quaternion.identity);
		}else if(type == Type.TypeUnit.LB){
			Instantiate(LB, transform.position, Quaternion.identity);
		}
	}
}
