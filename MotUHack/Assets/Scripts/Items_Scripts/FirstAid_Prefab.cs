using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid_Prefab : Item_Prefab {

	public int HealthToRestore = 1;

	public override void getItem(CharacterCollisions player){
		base.getItem(player);
		
        if ((player.playerObject.life + HealthToRestore) < player.playerObject.MAX_LIFE) {
            Debug.Log("Health += " + HealthToRestore);
            player.playerObject.life += HealthToRestore;
        }
        else {
            player.playerObject.life = player.playerObject.MAX_LIFE;
        }
		
        destroyItem();
	}
}
