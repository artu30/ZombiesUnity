using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : Ability {

	public string name = "Shield";
	public float resistance = 20.0f;

	public override void TriggerAbility (Transform transform)
	{
		// Do things
		Debug.Log ("Escudo activado");
	}
}

