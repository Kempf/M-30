﻿using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	public Rigidbody Bullet;
	public GameObject FirePos;

	public ParticleSystem pSFire;



	public float randX;
	public float randY;

	public Quaternion randRot;

	void Fire(){

		ChamberScript Cham = transform.Find ("ChamberCol").gameObject.GetComponent<ChamberScript> ();
		BreechScript Bree = transform.Find ("BreechDoor").gameObject.GetComponent<BreechScript> ();

		if (Cham.Loaded == true && Bree.open == false) {

			randX = Random.Range (-1f,1f) + Random.Range (-1f,1f) + Random.Range (-1f,1f);
			randY = Random.Range (-1f,1f) + Random.Range (-1f,1f) + Random.Range (-1f,1f);

			randRot.eulerAngles = new Vector3 (randX/30, randY/30, 0f);

				//prticle effects
				pSFire.Play ();
				//spawn and lauch projectile
				Rigidbody.Instantiate (Bullet, FirePos.transform.position, FirePos.transform.rotation * randRot);
				//recoil
				transform.localPosition = new Vector3 (0, 0, .6f);
				//unloading
				Cham.Loaded = false;
				Bree.EmpCase = true;

			}
		}

	// Update is called once per frame
	void FixedUpdate () {

		if (transform.localPosition.z > 0)
						transform.localPosition = new Vector3 (0, 0, transform.localPosition.z - .06f  );


		if (Input.GetButtonDown ("Submit")) 
						Fire ();
}
}