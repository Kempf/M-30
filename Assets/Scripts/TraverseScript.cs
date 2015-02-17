using UnityEngine;
using System.Collections;

public class TraverseScript : MonoBehaviour {

	private float maxTraverse = 24.5f;

	public GameObject Pivot;

	public float TraverseAngle;

	private float zRot;
	private float yRot;

	private float TraverseMod;

	public float speed;

	// Use this for initialization
	void Start () {
		zRot = transform.localEulerAngles.z;
		yRot = transform.localEulerAngles.y;
	}

	void Update () {
		Interact.TravDiv = Mathf.Round (TraverseAngle * 16.666f);
	}

	[RPC]
	public void Traverse(float TraverseMod){
		//limits
		if (TraverseAngle <= maxTraverse && TraverseAngle >= -maxTraverse)
			TraverseAngle += TraverseMod;
		//limits
		if (TraverseAngle > maxTraverse)
			TraverseAngle = maxTraverse;
		//more limits
		if (TraverseAngle < -maxTraverse)
			TraverseAngle = -maxTraverse;
		//applying rotation
		Pivot.transform.localEulerAngles = new Vector3 (0f, TraverseAngle, 0f);
		transform.localEulerAngles = new Vector3 (TraverseAngle * -20f, yRot, zRot);
	}
}
