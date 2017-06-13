using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendetta_1_EightElectronsController : MonoBehaviour {
	public GameManager gm;
	// Game objects
	public AtomPrefabController atomPrefab;
	// Audio	
	public AudioClip introClip;
	public AudioClip instructionsClip;
	public AudioClip successClip;
	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(introClip, 0.5F);
		Invoke("runChallenge", /*after*/ /*introClip.length*/ 3);
	}
	
	// Initiates challenge, seting up completion listener 
	private void runChallenge() {
		if (audio.isPlaying) audio.Stop();
		audio.PlayOneShot(instructionsClip, 0.5F);
		setupChallengeObjects();
		// subscribe to event of Atom filling its valence
		AtomController.OnFilledValence += valenceFilled;
	}

	// Instantiate necessary GameObjects in scene
	private void setupChallengeObjects() {
		AtomPrefabController atom = Instantiate(atomPrefab, transform) as AtomPrefabController;
		atom.numElectrons(7);
	}

	// Event listener: triggered when an atom fills it electrons
	private void valenceFilled(GameObject atom) {
		Debug.Log("Atom has full valence!!!");
		if (audio.isPlaying) audio.Stop();
		audio.PlayOneShot(successClip, 0.5F);
		Invoke("advanceGameState", /*after*/ successClip.length);
	}

	private void advanceGameState() {
		if (audio.isPlaying) audio.Stop();
		gm.advanceGameState();
		Destroy(this.gameObject);
	}
}
