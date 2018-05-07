using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Actions/Follow Player Action")]
public class FollowPlayerAction : Action
{
    public override void Act(ZombieStateController stateController)
    {
        FollowPlayer(stateController);
    }

    private void FollowPlayer(ZombieStateController stateController)
    {
		//stateController.agent.speed = stateController.movementSpeed;
		if (stateController.isDead) {
			stateController.agent.enabled = false;
			return;
		}
        if (stateController.fastRun) { stateController.animator.SetBool("IsRunning", true); }
        else stateController.animator.SetBool("IsWalking", true);

		stateController.agent.enabled = true;
        stateController.agent.destination = stateController.target.transform.position;
    }
}
