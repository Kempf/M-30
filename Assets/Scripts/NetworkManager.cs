using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject Spawn;
	public Camera MenuCamera;

	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	// Update is called once per frame
	void Connect () {
		Debug.Log ("Connect");
		PhotonNetwork.ConnectUsingSettings ( "M-30 A v001" );
		}

	void OnJoinedLobby () {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
		}

	void OnPhotonRandomJoinFailed () {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
		}

	void OnJoinedRoom () {
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
		}

	void SpawnMyPlayer(){
		MenuCamera.enabled = false;
		GameObject MyPlayer = PhotonNetwork.Instantiate ("Player", Spawn.transform.position, Spawn.transform.rotation, 0);
		MyPlayer.GetComponent <MouseLook> ().enabled = true;
		MyPlayer.GetComponent <CharacterMotorC> ().enabled = true;
		MyPlayer.GetComponent<FPSInputControllerC> ().enabled = true;
		MyPlayer.GetComponent<CharacterController> ().enabled = true;
		MyPlayer.GetComponentInChildren<MouseLook> ().enabled = true;
		MyPlayer.transform.FindChild ("PlayerCamPos").gameObject.SetActive (true);

		}
}
