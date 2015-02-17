using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	private Vector3 realPos;
	private Quaternion realRot;
	public float Speed = .1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (photonView.isMine) {
		} else {
			transform.position = Vector3.Lerp (transform.position, realPos, Speed);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, Speed);
		}
	}

	public void OnPhotonSerializeView(PhotonStream Stream, PhotonMessageInfo info) {
		if (Stream.isWriting) {
			Stream.SendNext(transform.position);
			Stream.SendNext (transform.rotation);
		} else {
			realPos = (Vector3) Stream.ReceiveNext();
			realRot = (Quaternion) Stream.ReceiveNext();
		}
	}
}
