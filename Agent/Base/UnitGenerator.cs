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
		if(typeUnitToGenerate == Type.TypeUnit.LT_CYTOTOXIQUE && UnitManager.NB_LYMPHOCYTES_T < UnitManager.MAX_LYMPHOCYTES_T){
			Instantiate(LT_Cytotoxique, transform.position, Quaternion.identity);
			UnitManager.NB_LYMPHOCYTES_T++;
		}else if(typeUnitToGenerate == Type.TypeUnit.LB && UnitManager.NB_LYMPHOCYTES_B < UnitManager.MAX_LYMPHOCYTES_B){
			Instantiate(LB, transform.position, Quaternion.identity);
			UnitManager.NB_LYMPHOCYTES_B++;
		}
	}

	public void Generate(Type.TypeUnit type){
		if(type == Type.TypeUnit.LT_CYTOTOXIQUE && UnitManager.NB_LYMPHOCYTES_T < UnitManager.MAX_LYMPHOCYTES_T){
			Instantiate(LT_Cytotoxique, transform.position, Quaternion.identity);
			UnitManager.NB_LYMPHOCYTES_T++;
		}else if(type == Type.TypeUnit.LB && UnitManager.NB_LYMPHOCYTES_B < UnitManager.MAX_LYMPHOCYTES_B){
			Instantiate(LB, transform.position, Quaternion.identity);
			UnitManager.NB_LYMPHOCYTES_B++;
		}
	}
}
