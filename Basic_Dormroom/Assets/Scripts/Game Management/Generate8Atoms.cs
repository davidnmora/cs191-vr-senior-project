using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate8Atoms : MonoBehaviour {
	public AtomPrefabController atomPrefab;
	public Transform atomsStartPos;
	private AtomPrefabController[] atoms = new AtomPrefabController[8];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void instantiate8Atoms() {
		for (int eCount = 0; eCount < 8; eCount++) {
			// create an AtomPrefab with eCount electrons
			atoms[eCount] = Instantiate(atomPrefab, transform) as AtomPrefabController;
			atoms[eCount].transform.position = atomsStartPos.position + new Vector3(eCount * 1.2f, 0, 0);
			atoms[eCount].numElectrons(eCount + 1);
		}
	}
}
