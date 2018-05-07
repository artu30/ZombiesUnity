using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie Assets/AI/Decisions/Find Tank")]
public class FindTankDecision : Decision
{
    public override bool Decide(ZombieStateController controller)
    {
        bool tankNear = TankNear(controller);
        return tankNear;
    }

    private bool TankNear(ZombieStateController controller)
    {
		GameObject minObjectDistance = null;
		float minDistance = 9999999f;
        // TODO: Ver si hay un tanque cerca, si es asi, en el estado nos ponemos detras
		// TODO: Ir al tanque mas cercano que tenga slots
		if (Vector3.Distance (controller.target.transform.position, controller.gameObject.transform.position) < 15.0f) {
			return false;
		}

        if (controller.tag != "tankZombie")
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("tankZombie");
            foreach (GameObject g in gameObjects)
            {
				float distanceToTank = Vector3.Distance (controller.gameObject.transform.position, g.transform.position);
				if ( distanceToTank < 10.0f && distanceToTank < minDistance )
                {
					minDistance = distanceToTank;
					minObjectDistance = g;
					if (minObjectDistance != null) {
						TankZombieSlot[] slots = minObjectDistance.GetComponentsInChildren<TankZombieSlot>();
						if (slots != null && slots.Length > 0) {
							foreach (TankZombieSlot tzs in slots) {
								if (!tzs.isOccupied || (tzs.isOccupied && tzs.occupedBy == controller.gameObject)) {
									controller.followingAt = g;
									return true;
								}
							}
						}
						minDistance = 99999f;
						minObjectDistance = null;
					}
                }
            }
        }
		controller.followingAt = null;
        return false;
    }
}
