using UnityEngine;
using System.Collections;

/// <summary>
/// La classe LTCytoMovement hérite de la classe AgentMovement
/// et permet de redéfinir les méthodes de mouvements des lymphocytes T Cytotoxique.
/// </summary>
public class LTCytoMovement : AgentMovement {

	/// <summary>
	/// Vérifie ce qui entre dans le percepts de l'agent.
	/// </summary>
	/// <param name="other">Other.</param>
	protected override void OnTriggerEnter2D(Collider2D other){
		if(other.name.Contains("Bacteria")){
			base.OnTriggerEnter2D(other);
		}
	}
}
