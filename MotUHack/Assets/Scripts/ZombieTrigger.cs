using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrigger : MonoBehaviour {

	private ZombieSpawner zombieSpawnerScript;

	public GameObject zombieSpawner;

	void Start() {
		zombieSpawnerScript = zombieSpawner.GetComponent<ZombieSpawner> ();
		if (zombieSpawnerScript == null)
			Debug.Log ("No zombie spawner associated");
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PlayerCollider") {
			if (zombieSpawnerScript != null) {
				zombieSpawnerScript.instantSpawn = true;
				Destroy (gameObject);
			}
		}
	}

}
