using UnityEngine;
using System.Collections;

public class Victory : Cutscene {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (destroyAllEnemies ());
	}

	public IEnumerator destroyAllEnemies()
	{

		/*foreach(GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
		{
			AgentMovement movement = cell.GetComponent<AgentMovement>();
			if(movement != null)
			{
				movement.enabled = false;
			}

			AgentAttack attack = cell.GetComponent<AgentAttack>();
			if(attack != null)
			{
				attack.enabled = false;
			}

		}

		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			AgentMovement movement = enemy.GetComponent<AgentMovement>();
			if(movement != null)
			{
				movement.enabled = false;
			}
			
			AgentAttack attack = enemy.GetComponent<AgentAttack>();
			if(attack != null)
			{
				attack.enabled = false;
			}

		}*/


		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if(enemy != null)
			{
				AgentLife life = enemy.GetComponent<AgentLife>();
				if(life != null)
				{
					life.Kill();
				}
				yield return new WaitForSeconds(0.05f);
			}
		}

		GameManager.gameManager.GetComponent<ObjectifManager>().updateCutsceneGoal();
	}
	
}
