using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Prefab : Item_Prefab {

    public float vel = 50f;
    public float damage = 10f;
    public float delay = 3f;
    public GameObject explosionEffect;
    public float radiusDamage = 7f;
    public float force = 1500;

    bool hasExploded = false;

    public override void getItem(CharacterCollisions player){
        base.getItem(player);        
        if (player.playerObject.grenades.Count < player.playerObject.MAX_GRENADES) {
            Debug.Log("+1 Grenade. Grenade no active");
            player.playerObject.grenades.Add(this);
            gameObject.SetActive(false);
        }
    }

    public void useGrenade(Vector3 posInit, Vector3 posFinal) {
        gameObject.SetActive(true);
        gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
        Debug.Log("Grenade active");
        transform.position = posInit;
        gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(posFinal * vel);
        StartCoroutine(explode());
    }

    IEnumerator explode() {
        yield return new WaitForSeconds(delay);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusDamage);
        foreach (Collider col in colliders) {
            Debug.Log("Granada daña a:" + col);
            //Rigidbody rb = col.GetComponent<Rigidbody>();
            //if (rb != null) {
            //    rb.AddExplosionForce(force, transform.position, radiusDamage);
            //}
            if (col.gameObject.tag.Contains("Zombie")) {
                ZombieStateController zs = col.gameObject.GetComponent<ZombieStateController>();
                if (zs) {
                    zs.ReceiveDamage(damage);
                }
            }
            if (col.gameObject.tag.Contains("Player")) {
                col.GetComponentInChildren<Character>().ReceiveDamage((int)damage);
            }
        }
        setVisible(false);
        Destroy(gameObject, audio.clip.length);
    }
}
