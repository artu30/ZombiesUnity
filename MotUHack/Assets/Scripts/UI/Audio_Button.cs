using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Audio_Button : MonoBehaviour {

	public AudioClip[] sounds;

	private Button button { get { return GetComponent<Button> (); } }
	private AudioSource source { get { return GetComponent<AudioSource> (); } }


	// Use this for initialization
	void Start () {
		gameObject.AddComponent<AudioSource> ();

		source.playOnAwake = false;
		source.volume = 0.3f;

	}


	public void PlayHoverSound(){
		source.clip = sounds[0];
		source.PlayOneShot (sounds[0]);
	}

	public void PlayClickSound(){
		source.clip = sounds[1];
		source.PlayOneShot (sounds[1]);
	}

}
