using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	public Rigidbody Bullet;
	public GameObject FirePos;
	public ParticleSystem pSFire;

	public float randX;
	public float randY;

	public Quaternion randRot;

	[RPC]
	public void FireCheck(){
		randX = Random.Range (-1f,1f) + Random.Range (-1f,1f) + Random.Range (-1f,1f);
		randY = Random.Range (-1f,1f) + Random.Range (-1f,1f) + Random.Range (-1f,1f);
		randRot.eulerAngles = new Vector3 (randX / 30, randY / 30, 0f);

		if (ChamberScript.Loaded == true && BreechScript.open == false) {
			//spawn and lauch projectile
			PhotonNetwork.Instantiate ("BulletPrefab", FirePos.transform.position, FirePos.transform.rotation * randRot, 0);
			gameObject.GetComponent<PhotonView>().RPC ("FireEffects", PhotonTargets.All, randRot);
	//		Rigidbody.Instantiate (Bullet, FirePos.transform.position, FirePos.transform.rotation * randRot);
			//recoil
			transform.localPosition = new Vector3 (0, 0, .6f);
			//unloading
			transform.Find ("ChamberCol").gameObject.GetComponent<PhotonView> ().RPC ("LoadRound", PhotonTargets.All, null);
			transform.Find ("BreechDoor").gameObject.GetComponent<PhotonView> ().RPC ("UnloadBreech", PhotonTargets.All, null);
		}
	}

	[RPC]
	public void FireEffects (Quaternion randRot) {
		//prticle effects
		pSFire.Play ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (transform.localPosition.z > 0)
						transform.localPosition = new Vector3 (0, 0, transform.localPosition.z - .06f  );


//		if (Input.GetButtonDown ("Submit")) 
//						PhotonView.RPC ("Fire", PhotonTargets.MasterClient);			
			//Fire ();
}
}