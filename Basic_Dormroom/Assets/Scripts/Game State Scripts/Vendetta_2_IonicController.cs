using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendetta_2_IonicController : MonoBehaviour {
	public GameManager gm;
	// Game objects
	public AtomPrefabController atomPrefab;
	// Spawn location
	public GameObject spawnNewAtomsHere;
	// Audio	
	public AudioClip introClip;
	public AudioClip instructionsClip;
	public AudioClip successClip;
	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(introClip, 0.5F);
		Invoke("runChallenge", /*after*/ introClip.length);
	}
	
	// Initiates challenge, seting up completion listener 
	private void runChallenge() {
		if (audio.isPlaying) audio.Stop();
		audio.PlayOneShot(instructionsClip, 0.5F);
		setupChallengeObjects();
		Invoke("advanceGameState", /*after*/ instructionsClip.length); // TO DO: removing "static" from event, I think, makes it not call this specific script
		// gm.OnReactionAttempted += handleReactionAttempt;
	}

	// Instantiate necessary GameObjects in scene: create a 7- and a 1-electron atom
	private void setupChallengeObjects() {
		AtomPrefabController atom_1e = Instantiate(atomPrefab, transform) as AtomPrefabController;
		atom_1e.transform.position = spawnNewAtomsHere.transform.position;
		atom_1e.numElectrons(1);
		AtomPrefabController atom_7e = Instantiate(atomPrefab, transform) as AtomPrefabController;
		atom_7e.transform.position = spawnNewAtomsHere.transform.position + new Vector3(2f, 0f, 0f);
		atom_7e.numElectrons(7);
	}

	// Event listener: triggered when atoms attempt to react (evaluation handled in GameManager)
	private void handleReactionAttempt(GameObject atom1, GameObject atom2, bool reactionSucceededIonic, bool reactionSucceededCovalent) {
		Debug.Log("handling covalent reaction attempt in State controller.");
		if (reactionSucceededIonic) {
			Debug.Log("Reaction succeeded!");
			if (audio.isPlaying) audio.Stop();
			audio.PlayOneShot(successClip, 0.5F);
			Invoke("advanceGameState", /*after*/ successClip.length);
		} else {
			Debug.Log("Reaction failed. :( ");
			// TO DO: ADD SOUND FEEDBACK
		}
	}

	private void advanceGameState() {
		if (audio.isPlaying) audio.Stop();
		gm.advanceGameState();
		Destroy(this.gameObject);
	}
}
