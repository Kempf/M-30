using UnityEngine;
using System.Collections;

public class TraverseScript : MonoBehaviour {

	private float maxTraverse = 24.5f;

	public GameObject Pivot;

	public float Traverse;

	private float zRot;
	private float yRot;

	private float TraverseMod;

	public float speed;

	// Use this for initialization
	void Start () {
	
		Traverse = 0f;

		zRot = transform.localEulerAngles.z;
		yRot = transform.localEulerAngles.y;

	}
	
	// Update is called once per frame
	public void Interact1 () {

		if (Input.GetButton ("Focus"))
			speed = 2f;
		else
			speed = 0.3f;

		//trav value
		TraverseMod = (Input.GetAxis ("Mouse Scroll"))/speed;
		//limits
		if (Traverse <= maxTraverse && Traverse >= -maxTraverse)
			Traverse += TraverseMod;
		//limits
		if (Traverse > maxTraverse)
			Traverse = maxTraverse;
		//more limits
		if (Traverse < -maxTraverse)
			Traverse = -maxTraverse;
		//applying rotation
		Pivot.transform.localEulerAngles = new Vector3 (0f, Traverse, 0f);
		transform.localEulerAngles = new Vector3 (Traverse * -20f, yRot, zRot);

	}
}
