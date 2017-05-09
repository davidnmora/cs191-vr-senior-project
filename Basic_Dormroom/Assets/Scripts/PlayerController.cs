using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
	private int count;
	public Text countText;
	public Text winText;

	void Start () 
	{
		rb = GetComponent<Rigidbody>(); // on first frame, get component
		count = 0;
		setCountText();
	}

	void FixedUpdate () {
		// record input from keys
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical   = Input.GetAxis ("Vertical");

		// load 3d vector of force (only works in x and y)
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Pickup")) {
			other.gameObject.SetActive(false);
			count++;
			setCountText();
		}
	}

	void setCountText() {
		countText.text = "Count: " + count.ToString();
		if (count >= 9) {
			countText.text = "";
			winText.text = "You win!!!";
		}
	}
}
