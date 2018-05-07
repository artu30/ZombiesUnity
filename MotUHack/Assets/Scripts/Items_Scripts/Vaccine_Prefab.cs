using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine_Prefab : Item_Prefab {

	public int VaccinesToAdd = 1;

	public override void getItem(CharacterCollisions player){
		base.getItem(player);

        player.playerObject.vaccines += VaccinesToAdd;
        Debug.Log("Vaccines +1");

        destroyItem();
	}
}
