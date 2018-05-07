using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

    [HideInInspector] public Animator playerAnimator;
    [HideInInspector] public AudioSource playerSound;

    // Use this for initialization
    void Start () {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerSound = GameObject.FindGameObjectWithTag("PlayerSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f) {
            playerSound.enabled = false;

            playerAnimator.SetBool("walkFordward", false);
            playerAnimator.SetBool("walkBackward", false);
            playerAnimator.SetBool("walkLeft", false);
            playerAnimator.SetBool("walkRight", false);
            playerAnimator.SetBool("dieHard", false);
        }
        else {
            playerSound.enabled = true;

            if (Input.GetAxis("Vertical") > 0.0f) {
                playerAnimator.SetBool("walkFordward", true);
                playerAnimator.SetBool("walkBackward", false);
                playerAnimator.SetBool("walkLeft", false);
                playerAnimator.SetBool("walkRight", false);
                playerAnimator.SetBool("dieHard", false);
            }
            else if (Input.GetAxis("Vertical") < 0.0f) {
                playerAnimator.SetBool("walkFordward", false);
                playerAnimator.SetBool("walkBackward", true);
                playerAnimator.SetBool("walkLeft", false);
                playerAnimator.SetBool("walkRight", false);
                playerAnimator.SetBool("dieHard", false);
            }

            if (Input.GetAxis("Horizontal") > 0.0f) {
                playerAnimator.SetBool("walkFordward", false);
                playerAnimator.SetBool("walkBackward", false);
                playerAnimator.SetBool("walkLeft", false);
                playerAnimator.SetBool("walkRight", true);
                playerAnimator.SetBool("dieHard", false);
            }
            else if (Input.GetAxis("Horizontal") < 0.0f) {
                playerAnimator.SetBool("walkFordward", false);
                playerAnimator.SetBool("walkBackward", false);
                playerAnimator.SetBool("walkLeft", true);
                playerAnimator.SetBool("walkRight", false);
                playerAnimator.SetBool("dieHard", false);
            }
        }
    }
}
