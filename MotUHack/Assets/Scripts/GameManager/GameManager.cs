using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameLevel {
	MAIN_MENU,
	LEVEL01
}

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public bool gameIsPaused = false;

	public GameLevel currentGameLevel { get; private set;}

	[SerializeField]
	public Transform[] zombiesSpawnerPrefab;

	[SerializeField]
	public Transform[] objectsSpawnerPrefab;

	public Transform playerPrefab;

	public AudioClip soundTrack;
	private AudioSource helicopterAudioSource;
	private bool helichopterIsPlaying = false;
	private bool playerCanBeatGame = false;

	private AudioSource source { get { return GetComponent<AudioSource> (); } }

	private bool soundTrackPlaying = false;

	private float score = 0;

	private float playerTime = 0.0f;

	Text lifeText;
	Text bulletsText;
	Text grenadesText;
	Text syringesText;
	Text scoreText;

	public GameObject pauseMenuUI;
	public GameObject gameOverUI;
	public GameObject gameBeatedUI;
	//public GameObject messageBoardUI;
	public GameObject messageWeaponBoardUI;
	public GameObject boxWeaponMessagePrefab;
	public GameObject boxMessage;

	public Text timeText;

	public GameObject GameBeatedArea;

	void Awake() {

		if (gm == null) {
			gm = this;
		} 

		currentGameLevel = (GameLevel)SceneManager.GetActiveScene ().buildIndex;

		GameObject PlayerUI = GameObject.FindGameObjectWithTag ("UI");
		if (PlayerUI) {
			Text[] lista = PlayerUI.gameObject.GetComponentsInChildren<Text> ();

			scoreText = lista [0];
			bulletsText = lista [1];
			lifeText = lista [2];
			grenadesText = lista [3];
			syringesText = lista [4];

		} else {
			Debug.Log ("GameManager: PlayerUI is Null");
		}

		if (pauseMenuUI == null) {
			Debug.Log ("GameManager: pauseMenuUI is Null");
		}

		if (gameOverUI == null) {
			Debug.Log ("GameManager: gameOverUI is Null");
		}

		if (gameBeatedUI == null) {
			Debug.Log ("GameManager: gameBeatedUI is Null");
		}

		helicopterAudioSource = gameObject.GetComponentInChildren<AudioSource> ();
	}

	void Start(){ 
		
		playerTime = Time.time;

		gameObject.AddComponent<AudioSource> ();

		if (currentGameLevel == GameLevel.LEVEL01) {
			
			source.clip = soundTrack;
			source.volume = 0.1f;
			source.loop = true;
			source.Play();
			soundTrackPlaying = true;

		}

		
		if(playerPrefab != null){
			
			lifeText.text = playerPrefab.GetComponentInChildren<Character>().life + " / 100";

			//bulletsText = playerPrefab.GetComponent<Character> ().getBullets ();
			//scoreText = playerPrefab.GetComponent<Character> ().getScore ();	
		}

		/*
		if (messageBoardUI != null) {
			StartCoroutine (showMessage ("Find 5 vaccines to scape!"));
		}
		*/

		if (messageWeaponBoardUI != null) {
			StartCoroutine(showMessage("Find 5 vaccines to scape!"));
		}
	}

	void Update(){
	
		if (Input.GetButtonUp ("Pause") && currentGameLevel != GameLevel.MAIN_MENU) {

			if (gameIsPaused) { ResumeGame (); } 

			else              { PauseGame ();  }
		}
	
	}

	public void ResumeGame(){

	//	messageBoardUI.SetActive (true);
		messageWeaponBoardUI.SetActive (true);

		if (soundTrackPlaying) {
			source.volume = 0.1f;
		}

		if (playerCanBeatGame && !helichopterIsPlaying) {
			helicopterAudioSource.volume = 0.4f;
			helichopterIsPlaying = true; 
		}

		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	void PauseGame(){

	//	messageBoardUI.SetActive (false);
		messageWeaponBoardUI.SetActive (false);

		if (soundTrackPlaying) {
			source.volume = 0.03f;
		}

		if (playerCanBeatGame && helichopterIsPlaying) {
			helicopterAudioSource.volume = 0.0f;
			helichopterIsPlaying = false;
		}

		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void KillEnemy(float points) {
		score += points;
		scoreText.text = score.ToString();
	}

	public void KillPlayer() {
		
		Debug.Log ("Player killed");
		Animator playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		playerAnimator.SetBool("walkFordward", false);
		playerAnimator.SetBool("walkBackward", false);
		playerAnimator.SetBool("walkLeft", false);
		playerAnimator.SetBool("walkRight", false);
		playerAnimator.SetBool("dieHard", true);

		AnimationClip[] clipsAnim = playerAnimator.runtimeAnimatorController.animationClips;
	
		int lenghtList = clipsAnim.Length;

		float timeAnimation = 0.0f;


		for (int i = 0; i < lenghtList; i++) {
			if (clipsAnim [i].name == "Dying Backwards") {
				timeAnimation = clipsAnim [i].length - 1;
			}

		}
			
		AudioSource sourcePlayer = GameObject.FindGameObjectWithTag ("PlayerDeathSound").GetComponent<AudioSource> ();

		sourcePlayer.enabled = true;
		sourcePlayer.volume = 0.1f;
		sourcePlayer.Play ();
	
		StartCoroutine(AnimationKillPlayer(timeAnimation));
	}

	IEnumerator AnimationKillPlayer(float seconds){
		yield return new WaitForSeconds (seconds);
		EndGame ();
	}
		
	public void LoadScene(int indexScene){
		if (gameIsPaused) {
			Time.timeScale = 1f;
			gameIsPaused = false;
		}
		gm.currentGameLevel = (GameLevel)indexScene;
		SceneManager.LoadScene(indexScene);
	}

	public void setUILife(int life){
		lifeText.text = life.ToString() + " / 100";
	}

	public void setUIBullets(int bullets){
		bulletsText.text = bullets.ToString();
	}

	public void setUIGrenades(int grenades){
		grenadesText.text = grenades.ToString();
	}

	public void setUISyringes(int syringes){
		syringesText.text = syringes.ToString () + " / 5";
	}

	public void setUIScore(int score){
		scoreText.text = score.ToString();
	}
		
	public void ExitGame(){
		Application.Quit ();
	}
		
	void EndGame() {
	//	messageBoardUI.SetActive (false);
		messageWeaponBoardUI.SetActive (false);

		if (soundTrackPlaying) {
			source.volume = 0.03f;
		}

		if (playerCanBeatGame && helichopterIsPlaying) {
			helicopterAudioSource.volume = 0.0f;
			helichopterIsPlaying = false;
		}

		Debug.Log ("Gamer Over!!!");
		gameOverUI.SetActive (true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void PlayerCanBeatGame(){
		
		//GameObject.FindGameObjectWithTag("GameBeated").SetActive (true);

		GameBeatedArea.SetActive(true);
		playerCanBeatGame = true;
		helicopterAudioSource.volume = 0.4f;
		helicopterAudioSource.Play ();
		helichopterIsPlaying = true;

		if (messageWeaponBoardUI != null) {
			StartCoroutine(showMessage("Find the scape area!"));
		}
	}

	public void GameBeated(){

		//messageBoardUI.SetActive (false);
		messageWeaponBoardUI.SetActive (false);

		if (soundTrackPlaying) {
			source.volume = 0.03f;
		}

		if (playerCanBeatGame && helichopterIsPlaying) {
			helicopterAudioSource.volume = 0.0f;
			helichopterIsPlaying = false;
		}

		Debug.Log ("Emtro en Game Beated");

		float lastTime = Time.time;
		playerTime = lastTime - playerTime;

		int minutes = (int)(playerTime / 60);
		int seconds = (int)(playerTime % 60);


		timeText.text = "Time: " + minutes + "m " + seconds + "s";

		Time.timeScale = 0f;
		gameIsPaused = true;

		gameBeatedUI.SetActive (true);
		Debug.Log ("GameBeated");
	}

    public void weaponMessage(string wMessage)
    {
        if (wMessage != "")
        {
            StartCoroutine(showWeaponMessage(wMessage));
        }
    }

	IEnumerator showMessage(string message){
		Debug.Log ("ShowMeassage");

		GameObject messageBox = Instantiate(boxMessage);
		messageBox.transform.SetParent (messageWeaponBoardUI.transform, false);
		Text messageText = messageBox.GetComponentInChildren<Text> ();
		messageText.text = message;
		messageBox.SetActive (true);
		Destroy (messageBox, 5.0f);
		yield return new WaitForSeconds (1);

	}

	IEnumerator showWeaponMessage(string message){
		

		Debug.Log ("ShowWeaponMeassage");
		GameObject messageBox = Instantiate(boxWeaponMessagePrefab);
		messageBox.transform.SetParent (messageWeaponBoardUI.transform, false);
		Text messageText = messageBox.GetComponentInChildren<Text> ();
		messageText.text = message;
		messageBox.SetActive (true);
		Destroy (messageBox, 5.0f);
		yield return new WaitForSeconds (1);

	}
}
