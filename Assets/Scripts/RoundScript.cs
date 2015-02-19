using UnityEngine;
using System.Collections;

public class RoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			gameObject.GetComponent<PhotonView>().RPC ("DestroyRound", PhotonTargets.MasterClient, null);
		}
	}

	[RPC]
	public void DestroyRound(){
		PhotonNetwork.Destroy (gameObject);
	}
}
