using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomController : MonoBehaviour {
	// Scripts
	public GameManager gm;
	public GameObject AtomInfoTextController;
	// public OVRGrabbable OVRG; // TOUCH TO DO: DRAG OVR INTO THIS IN LAB SCENE
	// Audio
	public AudioClip zap;
	public AudioClip zing;
	private AudioSource audio;
	// Electron Data and Counting
	public int numNativeElectrons; // atom's current electron count
	public int numSharedElectrons = 0;
	public int numTransferedElectrons = 0;
	private GameObject[] electrons = new GameObject[8];
	// Styling
	Color atomColorEmptyValence = new Color(0.0f, 0.0f, 1f); // green
	Color atomColorFullValence  = new Color(0.0f, 1f, 0.0f);  // blue
	// Events
	public delegate void FilledValence(GameObject go);
	public static event FilledValence OnFilledValence;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// If held by controller
		if (/* OVRG.isGrabbed */ true) { // TOUCH TO DO
			this.gameObject.tag = "GrabbedAtom";
			AtomInfoTextController.SetActive(true); // display count above it
		} else {
			this.gameObject.tag = "Untagged";
			AtomInfoTextController.SetActive(false);
		}
		
	}

	// PUBLIC FUNCTIONS
	// Resets atom and electron appearances based on numNativeElectrons atom has
	public void refreshElectrons(int newNumNativeElectrons) {
		numNativeElectrons = newNumNativeElectrons; // used on INITIALIZEATION to set numNativeElectrons
		var totalNumElectrons = numTotalElectrons();
		if ((totalNumElectrons == 8) && (OnFilledValence != null)) OnFilledValence(this.gameObject);
		// Interpolate atom body color (or make yellow if valence full)
		Color atomColor = (totalNumElectrons == 8) ? Color.yellow : Color.Lerp(atomColorEmptyValence, atomColorFullValence, totalNumElectrons/8f);
		setMaterialColor(this.gameObject, atomColor);
		for(int eNum = 0; eNum < 8; eNum++) {
			electrons[eNum] = this.gameObject.transform.GetChild(eNum).gameObject; // populate
			if (eNum < totalNumElectrons) { // set active electrons
				setMaterialColor(electrons[eNum], Color.blue);
				muteAudio(electrons[eNum], false);
			} else { // electron is "empty"
				setMaterialColor(electrons[eNum], new Color(1f, 0.0f, 0.0f, 0.1f)); // transparent red
				muteAudio(electrons[eNum], true);
			}
		}
	}

	// COLLISIONS AND INTERACTIONS
	void OnTriggerEnter(Collider other) {
		Debug.Log("Collision with atom: " + other.gameObject.name);
		handleElectronCollision(other.gameObject);
		
	}



	// PRIVATE HELPER FUNCTIONS
	private void setMaterialColor(GameObject gameObj, Color color) {
		gameObj.GetComponent<Renderer>().material.color = color;
	}

	private void muteAudio(GameObject gameObj, bool muteIt) {
		gameObj.GetComponent<AudioSource>().mute = muteIt;
	}

	private void handleElectronCollision(GameObject electron) {
		if (electron.name == "GrabbableElectron") {
			// delete it, incr. transferedElectrons (if legal), 
			if (numTotalElectrons() < 8) {
				Destroy(electron);
				numTransferedElectrons++;
				refreshElectrons(numNativeElectrons);
			} else {
				// TO DO: give visual + audio feedback
				audio.PlayOneShot(zap, 0.5F);
			}
		}
	}

	public int numTotalElectrons() {
		return numNativeElectrons + numSharedElectrons + numTransferedElectrons;
	}

	// PLACEHOLDER FOR TOUCH INTERACTIONS
	// placeholder for a grab and hold by touch controller
	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			audio.PlayOneShot(zap, 0.5F);
			gm.advanceGameState();
		}
	}

}
