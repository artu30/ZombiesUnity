using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Prefab : Item_Prefab, IWeapon{

    public int ammo = 100;
    public float cadence = 1f;
    public float velBullet = 4000f;
    public float damage = 1f;
    public GameObject bulletPrefab;
    public GameObject bulletPosition;
    public AudioSource shootAudio;
    public AudioSource shellAudio;
    public AudioSource noBulletsAudio;

    private bool canShoot = true;

    public override void getItem(CharacterCollisions player) {
        base.getItem(player);
        bool alreadyHasAPistol = false;
        foreach(Item_Prefab i in player.playerObject.weapons) {
            if (i.gameObject.GetComponent<Pistol_Prefab>()) {
                alreadyHasAPistol = true;
                i.gameObject.GetComponent<Pistol_Prefab>().ammo += ammo;
                destroyItem();
            }
        }
        if (!alreadyHasAPistol) {
            Debug.Log("Now player has a Pistol");
            transform.SetParent(player.playerObject.transform);
            transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            transform.position = player.gunPosition.transform.position;
            transform.rotation = Quaternion.Euler(0, player.playerObject.transform.rotation.eulerAngles.y + 180, 0);
            player.playerObject.weapons.Add(this);
            if (player.playerObject.weapon == null)
            {
                player.playerObject.weapon = this;
            }
            else
            {
                setVisible(false);
            }
        }
    }

    public int shoot(Vector3 objective){
        if (canShoot){
            if (ammo <= 0) {
                Debug.Log("No bullets");
                // Sonido del arma
                noBulletsAudio.Play();
            }
            else {
                --ammo;
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition.transform.position, bulletPosition.transform.rotation);
                Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
                bulletRigidBody.AddForce(objective * velBullet);
                Bullet_Prefab bulletScript = bullet.GetComponent<Bullet_Prefab>();
                bulletScript.setDamage(damage);
                // Sonido del arma
                StartCoroutine(playShootSound());
            }
            canShoot = false;
            StartCoroutine(waitCadence(cadence));
        }
        return ammo;
    }

    public void throwWeapon(Vector3 dir){
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

    IEnumerator playShootSound() {
        shootAudio.Play();
        yield return new WaitForSeconds(shootAudio.clip.length - 0.5f);
        shellAudio.Play();
    }
}
