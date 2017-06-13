using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomPrefabController : MonoBehaviour {
	public GameManager gm;

	public AtomController atom; // actual atom gameObject class
	public bool isSelected = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// PUBLIC METHODS
	public int numElectrons() {
		return atom.numNativeElectrons + atom.numSharedElectrons + atom.numTransferedElectrons;
	}

	public void numElectrons(int numNativeElectrons) {
		atom.refreshElectrons(numNativeElectrons);
	}

	public void incrementSharedElectrons(int increment) {
		atom.numSharedElectrons += increment;
	}

	public void incrementTransferedElectrons(int increment) {
		atom.numTransferedElectrons += increment;
	}


}
