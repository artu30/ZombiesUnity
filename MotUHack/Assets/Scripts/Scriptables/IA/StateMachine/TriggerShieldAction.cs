using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Actions/Trigger Shield Action")]
public class TriggerShieldAction : Action {

	public override void Act(ZombieStateController state)
	{
		TriggerShield (state);
	}

	private void TriggerShield(ZombieStateController state) {
		state.LaunchAbility ();
	}

}
