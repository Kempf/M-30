using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour {
	//raycast data
	public RaycastHit hit;

	//UI Elements
	private Canvas MainCanvas;
	private GameObject MainCanvasGO;
	private Canvas ScopeCanvas;
	private GameObject ScopeCanvasGO;
	private Text tagID;
	private GameObject tagIDGO;
	private Text Data;
	private GameObject DataGO;
	private Text Radio;
	private GameObject RadioGO;
	private Text Scoretxt;
	private GameObject ScoretxtGO;
	public static int Scoreint;

	//Camera Related
	public GameObject PlayerCamPos;
	public GameObject ScopeCamPos;
	private bool inScope;
	public Camera Main;

	//Object held position
	public GameObject Position;

	//Item in hands Y/N? String
	private bool HandsFree = true;
	private string IteminHands;
	//Angles for elev and trav in 6000div format
	public static float TravDiv;
	public static float ElevDiv;

	//Player Game Object
	public GameObject Player;

	//Scripts on player controller
	private MouseLook MouseLook1;
	private MouseLook MouseLook2;
	private FPSInputControllerC Controller;

	//Traverse script on gun

	private GunScript GunScript;

	private float speed;

	//private TraverseScript Trav;
	private GameObject Trav;
	private GameObject Elev;

	private GameObject Barrel;
	public static Quaternion randRot;

	//Field of Views
	public float NormFOV;
	public float ScopeFOV;

	private bool Started;

	void Awake () {
		Started = false;
	}

	void StartedCheck (){
		if (Started == false) {
			PlayerStart ();
			Started = true;
		} else {
			return;
		}
	}

	// Use this for initialization
	void PlayerStart () {

		GunScript = GameObject.Find ("M-30(Clone)").GetComponent<GunScript> ();

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
		tagID.text = "tagID";
		Data.text = "Data";
		Radio.text = "Radio";
		Scoretxt.text = Scoreint.ToString();

		MouseLook1 = Player.GetComponent <MouseLook>();
		MouseLook2 = PlayerCamPos.GetComponent <MouseLook>();
		Controller = Player.GetComponent <FPSInputControllerC>();

		inScope = false;
		ScopeCamPos = GameObject.Find ("ScopeCamPos");

		Trav = GameObject.FindGameObjectWithTag ("Traverse");//.gameObject.GetComponent<TraverseScript> ();
		Elev = GameObject.FindGameObjectWithTag ("Elevation");

		Barrel = GameObject.Find ("Barrel");

		GameObject myPlayer = gameObject;
	}

	public void AddScore(){
		Scoreint += 1;
		Scoretxt.text = Scoreint.ToString ();
        Radio.text = "";
		}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Focus"))
			speed = 2f;
		else
			speed = 0.3f;

		StartedCheck ();

		if (Input.GetButtonDown ("Submit")) {
			FireCheck ();
		}

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
			Chamber ();
		}



		//check if holding something
		if (HandsFree == true) {
			//checking tags etc.
			if (tagID.text == "Elevation") {
				Elevation ();
			} else if (tagID.text == "Traverse") {	
				Traverse ();
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
				hit.collider.gameObject.GetComponent<PhotonView>().RPC ("BreechChange", PhotonTargets.All, null);

			} else if (tagID.text == "Scope") {
				Traverse ();
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

				string TargetDist = ObjectScript.DistString;
				Radio.text = TargetDist;

		//		GameObject Target = GameObject.FindGameObjectWithTag ("Target");	
		//		ObjectScript script = Target.GetComponent<ObjectScript>() as ObjectScript; 
		//		string DistString = script.DistString;
		//		Radio.text = DistString;

			} else if (tagID.text == "Ammo Box" && Input.GetButtonDown ("Interact 1")) {
		
		//		GameObject AmmoBox = hit.collider.gameObject;
				hit.collider.gameObject.GetComponent<PhotonView>().RPC ("SpawnRound", PhotonTargets.MasterClient, null);
		//		AmmoBox.GetComponent<RoundSpawn>().SpawnRound ();
							
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
			PhotonNetwork.Destroy (Shell);
			PhotonNetwork.Destroy (Case);
		//	Destroy (Shell);
		//	Destroy (Case);
			IteminHands = "";
			HandsFree = true;
			PhotonNetwork.Instantiate ("Round",Pos, Rot, 0);
		//	Instantiate (Round, Pos, Rot);
		}

		if (Input.GetButtonDown ("Interact 2")){
			if (IteminHands != ""){
				gameObject.GetComponent<PhotonView>().RPC ("DropItem", PhotonTargets.All, null);
			}	
		}
	}

	void Traverse(){
		Data.text = TravDiv.ToString ();
		float TraverseMod;
		TraverseMod = (Input.GetAxis ("Mouse Scroll"))/speed;
		Trav.GetComponent<PhotonView> ().RPC ("Traverse", PhotonTargets.All, TraverseMod);
	}
	void Elevation(){
		Data.text = ElevDiv.ToString ();
		float ElevationMod;
		ElevationMod = (Input.GetAxis ("Mouse Scroll"))/speed;
		Elev.GetComponent<PhotonView> ().RPC ("Elevation", PhotonTargets.All, ElevationMod);	
	}
	void Chamber(){
		if (ChamberScript.Loaded == false){
			//loading round into chamber
			hit.collider.gameObject.GetComponent<PhotonView>().RPC ("LoadRound", PhotonTargets.All, null);
			GameObject RoundtoLoad = transform.Find (IteminHands).gameObject;
			PhotonNetwork.Destroy (RoundtoLoad);
			IteminHands = "";
			HandsFree = true;
		}
	}
	[RPC]
	void DropItem() {
		//dropping the object in hands

//		GameObject toDrop = myPlayer.transform.Find (IteminHands).gameObject;
//		toDrop.transform.parent = null;
//		toDrop.transform.rigidbody.constraints = RigidbodyConstraints.None;
//		toDrop.collider.enabled = true;
//		HandsFree = true;
//		IteminHands = "";
	}

	[RPC]
	void FireCheck (){
		float randX = Random.Range (-1f,1f) + Random.Range (-1f,1f) + Random.Range (-1f,1f);
		float randY = Random.Range (-1f,1f) + Random.Range (-1f,1f) + Random.Range (-1f,1f);
		randRot.eulerAngles = new Vector3 (randX / 30, randY / 30, 0f);

		Barrel.GetComponent<PhotonView>().RPC ("Fire", PhotonTargets.MasterClient, null);
	}
}
