using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronController : MonoBehaviour {
	public AudioClip zap;
	public AudioClip zing;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// FOR TESTING/DEBUGGIN
	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(0)) {
			audio.PlayOneShot(zing, 0.7F);
		}
	}

}
