using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    private Vector3 _targetPosition;

	// Use this for initialization
	void Start () {
        _targetPosition = GameObject.Find("StormLighter").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(_targetPosition, Vector3.up, Time.deltaTime * 25);
	}
}
