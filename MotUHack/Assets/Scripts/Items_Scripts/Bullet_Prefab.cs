using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Prefab : MonoBehaviour {

    float damage = 0f;

	void OnTriggerEnter(Collider coll){
        if (coll.gameObject.tag.Contains("Zombie")) {
            ZombieStateController zs = coll.gameObject.GetComponent<ZombieStateController>();
            if (zs) {
                zs.ReceiveDamage(damage);
                Destroy(gameObject);
            }
        } else if (coll.gameObject.GetComponent<IWeapon>() == null && coll.gameObject.GetComponent<Bullet_Prefab>() == null) {
            //Debug.Log(coll.gameObject.name);
			Destroy (gameObject);
		}
	}

    public void setDamage(float d) {
        damage = d;
    }
}
