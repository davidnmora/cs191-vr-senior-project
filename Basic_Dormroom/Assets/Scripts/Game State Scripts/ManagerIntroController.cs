using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerIntroController : MonoBehaviour {
	public GameManager gm;
	// Audio	
	public AudioClip clip;
	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(clip, 0.4F);
		// Invoke("advanceGameState", /*after*/ clip.length);
	}
	
	void Update () {

	}

	// Called by trigger on Electron Container
	public void advanceGameState() {
		if (audio.isPlaying) audio.Stop();
		gm.advanceGameState();
		Destroy(this.gameObject);
	}
}
