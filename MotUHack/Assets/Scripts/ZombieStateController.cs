using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/**
 * Contains behaviour tree's actual state of a zombie 
 * @author m.fonseca
 * @since 14/12/2017
 * TODO: Make this generic (ZombieStateController inherits StateController)
 */
public class ZombieStateController : MonoBehaviour {

    public LayerMask blockingLayer;       
    private Collider coll;             
    private Rigidbody rb;
	private GameManager gameManager;

    [HideInInspector] public NavMeshAgent agent;

    public State currentState;
    public State remainState;
	public AbilityCooldown abilityCooldown;

	public GameObject bloodParticle;

    [HideInInspector] public float life = 10.0f;
    [HideInInspector] public float movementSpeed = 1.0f;
    [HideInInspector] public float stamina = 5.0f;
	[HideInInspector] public float damage = 2.0f;
	[HideInInspector] public float attackRange = 1.0f;
	[HideInInspector] public float attackCadence = 1.5f;
    [HideInInspector] public GameObject target;
	[HideInInspector] public GameObject followingAt;
	[HideInInspector] public Ability[] abilities;
	[HideInInspector] public Animator animator;

	[HideInInspector] public float coolDownDuration = 1.2f;
	[HideInInspector] public float nextReadyTime;
	[HideInInspector] public float coolDownTimeLeft;
	[HideInInspector] public float points;
	[HideInInspector] public bool onCooldown = false;

	[HideInInspector] public bool fastRun;
	[HideInInspector] public bool isDead = false;

    // Use this for initialization
    void Start()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();	
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
		agent.enabled = false;
		abilityCooldown = GetComponent<AbilityCooldown> (); 
		animator = GetComponent<Animator> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
		//animator.speed = movementSpeed;
    }
		
    // Update is called once per frame
    void Update()
    {
		UpdateAttackCooldown ();
		UpdateAgent();
    }

	void UpdateAttackCooldown() {
		bool coolDownComplete = (Time.time > nextReadyTime);
		if (coolDownComplete) 
		{
			onCooldown = false;
		} else 
		{
			coolDownTimeLeft -= Time.deltaTime;
		}
	}

	void UpdateAgent() {
		currentState.UpdateState(this);
	}

	public void LaunchAbility() {
		abilityCooldown.RunAbility ();
	}
		
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }

	public void ReceiveDamage(float amount) {
		life -= amount;
		if (life <= 0) {
			// Notificamos al gameManager que hemos muerto
			animator.SetTrigger ("Death");
			StartCoroutine (bloodEffect ());
			isDead = true;
			coll.enabled = false;
			agent.enabled = false;
			gameManager.KillEnemy (points);
			Destroy (gameObject, 2.0f);
		} else {
			animator.SetTrigger ("Damage");
			StartCoroutine (bloodEffect ());
		}
	}

	IEnumerator bloodEffect() {
		GameObject g = Instantiate (bloodParticle, gameObject.transform);
		g.transform.localPosition = Vector3.zero;
		Destroy (g, 1.0f);
		yield return null;
	}
		
}
