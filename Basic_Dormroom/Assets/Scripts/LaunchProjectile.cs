using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void launch() {
		
		Rigidbody projRB = GetComponent<Rigidbody> ();
		GameObject projectile =  projRB.gameObject;
		Debug.Log (projectile.transform.forward);
		projRB.velocity = (projectile.transform.forward * 10f);
	}
}
