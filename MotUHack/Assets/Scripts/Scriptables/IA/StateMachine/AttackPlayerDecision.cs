using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Decisions/Attack Player")]
public class AttackPlayerDecision : Decision {

	public override bool Decide(ZombieStateController controller)
	{
		return PlayerInAttackRange(controller);
	}

	private bool PlayerInAttackRange(ZombieStateController controller) {
		
		if (Vector3.Distance (controller.transform.position, controller.target.transform.position) < 2) {
			return true;
		}
		return false;
	}
}
