using UnityEngine;
using System.Collections;

/// <summary>
/// La classe Agent définie les différents états que peuvent avoir les agents.
/// </summary>
public class Agent : MonoBehaviour {
	
	public static int WIGGLE = 0, GOTOENEMY = 1, ATTACK = 2;
	public int state;
}
