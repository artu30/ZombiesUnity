using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class Character : MonoBehaviour {
    [HideInInspector] public int MAX_LIFE     = 100;
    [HideInInspector] public int MAX_GRENADES = 4;
    [HideInInspector] public int MAX_VACCINES = 5;

    [HideInInspector] public AudioClip hurt1;
    [HideInInspector] public AudioClip hurt2;
    [HideInInspector] public AudioClip hurt3;
    [HideInInspector] public AudioClip hurt4;

    [HideInInspector] public int                  life;
    [HideInInspector] public int                  score;
    [HideInInspector] public int                  vaccines;
    [HideInInspector] public Item_Prefab          weapon = null;
    [HideInInspector] public List<Grenade_Prefab> grenades;
    [HideInInspector] public int                  indexWeapon;
    [HideInInspector] public List<Item_Prefab>    weapons;
    [HideInInspector] public GameObject           playerCollider;
    [HideInInspector] public CharacterController  controller;
    [HideInInspector] public Rigidbody            rigidBody;
    [HideInInspector] public GameObject           gameManager;
    [HideInInspector] public bool                 isDead;

    void Start () {
        isDead         = false;
        score          = 0;
        indexWeapon    = 0;
        gameManager    = GameObject.FindGameObjectWithTag("GameManager");
        playerCollider = GameObject.FindGameObjectWithTag ("PlayerCollider");
		rigidBody      = playerCollider.GetComponent<Rigidbody>();
        life           = 80;
        vaccines       = 0;
        weapon         = null;

    }

	void Update () {
    }

    public void ReceiveDamage(int amount) {
        if (!isDead)
        {
            if (life - amount > 0)
            {
                life -= amount;
                gameManager.GetComponent<GameManager>().setUILife(life);

                if (life > 5)
                {
                    AudioSource hurtSound = GetComponent<AudioSource>();

                    System.Random rand = new System.Random();
                    int hurtRand = rand.Next(1, 5);

                    switch (hurtRand)
                    {
                        case 1:
                            hurtSound.clip = hurt1;
                            break;
                        case 2:
                            hurtSound.clip = hurt2;
                            break;
                        case 3:
                            hurtSound.clip = hurt3;
                            break;
                        case 4:
                            hurtSound.clip = hurt4;
                            break;
                    }

                    hurtSound.Play();
                }
            }
            else
            {
                isDead = true;

                gameManager.GetComponent<GameManager>().setUILife(0);
                gameManager.GetComponent<GameManager>().KillPlayer();
            }
        }
    }

    public void IncrementScore(int points) {
        score += points;
        gameManager.GetComponent<GameManager>().setUIScore(score);
    }
}