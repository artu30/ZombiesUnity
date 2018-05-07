using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieWave : ScriptableObject {

    public float distanceBetweenZombies = 1.5f;
    public List<BaseZombie> zombieWave;
	public bool protectingTank = false;
	public float distanceToAttackTarget = 15.0f;
}
