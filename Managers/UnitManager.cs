using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {

	public static int NB_MACROPHAGES, NB_LYMPHOCYTES_T, NB_LYMPHOCYTES_B, NB_ANTICORPS, NB_BACTERIES, NB_VIRUS;

	public static int MAX_BACTERIES = 50, MAX_VIRUS = 50;

	void Awake(){
		NB_MACROPHAGES = NB_LYMPHOCYTES_T = NB_LYMPHOCYTES_B = NB_ANTICORPS = NB_BACTERIES = NB_VIRUS = 0;
	}
}
