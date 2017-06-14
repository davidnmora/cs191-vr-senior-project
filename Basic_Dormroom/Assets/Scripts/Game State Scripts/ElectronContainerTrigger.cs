using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronContainerTrigger : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// COLLISIONS AND INTERACTIONS
	bool firstTimeCalled = true;
	void OnTriggerEnter(Collider other) {
		if (firstTimeCalled && (other.gameObject.name == "RightHandColliderSphere" || other.gameObject.name == "LeftHandColliderSphere")) {
			Debug.Log("COLLISION FROM HAND");
			firstTimeCalled = false;
			GameObject.Find("ManagerIntro(Clone)").GetComponent<ManagerIntroController>().advanceGameState();
		}
		
	}
}
