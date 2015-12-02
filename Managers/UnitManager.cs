using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {

	public static int NB_MACROPHAGES, NB_CELLS, NB_LYMPHOCYTES_T, NB_LYMPHOCYTES_B, NB_BACTERIES, NB_VIRUS;


	public static int MAX_BACTERIES = 50, MAX_VIRUS = 50, MAX_MACROPHAGES = 50, MAX_LYMPHOCYTES_T = 50, MAX_LYMPHOCYTES_B = 50;

	void Awake(){

		NB_MACROPHAGES = NB_CELLS = NB_LYMPHOCYTES_T = NB_LYMPHOCYTES_B = NB_BACTERIES = NB_VIRUS = 0;

		GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject cell in cells){
			if(cell.name.Contains("Macrophage")){
				NB_MACROPHAGES++;
			}else if(cell.name.Contains("Cell")){
				NB_CELLS++;
			}else if(cell.name.Contains("LT")){
				NB_LYMPHOCYTES_T++;
			}else if(cell.name.Contains("LB")){
				NB_LYMPHOCYTES_B++;
			}
		}

		foreach(GameObject enemy in enemies){
			if(enemy.name.Contains("Bacteria")){
				NB_BACTERIES++;
			}else if(enemy.name.Contains("Virus")){
				NB_VIRUS++;
			}
		}

		ShowStats();
	}

	public static void ShowStats(){
		Debug.LogWarning("NB_MACRO : " + NB_MACROPHAGES + " - NB_CELL : " + NB_CELLS + " - NB_LT : " + NB_LYMPHOCYTES_T + " - NB_LB : " + NB_LYMPHOCYTES_B + " - NB_BACT : " + NB_BACTERIES + " - NB_VIRUS : " + NB_VIRUS);
	}

	public static void DeathCell(string type){
		if(type.Contains("Macrophage")){
			NB_MACROPHAGES--;
			GameManager.gameManager.GetComponent<ObjectifManager>().updateGoal(7);
		}else if(type.Contains("Cell")){
			NB_CELLS--;
		}else if(type.Contains("LT")){
			NB_LYMPHOCYTES_T--;
		}else if(type.Contains("LB")){
			NB_LYMPHOCYTES_B--;
		}else if(type.Contains("Bacteria")){
			NB_BACTERIES--;
			GameManager.gameManager.GetComponent<ObjectifManager>().updateGoal(0);
		}else if(type.Contains("Virus")){
			NB_VIRUS--;
		}
	}
}
