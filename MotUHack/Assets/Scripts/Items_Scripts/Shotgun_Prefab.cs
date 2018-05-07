using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Prefab : Item_Prefab, IWeapon
{

    public int ammo = 40;
    public float cadence = 1.5f;
    public float velBullet = 4000f;
    public float damage = 1f;
    public GameObject bulletPrefab;
    public GameObject bulletPosition;
    public AudioSource shootAudio;
    public AudioSource noBulletsAudio;

    private bool canShoot = true;

    public override void getItem(CharacterCollisions player) {
        base.getItem(player);
        bool alreadyHasAShotgun = false;
        foreach (Item_Prefab i in player.playerObject.weapons) {
            if (i.gameObject.GetComponent<Shotgun_Prefab>()) {
                alreadyHasAShotgun = true;
                i.gameObject.GetComponent<Shotgun_Prefab>().ammo += ammo;
                destroyItem();
            }
        }
        if (!alreadyHasAShotgun){
            Debug.Log("Now player has a Shotgun");
            transform.SetParent(player.playerObject.transform);
            transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            transform.position = player.shotgunPosition.transform.position;
            transform.rotation = Quaternion.Euler(0, player.playerObject.transform.rotation.eulerAngles.y, 0);
            player.playerObject.weapons.Add(this);
            if (player.playerObject.weapon == null) {
                player.playerObject.weapon = this;
            }
            else {
                setVisible(false);
            }
        }       
    }

    public int shoot(Vector3 objective) {
        if (canShoot) {
            if (ammo <= 0) {
                Debug.Log("No bullets");
                // Sonido del arma
                noBulletsAudio.Play();
            }
            else {
                ammo -= 5;
                float[] angles = new float[] { -10f, -5f, 0f, 5f, 10f };
                for (int i = 0; i < 5; i++) {
                    GameObject bullet = Instantiate(bulletPrefab, bulletPosition.transform.position, bulletPosition.transform.rotation);
                    Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
                    Vector3 rotatedVector = Quaternion.AngleAxis(angles[i], Vector3.up) * objective;
                    float velBulletRand = velBullet + Random.Range(-600,200);
                    bulletRigidBody.AddForce(rotatedVector * velBulletRand);
                    Bullet_Prefab bulletScript = bullet.GetComponent<Bullet_Prefab>();
                    bulletScript.setDamage(damage);
                }
                // Sonido del arma
                shootAudio.Play();
            }
            canShoot = false;
            StartCoroutine(waitCadence(cadence));
        }
        return ammo;
    }

    public void throwWeapon(Vector3 dir) {
        Debug.Log("Gun Throwed");
        // Gravedad
        // Fuerza
        // Deteccion de colision como la bala
        // Hacer daño
        // Destruir si ha pegado a un zombie, si no que se quede en el suelo
        destroyItem();
    }

    public int getAmmo()
    {
        return ammo;
    }

    IEnumerator waitCadence(float cadence) {
        yield return new WaitForSeconds(cadence);
        canShoot = true;
    }
}