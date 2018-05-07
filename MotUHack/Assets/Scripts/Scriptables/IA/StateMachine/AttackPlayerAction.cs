using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Actions/Attack Player Action")]
public class AttackPlayerAction : Action {

	public override void Act(ZombieStateController stateController)
	{
		AttackPlayer(stateController);
	}

	private void AttackPlayer(ZombieStateController stateController)
	{

		if (!stateController.onCooldown && !stateController.isDead) {
			//Debug.Log("Doing damage... " + stateController.damage);
			stateController.animator.SetTrigger ("Attack");
			stateController.onCooldown = true;
			stateController.nextReadyTime = stateController.coolDownDuration + Time.time;
			stateController.coolDownTimeLeft = stateController.coolDownDuration;
			stateController.agent.enabled = false;
			stateController.target.GetComponent<Character>().ReceiveDamage((int) stateController.damage);
		}

	}

}
