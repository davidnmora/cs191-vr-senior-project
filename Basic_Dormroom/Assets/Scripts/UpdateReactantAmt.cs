using UnityEngine;
using UnityEngine.UI;
using System;


public class UpdateReactantAmt : MonoBehaviour {

	void Start() {}
	void Update() {}

	public void update_value (float value) {
		Text myText = GetComponent<Text>();
		myText.text = value.ToString();
		/*this.guiText 
			foreach (string item in text_strings) {
				//if (item.name.equals ("text")) {
					double value = double.Parse(item);
					Debug.Log ("value" + value);
				//}
				
			}
		}
	}*/
	}
		
}
