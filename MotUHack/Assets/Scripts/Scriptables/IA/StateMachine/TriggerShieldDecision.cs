using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Decisions/Trigger Shield")]
public class TriggerShieldDecision : Decision {	

	public override bool Decide(ZombieStateController controller)
	{
		bool change = TriggerShieldDecide (controller);
		return change;
	}

	private bool TriggerShieldDecide(ZombieStateController controller) {
		if (controller.abilities != null && controller.abilities.Length > 0) {
			foreach (Ability a in controller.abilities) {
				if (a.type == AbilityType.SHIELD) {
					Debug.Log ("On cooldown: " + controller.abilityCooldown.onCooldown);
					if (controller.abilityCooldown.ability == a && controller.abilityCooldown.onCooldown) {
						return false;
					}
					if (Vector3.Distance (controller.target.transform.position, controller.gameObject.transform.position) < 30.0f) {
						controller.abilityCooldown.ability = a;
						return true;
					}
				}
			}
		}
		return false;
	}
}
