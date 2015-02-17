using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public GameObject TraverseWheel;
	public GameObject Pivot;
	public GameObject ElevationWheel;
	public GameObject BodyA;
	public GameObject Barrel;
	public GameObject FirePos;
	public GameObject BreechDoor;
	public GameObject ChamberCol;
	public GameObject ScopeCamPos;

	// Use this for initialization
	void Awake () {

	}

	[RPC]
	public void Traverse(float TraverseAngle){

		Pivot.transform.localEulerAngles = new Vector3 (0f, TraverseAngle, 0f);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
