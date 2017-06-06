using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomController : MonoBehaviour {
	public GameManager gm;
	// Audio
	public AudioClip zap;
	public AudioClip zing;
	AudioSource audio;
	// Data and Counting
	public int numElectrons; // atom's current electron count
	private GameObject[] electrons = new GameObject[8];
	// Styling
	Color atomColorEmptyValence = new Color(0.0f, 0.0f, 1f, 0.9f); // green
	Color atomColorFullValence  = new Color(0.0f, 1f, 0.0f, 0.9f);  // blue


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	// PUBLIC FUNCTIONS
	// Resets atom and electron appearances based on numElectrons atom has
	public void refreshElectrons(int newNumElectrons) {
		numElectrons = newNumElectrons;
		// Interpolate atom body color (or make yellow if valence full)
		Color atomColor = (numElectrons == 8) ? Color.yellow : Color.Lerp(atomColorEmptyValence, atomColorFullValence, numElectrons/8f);
		setMaterialColor(this.gameObject, atomColor);
		setLight(this.gameObject, atomColor);
		for(int eNum = 0; eNum < 8; eNum++) {
			electrons[eNum] = this.gameObject.transform.GetChild(eNum).gameObject; // populate
			if (eNum < numElectrons) { // set active electrons
				setLight(electrons[eNum], Color.blue);
				setMaterialColor(electrons[eNum], Color.blue);
				muteAudio(electrons[eNum], false);
			} else { // electron is "empty"
			setLight(electrons[eNum], isEnabled: false);
				setMaterialColor(electrons[eNum], new Color(1f, 0.0f, 0.0f, 0.1f)); // transparent red
				muteAudio(electrons[eNum], true);
			}
		}
	}

	// COLLISIONS AND INTERACTIONS
	void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);
	}



	// PRIVATE HELPER FUNCTIONS
	private void setLight(GameObject gameObj, Color color = new Color(), bool isEnabled = true) {
		Light lightComp = gameObj.GetComponent<Light>();
		lightComp.color = color;
		lightComp.enabled = isEnabled;
	}

	private void setMaterialColor(GameObject gameObj, Color color) {
		gameObj.GetComponent<Renderer>().material.color = color;
	}

	private void muteAudio(GameObject gameObj, bool muteIt) {
		gameObj.GetComponent<AudioSource>().mute = muteIt;
	}

	// PLACEHOLDER FOR TOUCH INTERACTIONS
	// placeholder for a grab and hold by touch controller
	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			audio.PlayOneShot(zap, 0.7F);
			Debug.Log("AtomPrefabController CLICK___________");
			if (this != gm.firstSelectedAtom && this != gm.secondSelectedAtom) {
				if (gm.firstSelectedAtom == null) {
					gm.firstSelectedAtom = this;
					Debug.Log("I'm the new firstSelectedAtom: "+ this.gameObject.name);
				} else if (gm.secondSelectedAtom == null) {
					gm.secondSelectedAtom = this;
					Debug.Log("I'm the new secondSelectedAtom: " + this.gameObject.name);
				}
			// (anything else impossible: user only has two touch controllers)
			}
			
		}
	}

}
