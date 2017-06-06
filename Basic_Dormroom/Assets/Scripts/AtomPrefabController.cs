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
		if (atom == gm.firstSelectedAtom /*|| atom == gm.secondSelectedAtom*/) {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				transform.Translate(0.1f, 0, 0);
			}

			if (Input.GetKey(KeyCode.RightArrow)) {
				transform.Translate(-0.1f, 0, 0);
			}
		}
		
		
	}

	// PUBLIC METHODS

	public int numElectrons() {
		return atom.numElectrons;
	}

	public void numElectrons(int num) {
		atom.refreshElectrons(num);
	}


}
