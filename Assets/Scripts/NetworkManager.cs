using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject GSpawn;
	public GameObject PSpawn;
	public GameObject MenuCamera;
	public GameObject Gameinterface;

	private bool isMaster = false;


	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	// Update is called once per frame
	void Connect () {

		if (PersistantDetails.Multiplayer == false) {
			print ("offline mode");
			isMaster = true;
			PhotonNetwork.offlineMode = true;
			PhotonNetwork.CreateRoom (null);
		} else {
			print ("online mode");
			PhotonNetwork.offlineMode = false;
			Debug.Log ("Connect");
			PhotonNetwork.ConnectUsingSettings ("M-30 A v001");
		}
	}

	void OnJoinedLobby () {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
		}

	void OnPhotonRandomJoinFailed () {
		Debug.Log ("OnPhotonRandomJoinFailed");
		isMaster = true;
		PhotonNetwork.CreateRoom (null);
		}

	void OnJoinedRoom () {
		Debug.Log ("OnJoinedRoom");

		SpawnMyPlayer ();
		}

	void SpawnMyPlayer(){
		if (isMaster == true) {
			PhotonNetwork.Instantiate ("M-30", GSpawn.transform.position, GSpawn.transform.rotation, 0);
			MenuCamera.SetActive (false);
			Gameinterface.SetActive (true);
			GameObject MyPlayer = PhotonNetwork.Instantiate ("PlayerM", PSpawn.transform.position, PSpawn.transform.rotation, 0);
			MyPlayer.GetComponent <MouseLook> ().enabled = true;
			MyPlayer.GetComponent <CharacterMotorC> ().enabled = true;
			MyPlayer.GetComponent<FPSInputControllerC> ().enabled = true;
			MyPlayer.GetComponent<CharacterController> ().enabled = true;
			MyPlayer.GetComponentInChildren<MouseLook> ().enabled = true;
			MyPlayer.transform.FindChild ("PlayerCamPos").gameObject.SetActive (true);
		} else {
			MenuCamera.SetActive (false);
			Gameinterface.SetActive (true);
			GameObject MyPlayer = PhotonNetwork.Instantiate ("Player", PSpawn.transform.position, PSpawn.transform.rotation, 0);
			MyPlayer.GetComponent <MouseLook> ().enabled = true;
			MyPlayer.GetComponent <CharacterMotorC> ().enabled = true;
			MyPlayer.GetComponent<FPSInputControllerC> ().enabled = true;
			MyPlayer.GetComponent<CharacterController> ().enabled = true;
			MyPlayer.GetComponentInChildren<MouseLook> ().enabled = true;
			MyPlayer.transform.FindChild ("PlayerCamPos").gameObject.SetActive (true);
		}
	}
}
