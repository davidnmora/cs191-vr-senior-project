using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendetta_3_CovalentController : MonoBehaviour {
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
		Invoke("runChallenge", introClip.length);
	}
	
	// Initiates challenge, seting up completion listener 
	private void runChallenge() {
		if (audio.isPlaying) audio.Stop();
		audio.PlayOneShot(instructionsClip, 0.5F);
		setupChallengeObjects();
		gm.OnReactionAttempted += handleReactionAttempt;
	}

	// Instantiate necessary GameObjects in scene: two 6-electron atoms
	private void setupChallengeObjects() {
		AtomPrefabController atom_6e_a = Instantiate(atomPrefab, transform) as AtomPrefabController;
		atom_6e_a.transform.position = spawnNewAtomsHere.transform.position;
		atom_6e_a.numElectrons(6);
		AtomPrefabController atom_6e_b = Instantiate(atomPrefab, transform) as AtomPrefabController;
		atom_6e_b.transform.position = spawnNewAtomsHere.transform.position + new Vector3(2f, 0f, 0f);
		atom_6e_b.numElectrons(6);
	}

	// Event listener: triggered when atoms attempt to react (evaluation handled in GameManager)
	private void handleReactionAttempt(GameObject atom1, GameObject atom2, bool reactionSucceededIonic, bool reactionSucceededCovalent) {
		if (reactionSucceededCovalent) {
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
