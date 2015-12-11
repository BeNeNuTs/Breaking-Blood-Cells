using UnityEngine;
using System.Collections;

public class LevelAgentManager : MonoBehaviour {


	[Range (0,5)]
	public float timeScale = 1.0f;


	public AgentSpawn bacteriaSpawn;
	public AgentSpawn MacrophageSpawn;
	public AgentSpawn LTAuxSpawn;
	public AgentSpawn LTCytoSpawn;
	public AgentSpawn LBSpawn;
	public AgentSpawn VirusSpawn;

	public bool bacteria;
	public bool virus;

	public int maxMacrophage;
	public int maxBacteria;
	public int maxLT;
	public int maxVirus;
	public int maxLB;

	[Range (0,60)]
	public float spawnRateMacrophage;
	[Range (0,60)]
	public float spawnRateBacteria;
	[Range (0,60)]
	public float spawnRateLTAux;
	[Range (0,60)]
	public float spawnRateLTCyto;
	[Range (0,60)]
	public float spawnRateVirus;
	[Range (0,60)]
	public float spawnRateLB;

	public bool destroyBacteria;
	public bool destroyMacrophage;
	public bool destroyVirus;
	public bool destroyLTCyto;
	public bool destroyLTAux;
	public bool destroyLB;

	public bool takeResidu = true;

	public bool showStats = false;


	// Use this for initialization
	void Start () 
	{
		CameraControl.zoomEnabled = true;
		CameraControl.moveEnabled = true;

		if (bacteria && virus) 
		{
			Debug.Log("Impossible de sélectionner bactéries et virus en meme temps");
			virus = false;
		}

		if (bacteria) 
		{
			VirusSpawn.enabled = false;
			bacteriaSpawn.enabled = true;
		} 
		else 
		{
			VirusSpawn.enabled = true;
			bacteriaSpawn.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameManager.canTakeResidu = takeResidu;

		Time.timeScale = timeScale;

		bacteriaSpawn.spawnRate = spawnRateBacteria;
		MacrophageSpawn.spawnRate = spawnRateMacrophage;
		LTAuxSpawn.spawnRate = spawnRateLTAux;
		LTCytoSpawn.spawnRate = spawnRateLTCyto;
		LBSpawn.spawnRate = spawnRateLB;
		VirusSpawn.spawnRate = spawnRateVirus;

		if (LTAuxMovement.backToBase && virus)
			maxLT = -1;

		UnitManager.MAX_MACROPHAGES = maxMacrophage;
		UnitManager.MAX_BACTERIES = maxBacteria;
		UnitManager.MAX_LYMPHOCYTES_T = maxLT;
		UnitManager.MAX_VIRUS = maxVirus;
		UnitManager.MAX_LYMPHOCYTES_B = maxLB;


		if (showStats) 
		{
			showStats = false;
			UnitManager.ShowStats();
		}


		if (destroyBacteria) 
		{
			
			foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				if(enemy.name.Contains("Bacteria"))
				{
					AgentLife life = enemy.GetComponent<AgentLife>();
					if(life != null)
					{
						life.Kill();
					}
				}
			}
			destroyBacteria = false;
		}

		if (destroyVirus) 
		{
			
			foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				if(enemy.name.Contains("Virus"))
				{
					AgentLife life = enemy.GetComponent<AgentLife>();
					if(life != null)
					{
						life.Kill();
					}
				}
			}
			destroyVirus = false;
		}

		if (destroyMacrophage) 
		{
			
			foreach(GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
			{
				if(cell.name.Contains("Macrophage"))
				{
					AgentLife life = cell.GetComponent<AgentLife>();
					if(life != null)
					{
						life.Kill();
					}
				}
			}
			destroyMacrophage = false;
		}

		if (destroyLTAux) 
		{
			
			foreach(GameObject cell in GameObject.FindGameObjectsWithTag("LTAux"))
			{
		
					AgentLife life = cell.GetComponent<AgentLife>();
					if(life != null)
					{
						life.Kill();
					}

			}
			destroyLTAux = false;
		}

		if (destroyLTCyto) 
		{
			
			foreach(GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
			{
				if(cell.name.Contains("LTCyto"))
				{
					AgentLife life = cell.GetComponent<AgentLife>();
					if(life != null)
					{
						life.Kill();
					}
				}
			}
			destroyLTCyto = false;
		}

		if (destroyLB) 
		{
			
			foreach(GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
			{
				if(cell.name.Contains("LB"))
				{
					AgentLife life = cell.GetComponent<AgentLife>();
					if(life != null)
					{
						life.Kill();
					}
				}
			}
			destroyLB = false;
		}


	}
}
