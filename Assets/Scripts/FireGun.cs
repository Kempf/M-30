using UnityEngine;
using System.Collections;

public class FireGun : MonoBehaviour {

	public GameObject FirePos;
	public ParticleSystem pSFire;

	[RPC]
	public void Fire () {

		if (ChamberScript.Loaded == true && BreechScript.open == false) {

			GameObject Bullet = PhotonNetwork.Instantiate ("BulletPrefab", FirePos.transform.position, FirePos.transform.rotation * Interact.randRot, 0);
			Bullet.GetComponent<BulletScript>().BulletEnabled = true;

			pSFire.Play ();

			transform.Find ("ChamberCol").gameObject.GetComponent<PhotonView> ().RPC ("LoadRound", PhotonTargets.All, null);
			transform.Find ("BreechDoor").gameObject.GetComponent<PhotonView> ().RPC ("UnloadBreech", PhotonTargets.All, null);
			gameObject.GetComponent<PhotonView>().RPC ("AfterFire", PhotonTargets.All, null);
		}
	}

	[RPC]
	public void AfterFire() {
		transform.localPosition = new Vector3 (0, 0, .6f);
	}

	void Update () {
		if (transform.localPosition.z > 0) {
			transform.localPosition = new Vector3 (0, 0, transform.localPosition.z - .06f);
		}
	}
}
