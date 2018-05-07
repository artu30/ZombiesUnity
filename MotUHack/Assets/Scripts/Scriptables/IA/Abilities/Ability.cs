using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AbilityType {
	SHIELD, DAMAGE 
}

public abstract class Ability : ScriptableObject {

	public float cooldown;
	public AbilityType type;

	public abstract void TriggerAbility (Transform transform);

}
