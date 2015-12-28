using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<Transform> agentSpawnPosition;

	GameManager gameManager;

	void Start()
	{
		//Insolite n'est-il pas ?
		gameManager = GameManager.gameManager.GetComponent<GameManager> ();
	}


	// Update is called once per frame
	void Update () 
	{

		Vector3 positionSpawn = agentSpawnPosition [Random.Range (0, agentSpawnPosition.Count - 1)].position;

		spawnTimer += Time.deltaTime;
		if (spawnTimer >= spawnRate) 
		{
			spawnTimer = 0;

			switch(typeAgent)
			{
			case TypeSpawn.Bacteria:
				if(UnitManager.NB_BACTERIES < UnitManager.MAX_BACTERIES)
				{

					Quaternion q = Quaternion.AngleAxis (180, Vector3.forward);
					Instantiate(gameManager.bacteria, positionSpawn,q);

					UnitManager.NB_BACTERIES++;
				}
				break;
			case TypeSpawn.Macrophage:
				if(UnitManager.NB_MACROPHAGES < UnitManager.MAX_MACROPHAGES)
				{
					Instantiate(gameManager.macrophage, positionSpawn,Quaternion.identity);
					UnitManager.NB_MACROPHAGES++;
				}
				break;
			case TypeSpawn.Virus:
				if(UnitManager.NB_VIRUS < UnitManager.MAX_VIRUS)
				{
					
					Quaternion q = Quaternion.AngleAxis (180, Vector3.forward);
					Instantiate(gameManager.virus, positionSpawn,q);
					UnitManager.NB_VIRUS++;
				}
				break;
			case TypeSpawn.LTAux:
				if(UnitManager.NB_LYMPHOCYTES_T < UnitManager.MAX_LYMPHOCYTES_T)
				{
					Instantiate(gameManager.LTAux, positionSpawn,Quaternion.identity);
					UnitManager.NB_LYMPHOCYTES_T++;
				}
				break;
			case TypeSpawn.LTCyto:
				if(UnitManager.NB_LYMPHOCYTES_T < UnitManager.MAX_LYMPHOCYTES_T)
				{
					Instantiate(gameManager.LTCyto, positionSpawn,Quaternion.identity);
					UnitManager.NB_LYMPHOCYTES_T++;
				}
				break;
			case TypeSpawn.LB:
				if(UnitManager.NB_LYMPHOCYTES_B < UnitManager.MAX_LYMPHOCYTES_B)
				{
					Instantiate(gameManager.LB, positionSpawn,Quaternion.identity);
					UnitManager.NB_LYMPHOCYTES_B++;
				}
				break;
			}
		}
	
	}
}
