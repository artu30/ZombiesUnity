using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Actions/Follow Tank Action")]
public class FollowTankAction : Action
{
    public override void Act(ZombieStateController state)
    {
		FollowTank(state);
    }

	private void FollowTank(ZombieStateController stateController)
	{
		GameObject g = stateController.followingAt;
		stateController.animator.SetBool("IsWalking", true);
		if (g != null) {
			TankZombieSlot[] slots =  g.GetComponentsInChildren<TankZombieSlot>();
			if (slots != null) {
				for (int i = 0; i < slots.Length; i++) {
					if (slots [i].occupedBy == stateController.gameObject) {
						stateController.agent.destination = slots [i].transform.position;
						stateController.agent.autoRepath = true;
						break; 
					}
					if (!slots [i].isOccupied && slots [i].occupedBy != stateController.gameObject) {
						stateController.agent.destination = slots [i].transform.position;
						slots [i].isOccupied = true;
						slots [i].occupedBy = stateController.gameObject;
						break;
					}
				}
			}
				//stateController.agent.speed = zs.movementSpeed;
		}
	}


}
