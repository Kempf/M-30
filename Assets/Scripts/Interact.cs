using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour {
	//raycast data
	public RaycastHit hit;
	//UI texts
	public Text tagID;
	public GameObject tagIDGO;
	public Text Data;
	public GameObject DataGO;
	public Text Radio;
	public GameObject RadioGO;
	public Text Scoretxt;
	public GameObject ScoretxtGO;
	//Cameras
	public GameObject PlayerCamPos;
	public GameObject ScopeCamPos;
	private bool inScope;
	public Camera Main;
	public Canvas MainCanvas;
	public GameObject MainCanvasGO;
	public Canvas ScopeCanvas;
	public GameObject ScopeCanvasGO;
	//desired object help position
	public GameObject Position;
	//hands free? and what is in hands?
	public bool HandsFree = true;
	public string IteminHands;
	//angles for elev and trav in 6000div format
	private float ElevDiv;
	private float TravDiv;

	public GameObject Round;
	
	public int Scoreint;

	public GameObject Player;

	private MouseLook MouseLook1;
	private MouseLook MouseLook2;
	private FPSInputControllerC Controller;

	private TraverseScript Trav;

	public float NormFOV;
	public float ScopeFOV;

	// Use this for initialization
	void Awake () {

		MainCanvasGO = GameObject.Find ("Canvas - Main");
		MainCanvas = MainCanvasGO.GetComponent<Canvas> ();
		ScopeCanvasGO = GameObject.Find ("Canvas - Scope");
		ScopeCanvas = ScopeCanvasGO.GetComponent<Canvas> ();

		tagIDGO = GameObject.Find ("TagID");
		DataGO = GameObject.Find ("Data");
		RadioGO = GameObject.Find ("Radio");
		ScoretxtGO = GameObject.Find ("Score");

		tagID = tagIDGO.GetComponent<Text> ();
		Data = DataGO.GetComponent<Text> ();
		Radio = RadioGO.GetComponent<Text> ();
		Scoretxt = ScoretxtGO.GetComponent<Text> ();

		//clearing text
		tagID.text = "";
		Data.text = "";
		Scoretxt.text = "0";
		Scoreint = 0;

		MouseLook1 = Player.GetComponent <MouseLook>();
		MouseLook2 = PlayerCamPos.GetComponent <MouseLook>();
		Controller = Player.GetComponent <FPSInputControllerC>();

		inScope = false;
		ScopeCamPos = GameObject.Find ("ScopeCamPos");
	
		Trav = GameObject.FindGameObjectWithTag ("Traverse").gameObject.GetComponent<TraverseScript> ();
	}

	public void AddScore(){
		Scoreint += 1;
		Scoretxt.text = Scoreint.ToString ();
        Radio.text = "";
		}

	// Update is called once per frame
	void Update () {
				
                Screen.lockCursor = true;
				Screen.showCursor = false;
        
		if (inScope == false) {
						//Raycast and hit ID
						Vector3 Forward = transform.TransformDirection (Vector3.forward);
						if (Physics.Raycast (transform.position, Forward, out hit, 2f)) {
								tagID.text = hit.collider.tag;
						} else { 
								tagID.text = "";
						}
			
						Main.transform.position = PlayerCamPos.transform.position;
						Main.transform.rotation = PlayerCamPos.transform.rotation;
						Main.camera.fieldOfView = NormFOV;
				} else {
						Main.transform.position = ScopeCamPos.transform.position;
						Main.transform.rotation = ScopeCamPos.transform.rotation;
						Main.camera.fieldOfView = ScopeFOV;
				}

		if (tagID.text == "Chamber" && IteminHands == "Position/Round(Clone)" && Input.GetButtonDown ("Interact 1") ) {
			ChamberScript Cham = hit.collider.gameObject.GetComponent<ChamberScript> ();
			if (Cham.Loaded == false){
			//loading round into chamber
					GameObject RoundtoLoad = transform.Find (IteminHands).gameObject;
					Destroy (RoundtoLoad);
				//	ChamberScript Cham = hit.collider.gameObject.GetComponent<ChamberScript> ();
					Cham.Load ();
					IteminHands = "";
					HandsFree = true;
			}
		}



		//check if holding something
		if (HandsFree == true) {
						//checking tags etc.
						if (tagID.text == "Elevation") {
								//calling and using elevation script
								ElevationScript Elev = hit.collider.gameObject.GetComponent<ElevationScript> ();
								Elev.Interact1 ();
								ElevDiv = Mathf.Round (Elev.Elevation * 16.666f);
								Data.text = ElevDiv.ToString ();

						} else if (tagID.text == "Traverse") {		
								//calling and using traverse script
								//Trav = hit.collider.gameObject.GetComponent<TraverseScript> ();
								Trav.Interact1 ();
								TravDiv = Mathf.Round (Trav.Traverse * 16.666f);
								Data.text = TravDiv.ToString ();
		
						} else if (tagID.text == "Round" && Input.GetButtonDown ("Interact 1")) {		
								//picking up the round... figure it out
								hit.collider.transform.position = Position.transform.position;
								hit.collider.transform.parent = Position.transform;
								hit.collider.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
								hit.collider.enabled = false;
								HandsFree = false;
								IteminHands = "Position/Round(Clone)";

						} else if (tagID.text == "Shell" && Input.GetButtonDown ("Interact 1")) {		
								//picking up the shell... figure it out
								hit.collider.transform.position = Position.transform.position;
								hit.collider.transform.parent = Position.transform;
								hit.collider.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
								hit.collider.enabled = false;
								HandsFree = false;
								IteminHands = "Position/Shell(Clone)";

						} else if (tagID.text == "Case" && Input.GetButtonDown ("Interact 1")) {		
								//picking up the Case... figure it out
								hit.collider.transform.position = Position.transform.position;
								hit.collider.transform.parent = Position.transform;
								hit.collider.transform.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
								hit.collider.enabled = false;
								HandsFree = false;
								IteminHands = "Position/Case(Clone)";

						} else if (tagID.text == "Breech" && Input.GetButtonDown ("Interact 1")) {
								//activating breechdoor script
								BreechScript Brch = hit.collider.gameObject.GetComponent<BreechScript> ();
								Brch.Interact1 ();

						} else if (tagID.text == "Scope") {
								Trav.Interact1 ();
								if (Input.GetButtonDown ("Interact 1")){
								//switching cams
								
								MainCanvas.enabled = !MainCanvas.enabled;
								ScopeCanvas.enabled = !ScopeCanvas.enabled;
								inScope = !inScope;
								
					//			Main.enabled = !Main.enabled;
					//			ScopeCam.enabled = !ScopeCam.enabled;

								MouseLook1.enabled = !MouseLook1.enabled;
								MouseLook2.enabled = !MouseLook2.enabled;
								Controller.enabled = !Controller.enabled;
								}
						} else if (tagID.text == "Radio" && Input.GetButtonDown ("Interact 1")) {

								GameObject Target = GameObject.FindGameObjectWithTag ("Target");	
								ObjectScript script = Target.GetComponent<ObjectScript>() as ObjectScript; 
								string DistString = script.DistString;
								Radio.text = DistString;

						} else {
								Data.text = "";
						}
		}

		if (tagID.text == "Case" && IteminHands == "Position/Shell(Clone)" && Input.GetButtonDown ("Interact 1")) {
			//loading shell into case to make round
			GameObject Shell = transform.Find (IteminHands).gameObject;
			GameObject Case = hit.collider.gameObject;
			Vector3 Pos = Shell.transform.position;
			Quaternion Rot = Shell.transform.rotation;
			Destroy (Shell);
			Destroy (Case);
			IteminHands = "";
			HandsFree = true;
			Instantiate (Round, Pos, Rot);
		}

		if (Input.GetButtonDown ("Interact 2")){
			if (IteminHands != ""){
			//dropping the object in hands
			GameObject toDrop = transform.Find (IteminHands).gameObject;
			toDrop.transform.parent = null;
			toDrop.transform.rigidbody.constraints = RigidbodyConstraints.None;
			toDrop.collider.enabled = true;
			HandsFree = true;
			IteminHands = "";
			}	
		}
	}
}
