using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendetta_1_EightElectronsController : MonoBehaviour {
	public GameManager gm;
	// Game objects
	public AtomPrefabController atomPrefab;
	public Generate8Atoms generate8Atoms;
	// Spawn location
	public GameObject spawnNewAtomsHere;
	// Audio	
	public AudioClip introClip;
	public AudioClip instructionsClip;
	public AudioClip successClip;
	private AudioSource audio;

	void Start () {
		GameObject.Find("Vendetta_1_Generate8Atoms").GetComponent<Generate8Atoms>().instantiate8Atoms();
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(introClip, 0.5F);
		Invoke("runChallenge", /*after*/ introClip.length);
	}
	
	// Initiates challenge, seting up completion listener 
	private void runChallenge() {
		generate8Atoms.gameObject.SetActive(false);
		if (audio.isPlaying) audio.Stop();
		audio.PlayOneShot(instructionsClip, 0.5F);
		setupChallengeObjects();
		// subscribe to event of Atom filling its valence
		AtomController.OnFilledValence += valenceFilled;
	}

	// Instantiate necessary GameObjects in scene
	private void setupChallengeObjects() {
		AtomPrefabController atom = Instantiate(atomPrefab, transform) as AtomPrefabController;
		atom.transform.position = spawnNewAtomsHere.transform.position;
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
