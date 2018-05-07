using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ZombieTypes
{
    SLOW, TANK
}

/**
 * Representation of a base zombie as ScriptableObject.
 * In fact, designer can modify those values without broke anything
 * @author m.fonseca
 * @since 12/12/2017
 */

public abstract class BaseZombie : ScriptableObject {

    public ZombieTypes zombieType;
    public float life = 10.0f;
    public float movementSpeed = 1.0f;
    public float stamina = 5.0f;
	public float damage = 2.0f;
	public float attackRange = 1.0f;
	public float attackCadence = 1.2f;
	public float points = 50.0f;
    public bool fastRun = false;
    public GameObject prefab;

	public Ability[] abilities;

    public abstract void Init(Transform currentPosition);

}
