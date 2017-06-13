using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomInfoTextController : MonoBehaviour {

	public AtomController atom;

	void Start () {
		
	}
	
	void Update () {
		GetComponent<TextMesh>().text = updateElectronInfo();
		transform.LookAt(Camera.main.transform);
	}

	public string updateElectronInfo() {
		return "Started with: " + atom.numNativeElectrons.ToString() + "\nSharing: " + atom.numSharedElectrons.ToString() + "\nGained: " + atom.numTransferedElectrons.ToString();
	}
}
