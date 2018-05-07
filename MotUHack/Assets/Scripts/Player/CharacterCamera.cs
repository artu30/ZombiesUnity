using UnityEngine;
using System.Collections;

public class CharacterCamera : MonoBehaviour {
	private Vector3 offset;
	private Ray cameraRay;
	private RaycastHit cameraRayHit;

	private GameObject player;
    private GameObject playerLight;
    private GameObject camera;

    [HideInInspector] public GameObject gameManager;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        player = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
        playerLight = GameObject.FindGameObjectWithTag("PlayerLight");

        offset = camera.transform.position - player.transform.position;
	}

	void Update () {
        if (!gameManager.GetComponent<GameManager>().gameIsPaused && !player.GetComponent<Character>().isDead)
        {
            /* -------------------------------------- */
            /* --------------- Camera --------------- */
            /* -------------------------------------- */
            camera.transform.position = player.transform.position + offset;

            /* -------------------------------------- */
            /* --------------- Player --------------- */
            /* -------------------------------------- */
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, player.transform.position.y, cameraRayHit.point.z);
                player.transform.LookAt(targetPosition);

                playerLight.transform.LookAt(targetPosition);
            }
        }
	}
}