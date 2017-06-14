using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager exists to store and track global variables
public class GameManager : Singleton<GameManager> { 
	protected GameManager() {}	
	// Atom interaction
	private GameObject[] grabbedAtoms;
	private float reactionDistThreshold = 1.7f;
	// State 
	private string[] GameStates = {
		"ManagerIntro",
		// TO DO: piston cannon goes here
		"Vendetta_0_Intro",
		"Vendetta_1_EightElectrons",
		"Vendetta_2_Ionic",
		"Vendetta_3_Covalent"
	};
	public static int gameState;
	public ManagerIntroController ManagerIntro;
	public Vendetta_0_IntroController Vendetta_0_Intro;
	public Vendetta_1_EightElectronsController Vendetta_1_EightElectrons;
	public Vendetta_2_IonicController Vendetta_2_Ionic;
	public Vendetta_3_CovalentController Vendetta_3_Covalent;
	// Events
	public delegate void ReactionAttempted(GameObject atom1, GameObject atom2, bool reactionSucceededIonic, bool reactionSucceededCovalent);
	public event ReactionAttempted OnReactionAttempted;

	// Use THIS for initialization
	void Awake () {
        Debug.Log("Press down SPACEBAR/right thumbstick to start game.");
		gameState = -1;
		// hide the touch controllers' sphere colliders
		GameObject.Find("LeftHandColliderSphere" ).GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
		GameObject.Find("RightHandColliderSphere").GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0f);
	}
	
	// Update is called once per frame
	bool firstTime = true;
	void Update () {     
		// press space bar to begin game 
		if (firstTime && (Input.GetKeyDown("space") || OVRInput.Get(OVRInput.Button.SecondaryThumbstick))) {
            Debug.Log("Starting Game!");
			advanceGameState(); 
			firstTime = false;
		}
		// Handle the user holding and reacting two atoms
		handleAtomInteraction();
	}

	// *** PUBLIC FUNCTIONS **

	/*
	 * Update state manages transitions between all states (descrete tasks).
	 * It organizes state flow and, when called, advances to the next state 
	 * -- meaning the state scripts it calls must call GameManager.advanceGameState() when they finish.
	 */
	public void advanceGameState() {
		gameState++;
		if (gameState < GameStates.Length) {
			switch (GameStates[gameState]) {
				case "ManagerIntro":
					Debug.Log("Executing " + GameStates[gameState]);
					ManagerIntroController MI = Instantiate(ManagerIntro, transform.position, Quaternion.identity) as ManagerIntroController;
					break;
				case "Vendetta_0_Intro":
					Debug.Log("Executing " + GameStates[gameState]);
					Vendetta_0_IntroController V0 = Instantiate(Vendetta_0_Intro, transform.position, Quaternion.identity) as Vendetta_0_IntroController;
					break;
				case "Vendetta_1_EightElectrons":
					Debug.Log("Executing " + GameStates[gameState]);
					Vendetta_1_EightElectronsController V1 = Instantiate(Vendetta_1_EightElectrons, transform.position, Quaternion.identity) as Vendetta_1_EightElectronsController;
					break;
				case "Vendetta_2_Ionic":
					Debug.Log("Executing " + GameStates[gameState]);
					Vendetta_2_IonicController V2 = Instantiate(Vendetta_2_Ionic, transform.position, Quaternion.identity) as Vendetta_2_IonicController;
					break;
				case "Vendetta_3_Covalent":
					Debug.Log("Executing " + GameStates[gameState]);
					Vendetta_3_CovalentController V3 = Instantiate(Vendetta_3_Covalent, transform.position, Quaternion.identity) as Vendetta_3_CovalentController;
					break;
				default:
					Debug.Log("DEFAULT: all states complete. End of states.");
					break;			
			}
		}
	}



	// *** PRIVATE FUNCTIONS **

	private void handleAtomInteraction() {
		grabbedAtoms = GameObject.FindGameObjectsWithTag("GrabbedAtom");
		if ((grabbedAtoms != null) && grabbedAtoms.Length == 2) { // TOUCH TO DO: should be == for touch controllers
			handReactionAttempts();
		} else {
			GetComponent<TextMesh>().text = "";
		}
	}

	private void handReactionAttempts() {
		float dist = Vector3.Distance(grabbedAtoms[0].transform.position, grabbedAtoms[1].transform.position);
		if (dist <= reactionDistThreshold) {
			transform.LookAt(Camera.main.transform);
			transform.position = midpoint(grabbedAtoms[0].transform.position, grabbedAtoms[1].transform.position);
			int numElectronsToShare = (dist/reactionDistThreshold) > 0.65 ? 1 : 2; // outer 35% shares 1, inner 65% shares 2 (sweet spot)
			// TO DO: detect ionic-ness and change text? or have a different button press to react ionically...
			GetComponent<TextMesh>().text = "Share " + numElectronsToShare.ToString() + "\nelectron" + (numElectronsToShare == 1 ? "" : "s") + "\n(click down thumbstick)";
			if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick) || OVRInput.Get(OVRInput.Button.SecondaryThumbstick)) {
				evalReaction(numElectronsToShare);
			}
		} else {
			GetComponent<TextMesh>().text = "";
		}
	}

	private Vector3 midpoint(Vector3 a, Vector3 b) {
		return (a + b)/2;
	}

	// Evaluates if reaction would satisfy both atoms with 8 electrons, triggers event with result
	private void evalReaction(int numElectronsToShare) {
		Debug.Log("Evaluating the atoms.");
		AtomController atom0 = grabbedAtoms[0].GetComponent<AtomController>();
		AtomController atom1 = grabbedAtoms[1].GetComponent<AtomController>();
		// Covalent: see if reactions works ionically (one has 8, one loses all valence electrons)
		 
		bool reactionSucceededIonic = true; // TO DO: impelment actual checking
		// Covalent: see if reaction could happen (e.g. both would now have 8 electrons)
		bool atom0Gets8 = (atom0.numTotalElectrons() + numElectronsToShare) == 8;
		bool atom1Gets8 = (atom1.numTotalElectrons() + numElectronsToShare) == 8;
		bool reactionSucceededCovalent = atom0Gets8 && atom1Gets8; 
		
		if (OnReactionAttempted != null) OnReactionAttempted(grabbedAtoms[0], grabbedAtoms[1], reactionSucceededIonic, reactionSucceededCovalent); // broadcast to event listeners
	}

}

