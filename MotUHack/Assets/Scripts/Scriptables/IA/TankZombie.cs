using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A powerfull zombie who acts as tank of other Zombies.
 * @author m.fonseca
 * @since 14/12/2017
 */
public class TankZombie : BaseZombie {
    // TODO: Abilities
    public string tag = "tankZombie";
    public int maxProtectionPositions = 10;
    public float distanceBetweenPositions = 1f;

	[HideInInspector]
    public List<Vector3> generatedPositions;

    public override void Init(Transform currentPosition)
    {
		generatedPositions.Clear ();
        GenerateProtectedPositions(currentPosition);
    }

    // Algoritmo de posiciones generadas.
    // 1. Partiendo de la posicion actual, se genera tanto a izquierda como a derecha un transform
    // 2. Se va hacia atras N espacio y se genera en diagonal 4 posiciones
	// TODO: Fix this code
    private void GenerateProtectedPositions(Transform currentPosition)
    {
		float zValue = 2;
		generatedPositions.Add(new Vector3 (currentPosition.position.x + distanceBetweenPositions, currentPosition.position.y, currentPosition.position.z));
		generatedPositions.Add(new Vector3 (currentPosition.position.x + -1 * distanceBetweenPositions, currentPosition.position.y, currentPosition.position.z));

		float eachPos = distanceBetweenPositions / 2;

		generatedPositions.Add(new Vector3 (currentPosition.position.x + eachPos * 2, currentPosition.position.y, currentPosition.position.z - zValue));
		generatedPositions.Add(new Vector3 (currentPosition.position.x + eachPos, currentPosition.position.y, currentPosition.position.z - zValue));
		generatedPositions.Add(new Vector3 (currentPosition.position.x - eachPos, currentPosition.position.y, currentPosition.position.z - zValue));
		generatedPositions.Add(new Vector3 (currentPosition.position.x - eachPos * 2, currentPosition.position.y, currentPosition.position.z - zValue));
		zValue *= 2;

		generatedPositions.Add(new Vector3 (currentPosition.position.x + eachPos * 2, currentPosition.position.y, currentPosition.position.z - zValue));
		generatedPositions.Add(new Vector3 (currentPosition.position.x + eachPos, currentPosition.position.y, currentPosition.position.z - zValue));
		generatedPositions.Add(new Vector3 (currentPosition.position.x - eachPos, currentPosition.position.y, currentPosition.position.z - zValue));
		generatedPositions.Add(new Vector3 (currentPosition.position.x - eachPos * 2, currentPosition.position.y, currentPosition.position.z - zValue));
    }
}
