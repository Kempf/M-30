﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public ParticleSystem pSExplosion;
	public GameObject origin;
	public float distance;
	public GameObject explosionSphere;

	public float radius = 300.0F;
	public float power = 10000.0F;
	public float iVelocity = 515f;

	public Vector3 dir;

	public Vector3 lastPos;
	public Vector3 nowPos;

	private float spin;

	void Awake () {

		origin = GameObject.Find ("origin");
		Destroy (gameObject, 120);
		rigidbody.velocity = transform.forward * iVelocity;

		}

	void FixedUpdate () {

		nowPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		dir = nowPos - lastPos;

		if (dir != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(dir),
				Time.deltaTime * 1f
				);
		}

		spin += 2;
		GameObject Shell = transform.Find ("BulletGO").gameObject;
		Shell.transform.localEulerAngles = new Vector3 (0f, 0f, -spin);

		if (Physics.Raycast (transform.position, dir, 6f))
			Explode () ;

		lastPos = nowPos;

		}

	void OnCollisionEnter(Collision collision) {
		Explode ();
		}

	public void Explode () {

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			if (hit && hit.rigidbody)
				hit.rigidbody.AddExplosionForce (power, explosionPos, radius, 3.0F);
		}

		//destroy collider to stop collision spam
		Destroy (collider);
		//create volumetric explosion sphere
		Instantiate (explosionSphere, rigidbody.position, transform.rigidbody.rotation);
		//making it stay still
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		//print distance
		print (distance = Vector3.Distance (transform.position, origin.transform.position));
		//tells targets that they're getting blown up
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Target");
		foreach (GameObject target in targets){
			ObjectScript targetScript = target.gameObject.GetComponent<ObjectScript> ();
			targetScript.ExplosionHandler ();
		}
		//get rid of the object when it's all done
		Destroy (gameObject);

		}
}