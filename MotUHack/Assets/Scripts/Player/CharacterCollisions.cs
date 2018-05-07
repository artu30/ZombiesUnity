using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class CharacterCollisions : MonoBehaviour {
	private float speed;
	private float rotationSpeed;
	private float maxVelocityChange;
    [HideInInspector] public Rigidbody   rigidBody;
    [HideInInspector] public GameObject  gameManager;
    [HideInInspector] public Character   playerObject;
    private Ray cameraRay;
    private RaycastHit cameraRayHit;
    
    private GameObject camera;
    private Vector3 offset;

    public GameObject gunPosition;
    public GameObject shotgunPosition;
    public GameObject grenadePosition;

    bool qKeyPressed = false;

    [HideInInspector] public Animator playerAnimator;

    void Start () {

        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        offset = camera.transform.position - transform.position;
        playerObject      = GetComponentInChildren<Character>();
        rigidBody         = GetComponent<Rigidbody>();
		speed             = 5.0f;
		rotationSpeed     = 10.0f;
		maxVelocityChange = 5.0f;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().gameIsPaused && !playerObject.isDead) {
            // Se movera solo en eje x y en eje z
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            // Velocidad del personaje
            Vector3 targetVelocity = new Vector3(horizontal, 0, vertical);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Aplica fuerza al chocar para nuestro personaje
            Vector3 velocity = rigidBody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);

            // Movimiento del personaje por ejes
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            // Muevo el rigidbody a la velocidad establecida
            rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);

            if (Input.GetKey(KeyCode.Q))
            {

                if (!qKeyPressed)
                {
                    qKeyPressed = true;
                    cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if ((Physics.Raycast(cameraRay, out cameraRayHit)) && ((playerObject.grenades.Count > 0)))
                    {
                        Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                        Vector3 dir = targetPosition - transform.position;
                        dir.y = targetPosition.y;
                        playerObject.grenades[playerObject.grenades.Count - 1].useGrenade(grenadePosition.transform.position, dir);
                        playerObject.grenades.RemoveAt(playerObject.grenades.Count - 1);

                        gameManager.GetComponent<GameManager>().setUIGrenades(playerObject.grenades.Count);
                    }
                }
            }
            else
            {
                qKeyPressed = false;
            }

            if (playerObject.weapon != null)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    int bullets = playerObject.weapon.GetComponent<IWeapon>().shoot(playerObject.transform.forward);
                    gameManager.GetComponent<GameManager>().setUIBullets(bullets);
                }
            }

            if (playerObject.weapon != null)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    //playerObject.weapon.GetComponent<IWeapon>().throwWeapon(playerObject.transform.forward);
                    Debug.Log("Tirar arma");
                }
            }

            var d = Input.GetAxis("Mouse ScrollWheel");
            if (d > 0f)
            {
                if (playerObject.weapons.Count > 0)
                {
                    if (playerObject.indexWeapon + 1 >= playerObject.weapons.Count)
                    {
                        playerObject.indexWeapon = 0;
                    }
                    else
                    {
                        playerObject.indexWeapon++;
                    }
                }
            }
            else if (d < 0f)
            {
                if (playerObject.weapons.Count > 0)
                {
                    if (playerObject.indexWeapon - 1 < 0)
                    {
                        playerObject.indexWeapon = playerObject.weapons.Count - 1;
                    }
                    else
                    {
                        playerObject.indexWeapon--;
                    }
                }
            }

            if (d > 0f || d < 0f)
            {
                if (playerObject.weapon != null)
                {
                    playerObject.weapon.GetComponent<Item_Prefab>().setVisible(false);
                    playerObject.weapon = playerObject.weapons[playerObject.indexWeapon];
                    gameManager.GetComponent<GameManager>().setUIBullets(playerObject.weapon.GetComponent<IWeapon>().getAmmo());
                    playerObject.weapon.GetComponent<Item_Prefab>().setVisible(true);

                    if (playerObject.weapon.GetComponent<Pistol_Prefab>() != null)
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("Have a gun in your hand!!");
                    }
                    else if (playerObject.weapon.GetComponent<Uzi_Prefab>() != null)
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("Changed to uzi, smart guy!!");
                    }
                    else if (playerObject.weapon.GetComponent<Shotgun_Prefab>() != null)
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("Uh, you use a shotgun... shoot!!");
                    }
                }
            }
        }
        else if (playerObject.isDead) {

            playerAnimator.SetBool("walkFordward", false);
            playerAnimator.SetBool("walkBackward", false);
            playerAnimator.SetBool("walkLeft", false);
            playerAnimator.SetBool("walkRight", false);
            playerAnimator.SetBool("dieHard", true);

            rigidBody.isKinematic = true;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Item") {
            //Debug.Log(other.GetComponent<Grenade_Prefab>());
            if (other.GetComponent<Grenade_Prefab>() != null)
            {
                gameManager.GetComponent<GameManager>().weaponMessage("Pick up a grenade!! Destroy everyone!");
                other.GetComponent<Item_Prefab>().getItem(this);
                gameManager.GetComponent<GameManager>().setUIGrenades(playerObject.grenades.Count);
            }
            else if (other.GetComponent<Vaccine_Prefab>() != null)
            {
                other.GetComponent<Item_Prefab>().getItem(this);
                gameManager.GetComponent<GameManager>().setUISyringes(playerObject.vaccines);
                if (playerObject.vaccines == playerObject.MAX_VACCINES)
                {
                    gameManager.GetComponent<GameManager>().weaponMessage("Now you can escape!!");
                    gameManager.GetComponent<GameManager>().PlayerCanBeatGame();
                }
                else {
                    gameManager.GetComponent<GameManager>().weaponMessage("Pick up a vaccine!!");
                }
            }
            else if (other.GetComponent<FirstAid_Prefab>() != null)
            {
                gameManager.GetComponent<GameManager>().weaponMessage("Pick up a First Aid Kit!! Do you feel better?!");
                other.GetComponent<Item_Prefab>().getItem(this);
                gameManager.GetComponent<GameManager>().setUILife(playerObject.life);
            }
            else
            {
                bool alreadyHasTheWeapon = false;
                if (other.GetComponent<Pistol_Prefab>() != null) {
                    foreach (Item_Prefab i in playerObject.weapons) {
                        if (i.gameObject.GetComponent<Pistol_Prefab>()) {
                            alreadyHasTheWeapon = true;
                        }                        
                    }
                    if (alreadyHasTheWeapon) {
                        gameManager.GetComponent<GameManager>().weaponMessage("More ammo for the Makarov!! Enjoy it");
                    } else {
                        gameManager.GetComponent<GameManager>().weaponMessage("Pickup gun!! Enjoy the Makarov");
                    }
                }
                else if (other.GetComponent<Uzi_Prefab>() != null)
                {
                    foreach (Item_Prefab i in playerObject.weapons)
                    {
                        if (i.gameObject.GetComponent<Uzi_Prefab>())
                        {
                            alreadyHasTheWeapon = true;
                        }
                    }
                    if (alreadyHasTheWeapon)
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("More ammo for the uzi!! Use it with caution");
                    }
                    else
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("Pickup uzi!! Use it with caution");
                    }
                }
                else if (other.GetComponent<Shotgun_Prefab>() != null)
                {
                    foreach (Item_Prefab i in playerObject.weapons)
                    {
                        if (i.gameObject.GetComponent<Shotgun_Prefab>())
                        {
                            alreadyHasTheWeapon = true;
                        }
                    }
                    if (alreadyHasTheWeapon)
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("More ammo for the shotgun!! Be a badass!!");
                    }
                    else
                    {
                        gameManager.GetComponent<GameManager>().weaponMessage("Pickup shotgun!! Be a badass!!");
                    }
                }
                other.GetComponent<Item_Prefab>().getItem(this);
                gameManager.GetComponent<GameManager>().setUIBullets(playerObject.weapon.GetComponent<IWeapon>().getAmmo());
            }
        }

		if (other.gameObject.tag == "GameBeated") {
			other.GetComponent<BeatGameTrigger> ().GameBeated ();	
		}

    }
}