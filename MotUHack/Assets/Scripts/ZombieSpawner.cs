using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public ZombieWave zombieWaveSO;

    public GameObject zombiePrefab;
	public GameObject tankZombieSlotPrefab;
    public float timeBetweenWaves = 5.0f;
    private float waveCountdown;

    private SpawnState state = SpawnState.COUNTING;
    private Collider spawnCollider;

	public bool instantSpawn = false;

	// Use this for initialization
	void Start () {
        waveCountdown = timeBetweenWaves;
        spawnCollider = GetComponent<Collider>();
        //StartCoroutine(SpawnWave(zombieWaveSO));
    }
	
	// Update is called once per frame
	void Update () {
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(zombieWaveSO));
            }
     
        } else {
			waveCountdown -= Time.deltaTime;
        }
		if (instantSpawn) {
			StartCoroutine(SpawnWave(zombieWaveSO));
			instantSpawn = false;
		}
	}

	// TODO: Refactor this
    IEnumerator SpawnWave(ZombieWave _zombieWave)
    {
        state = SpawnState.SPAWNING;
        Bounds bounds = spawnCollider.bounds;
		Vector3 basePos = new Vector3(Random.Range(-bounds.extents.x + 5, bounds.extents.x - 5) + transform.position.x, 
			transform.position.y, Random.Range(-bounds.extents.z + 5, bounds.extents.z - 5) + transform.position.z);
        foreach (BaseZombie z in zombieWaveSO.zombieWave)
        {
			// TODO: Refactor this
			if (z.zombieType == ZombieTypes.TANK) {
				TankZombie tank = z as TankZombie;
				zombiePrefab.tag = tank.tag;
				basePos += new Vector3 (zombieWaveSO.distanceBetweenZombies, 0, zombieWaveSO.distanceBetweenZombies);
                GameObject zombieInstance = Instantiate(zombiePrefab, basePos, Quaternion.identity);
				tank.Init(zombieInstance.transform);
				foreach (Vector3 v in tank.generatedPositions) {
					GameObject tankZombieSlot = Instantiate (tankZombieSlotPrefab, zombieInstance.transform);
					tankZombieSlot.transform.position = v;
					tankZombieSlot.transform.parent = zombieInstance.transform;
				}
                //zombieInstance.GetComponent<MeshFilter>().mesh = z.mesh;
                ZombieStateController zScript = zombieInstance.GetComponent<ZombieStateController>();
                zScript.life = z.life;
                zScript.stamina = z.stamina;
                zScript.movementSpeed = z.movementSpeed;
				zScript.abilities = z.abilities;
				zScript.damage = z.damage;
				zScript.attackRange = z.attackRange;
                zScript.fastRun = z.fastRun;
				zScript.coolDownDuration = z.attackCadence;
				zScript.points = z.points;
				//zScript.distanceToAttackTarget = zombieWaveSO.distanceToAttackTarget;
            }
			if (z.zombieType == ZombieTypes.SLOW) {
				SlowZombie slow = z as SlowZombie;
				basePos += new Vector3 (zombieWaveSO.distanceBetweenZombies, 0, zombieWaveSO.distanceBetweenZombies);
				GameObject zombieInstance = Instantiate(z.prefab, basePos, Quaternion.identity);
				zombieInstance.tag = slow.tag;
                //zombieInstance.GetComponent<MeshFilter>().mesh = z.mesh;
                ZombieStateController zScript = zombieInstance.GetComponent<ZombieStateController>();
                zScript.life = z.life;
                zScript.stamina = z.stamina;
                zScript.movementSpeed = z.movementSpeed;
				zScript.abilities = z.abilities;
				zScript.damage = z.damage;
				zScript.attackRange = z.attackRange;
                zScript.fastRun = z.fastRun;
				zScript.coolDownDuration = z.attackCadence;
				zScript.points = z.points;
                //zScript.distanceToAttackTarget = zombieWaveSO.distanceToAttackTarget;
            }
        }
		waveCountdown = timeBetweenWaves;
        state = SpawnState.WAITING;
        yield break;
    }
}
