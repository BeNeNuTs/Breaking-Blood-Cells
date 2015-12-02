using UnityEngine;
using System.Collections;

public class AgentSpawn : MonoBehaviour {

	public enum TypeSpawn
	{
		Macrophage,
		Bacteria,
		Virus,
		LTAux,
		LTCyto,
		LB
	}

	public TypeSpawn typeAgent;
	public float spawnRate = 0f; 
	float spawnTimer = 0f;


	GameManager gameManager;

	void Start()
	{
		//Insolite n'est-il pas ?
		gameManager = GameManager.gameManager.GetComponent<GameManager> ();
	}


	// Update is called once per frame
	void Update () 
	{


		spawnTimer += Time.deltaTime;
		if (spawnTimer >= spawnRate) 
		{
			spawnTimer = 0;

			switch(typeAgent)
			{
			case TypeSpawn.Bacteria:
				if(UnitManager.NB_BACTERIES < UnitManager.MAX_BACTERIES)
				{
					Instantiate(gameManager.bacteria, transform.position,Quaternion.identity);
					UnitManager.NB_BACTERIES++;
				}
				break;
			case TypeSpawn.Macrophage:
				if(UnitManager.NB_MACROPHAGES < UnitManager.MAX_MACROPHAGES)
				{
					Instantiate(gameManager.macrophage, transform.position,Quaternion.identity);
					UnitManager.NB_MACROPHAGES++;
				}
				break;
			case TypeSpawn.Virus:
				if(UnitManager.NB_VIRUS < UnitManager.MAX_VIRUS)
				{
					Instantiate(gameManager.virus, transform.position,Quaternion.identity);
					UnitManager.NB_VIRUS++;
				}
				break;
			case TypeSpawn.LTAux:
				if(UnitManager.NB_LYMPHOCYTES_T < UnitManager.MAX_LYMPHOCYTES_T)
				{
					Instantiate(gameManager.LTAux, transform.position,Quaternion.identity);
					UnitManager.NB_LYMPHOCYTES_T++;
				}
				break;
			case TypeSpawn.LTCyto:
				if(UnitManager.NB_LYMPHOCYTES_T < UnitManager.MAX_LYMPHOCYTES_T)
				{
					Instantiate(gameManager.LTCyto, transform.position,Quaternion.identity);
					UnitManager.NB_LYMPHOCYTES_T++;
				}
				break;
			case TypeSpawn.LB:
				if(UnitManager.NB_LYMPHOCYTES_B < UnitManager.MAX_LYMPHOCYTES_B)
				{
					Instantiate(gameManager.LB, transform.position,Quaternion.identity);
					UnitManager.NB_LYMPHOCYTES_B++;
				}
				break;
			}
		}
	
	}
}
