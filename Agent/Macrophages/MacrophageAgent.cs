using UnityEngine;
using System.Collections;

/// <summary>
/// La classe MacrophageAgent hérite de la classe Agent 
/// pour ajouter les états BRING_RESIDUS et GOTOANTIBODY aux macrophages.
/// </summary>
public class MacrophageAgent : Agent {

	public static int BRING_RESIDUS = 3;
	public static int GOTOANTIBODY = 4;
}
