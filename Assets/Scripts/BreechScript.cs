using UnityEngine;
using System.Collections;

public class BreechScript : MonoBehaviour {

	public bool open;
	public float angle = 0f;
	public bool EmpCase;
	public GameObject ExpendedCase;

	public void Interact1(){

		open = !open;

		if (EmpCase == true) {

			EmpCase = !EmpCase;
			Vector3 pos = new Vector3 (transform.position.x - 0.25f, transform.position.y, transform.position.z - .1f);
			Vector3 rot = new Vector3 (transform.rotation.eulerAngles.x - 90f,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
			Instantiate (ExpendedCase, pos, Quaternion.Euler(rot));

				}
		}

	// Use this for initialization
	void Awake () {
		open = false;
		EmpCase = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (Input.GetKeyDown(KeyCode.C))
		    EmpCase = true;

		if (open == true && angle > -170)
				angle -= 5f;
		else if (open == false && angle < 1)
				angle += 5f;

		transform.localEulerAngles = new Vector3 (0f, angle, 0);
	}
}
