using UnityEngine;
using System.Collections;

/// <summary>
/// La classe VirusAgent hérite de la classe Agent 
/// pour ajouter les états CONTROL et DUPLICATE aux virus.
/// </summary>
public class VirusAgent : Agent {

	public static int CONTROL = 3;
	public static int DUPLICATE = 4;
}
