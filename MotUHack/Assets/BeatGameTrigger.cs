using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGameTrigger : MonoBehaviour {

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

	public void GameBeated() {
		gm.GameBeated ();
	}
}
